using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using NetTopologySuite.Features;
using NetTopologySuite.IO;

namespace PolygonCovers.Anchors
{
    class Program
    {
        static void Main(string[] args)
        {
            var geoJson = @"/data/work/anyways/projects/fod-od/data/hexagon-layer.geojson";
            using var geoJsonStream = File.OpenRead(geoJson);
            using var textStream = new StreamReader(geoJsonStream);

            var features = NetTopologySuite.IO.GeoJsonSerializer.CreateDefault().Deserialize(textStream, typeof(FeatureCollection)) as FeatureCollection;

            IEnumerable<AnchorPoint> GetAnchors()
            {
                foreach (var feature in features) {
                    if (!feature.Attributes.TryGetId(out var id)) continue;
                    
                    var center = feature.Geometry.Centroid;
                    yield return new AnchorPoint()
                    {
                        Id = id.ToString(),
                        Lon = center.X,
                        Lat = center.Y
                    };
                }
            }

            using var writer = new StreamWriter(File.Open("file.csv", FileMode.Create));
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords((IEnumerable) GetAnchors());
        }
    }
}
