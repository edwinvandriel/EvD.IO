using EvD.IO.FixedPosition.Models;
using System;

namespace EvD.IO.FixedPosition.Attributes
{
    /// <summary>
    /// Attribute to configure your column.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ColumnAttribute : Attribute, IColumnItem
    {
        /// <summary>
        /// Start postion of your column. Start at position 1.
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// End position of your column.
        /// </summary>
        public int End { get; set; }

        /// <summary>
        /// Format your data. Use the syntax from String.Format
        /// like 'dd-MM-yyyy' to format a DateTime
        /// </summary>
        public string Format { get; set; }
    }
}
