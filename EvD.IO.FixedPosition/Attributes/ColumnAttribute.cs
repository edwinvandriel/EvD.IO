using System;

namespace EvD.IO.FixedPosition.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ColumnAttribute : Attribute
    {
        public int Start { get; set; }
        public int End { get; set; }
        public string Format { get; set; }
    }
}
