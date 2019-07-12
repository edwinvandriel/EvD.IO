namespace EvD.IO.FixedPosition.Models
{
    public interface IColumnItem
    {
        /// <summary>
        /// Start postion of your column.
        /// </summary>
        int Start { get; set; }

        /// <summary>
        /// End position of your column.
        /// </summary>
        int End { get; set; }

        /// <summary>
        /// Format specifier for the data.
        /// </summary>
        string Format { get; set; }
    }
}
