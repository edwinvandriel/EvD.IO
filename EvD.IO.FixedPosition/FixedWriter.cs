using EvD.IO.FixedPosition.Attributes;
using EvD.IO.FixedPosition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EvD.IO.FixedPosition
{
    /// <summary>
    /// Class for creating fixed position column rows
    /// </summary>
    /// <typeparam name="T">Class to convert</typeparam>
    public class FixedWriter<T> 
    {
        private readonly Type _classInfo;
       
        public FixedWriter()
        {
            _classInfo = typeof(T);
        }

        /// <summary>
        /// Create lines with fixed columns
        /// </summary>
        /// <param name="rows">Collection with objects to convert</param>
        /// <returns>String with every line a row containing the fixed columns</returns>
        public string ToFixedPositionString(IEnumerable<T> rows)
        {
            var classType = typeof(T);

            var totalCharacters = GetRowLength(classType);
            var columnConfiguration = GetColumnConfiguration();

            var results = new StringBuilder();
            foreach (var row in rows)
            {
                var emptyLine = new string(' ', totalCharacters);

                foreach (var config in columnConfiguration)
                {
                    var columnName = config.Key;
                    var column = config.Value;

                    var rawPropertyValue = classType.GetProperty(columnName).GetValue(row);
                    var propertyValue = rawPropertyValue.ToString();

                    if (!String.IsNullOrWhiteSpace(column.Format))
                        propertyValue = String.Format("{0:" + column.Format + "}", classType.GetProperty(columnName).GetValue(row));

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

        private int GetRowLength(Type classType)
        {
            var rowAttribute = (RowAttribute)classType.GetCustomAttribute(typeof(RowAttribute));
            if (rowAttribute == null) throw new NotImplementedException("Row attribute not found");

            return rowAttribute.Length;
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
