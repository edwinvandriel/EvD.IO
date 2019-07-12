# IO Library for .NET core

### Fixed position creator class

This class helps you to create files or reports with fixed row lengths and columns at fixed positions.

The class you create presents the row in your file or report. Example of a class:
```c#
    [Row(Length = 80)]
    public class Line
    {
        [Column(Start = 1, End = 30)]
        public string Column1 { get; set; }
        [Column(Start = 35, End = 45, Format = "dd-MM-yyyy")]
        public DateTime Column2 { get; set; }
    }
```

This class creates a row with a width of 80 characters. Also with *Column1* on position **1** until **30** and *Column2* on position **35** until **45**.
*Column2* also uses a format. The format syntax is the same as from ``string.Format``.

```c#
var writer = new FixedWriter<Line>();
var rows = writer.ToFixedPositionString(lines);
```
In this example *lines* is a collections rows of type *Line*.
