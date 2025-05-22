namespace KaizenCase2.Models
{
    public class Item
    {
        public string? Locale { get; set; }
        public required string Description { get; set; }
        public required BoundingPoly BoundingPoly { get; set; }
    }

    public class BoundingPoly
    {
        public required List<Coordinate> Vertices { get; set; }
    }

    public class Coordinate
    {
        public short X { get; set; }
        public short Y { get; set; }
    }
}