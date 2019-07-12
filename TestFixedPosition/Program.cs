using EvD.IO.FixedPosition;
using EvD.IO.FixedPosition.Attributes;
using System;
using System.Collections.Generic;

namespace TestFixedPosition
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = new List<Line>();
            var startDate = new DateTime(2019, 1, 1);
            for(var idx = 1; idx < 1000; idx++)
            {
                startDate = startDate.AddDays(1);
                lines.Add(new Line { Column1 = $"Test regel {idx}", Column2 = startDate });
            }

            var writer = new FixedWriter<Line>();
            Console.WriteLine(writer.ToFixedPositionString(lines));
        }
    }

    [Row(Length = 80)]
    public class Line
    {
        [Column(Start = 1, End = 30)]
        public string Column1 { get; set; }
        [Column(Start = 35, End = 45, Format = "dd-MM-yyyy")]
        public DateTime Column2 { get; set; }
    }
}
