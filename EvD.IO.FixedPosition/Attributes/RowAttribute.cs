using System;

namespace EvD.IO.FixedPosition.Attributes
{
    /// <summary>
    /// Attribute to configure you row.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]

    public class RowAttribute : Attribute
    {
        /// <summary>
        /// Specify the length of your row.
        /// </summary>
        public int Length { get; set; }
    }
}
