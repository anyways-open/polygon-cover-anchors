using CsvHelper.Configuration.Attributes;

namespace PolygonCovers.Anchors
{
    public class AnchorPoint
    {
        [Name("lon")]
        public double Lon { get; set; }
        [Name("lat")]
        public double Lat { get; set; }
        [Name("id")]
        public string Id { get; set; }
    }
}