namespace EvD.IO.FixedPosition.Models
{
    public class ColumnItem : IColumnItem
    {
        /// <summary>
        /// Start postion of your column.
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// End position of your column.
        /// </summary>
        public int End { get; set; }

        /// <summary>
        /// Format specifier for the data.
        /// </summary>
        public string Format { get; set; }
    }
}
