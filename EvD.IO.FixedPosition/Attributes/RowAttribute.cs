using System;

namespace EvD.IO.FixedPosition.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]

    public class RowAttribute : Attribute
    {
        public int Length { get; set; }
    }
}
