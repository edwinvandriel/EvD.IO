namespace EvD.IO.FixedPosition.Models
{
    public class ColumnItem : IColumnItem
    {
        public int Start { get; set; }
        public int End { get; set; }
        public string Format { get; set; }
    }
}
