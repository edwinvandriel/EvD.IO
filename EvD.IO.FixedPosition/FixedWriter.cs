using EvD.IO.FixedPosition.Attributes;
using EvD.IO.FixedPosition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EvD.IO.FixedPosition
{
    public class FixedWriter<T> 
    {
        private readonly Type _classInfo;
       
        public FixedWriter()
        {
            _classInfo = typeof(T);
        }

        private int GetRowLength(Type classType)
        {
            var rowAttribute = (RowAttribute)classType.GetCustomAttribute(typeof(RowAttribute));
            if (rowAttribute == null) throw new NotImplementedException("Row attribute not found");

            return rowAttribute.Length;
        }

        public string ToFixedPositionString(IEnumerable<T> invoices)
        {
            var classType = typeof(T);

            var totalCharacters = GetRowLength(classType);
            var columnConfiguration = GetColumnConfiguration();

            var results = new StringBuilder();
            foreach (var invoice in invoices)
            {
                var emptyLine = new string(' ', totalCharacters);

                foreach (var config in columnConfiguration)
                {
                    var columnName = config.Key;
                    var column = config.Value;

                    var rawPropertyValue = classType.GetProperty(columnName).GetValue(invoice);
                    var propertyValue = rawPropertyValue.ToString();

                    if (!String.IsNullOrWhiteSpace(column.Format))
                        propertyValue = String.Format("{0:" + column.Format + "}", classType.GetProperty(columnName).GetValue(invoice));

                    var propertyLength = column.End - column.Start;

                    if (propertyValue.Length < propertyLength)
                    {
                        propertyValue += new string(' ', propertyLength - propertyValue.Length);
                    }

                    var validValue = propertyValue.Substring(0, propertyLength);

                    emptyLine = emptyLine.Insert(column.Start - 1, validValue);
                }

                results.AppendLine(emptyLine.Substring(0, totalCharacters));
            }

            return results.ToString();
        }

        private Dictionary<string, IColumnItem> GetColumnConfiguration()
        {
            var allMembers = from m in _classInfo.GetMembers()
                             let columnAttribute = (ColumnAttribute)m.GetCustomAttribute(typeof(ColumnAttribute))
                             where columnAttribute != null
                             orderby columnAttribute.Start
                             select new
                             {
                                 m.Name,
                                 columnAttribute.Start,
                                 columnAttribute.End,
                                 columnAttribute.Format
                             };

            return allMembers.ToDictionary(k => k.Name, v => new ColumnItem { Start = v.Start, End = v.End, Format = v.Format } as IColumnItem);
        }

    }
}
