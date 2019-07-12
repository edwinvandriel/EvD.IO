namespace EvD.IO.FixedPosition.Models
{
    public interface IColumnItem
    {
        int Start { get; set; }
        int End { get; set; }
        string Format { get; set; }
    }
}
