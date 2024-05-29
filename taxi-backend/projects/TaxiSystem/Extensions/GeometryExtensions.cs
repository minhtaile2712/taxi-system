using NetTopologySuite.Geometries;
using ProjNet;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;

namespace TaxiSystem.Extensions;

public static class GeometryExtensions
{
    private static readonly CoordinateSystemServices _coordinateSystemServices
        = new CoordinateSystemServices(
            new Dictionary<int, string>
            {
                // Coordinate systems:

                [4326] = GeographicCoordinateSystem.WGS84.WKT,

                // This coordinate system covers the area of Vietnam.
                // Different data requires a different coordinate system.
                [32648] = "PROJCS[\"WGS 84 / UTM zone 48N\", " +
                    "GEOGCS[\"WGS 84\", " +
                    "DATUM[\"WGS_1984\", " +
                    "SPHEROID[\"WGS 84\", 6378137.0, 298.257223563, AUTHORITY[\"EPSG\",\"7030\"]], " +
                    "TOWGS84[0, 0, 0, 0, 0, 0, 0], " +
                    "AUTHORITY[\"EPSG\",\"6326\"]], " +
                    "PRIMEM[\"Greenwich\", 0.0, AUTHORITY[\"EPSG\",\"8901\"]], " +
                    "UNIT[\"degree\", 0.017453292519943295], " +
                    "AXIS[\"Geodetic longitude\", EAST], " +
                    "AXIS[\"Geodetic latitude\", NORTH], " +
                    "AUTHORITY[\"EPSG\",\"4326\"]], " +
                    "PROJECTION[\"Transverse_Mercator\"], " +
                    "PARAMETER[\"central_meridian\", 105.0], " +
                    "PARAMETER[\"latitude_of_origin\", 0.0], " +
                    "PARAMETER[\"scale_factor\", 0.9996], " +
                    "PARAMETER[\"false_easting\", 500000.0], " +
                    "PARAMETER[\"false_northing\", 0.0], " +
                    "UNIT[\"metre\", 1.0, AUTHORITY[\"EPSG\",\"9001\"]], " +
                    "AXIS[\"Easting\", EAST], " +
                    "AXIS[\"Northing\", NORTH], " +
                    "AUTHORITY[\"EPSG\",\"32648\"]]"
            });

    public static Geometry ProjectTo(this Geometry geometry, int srid)
    {
        var transformation = _coordinateSystemServices.CreateTransformation(geometry.SRID, srid);

        var result = geometry.Copy();
        result.Apply(new MathTransformFilter(transformation.MathTransform));

        return result;
    }

    private class MathTransformFilter : ICoordinateSequenceFilter
    {
        private readonly MathTransform _transform;

        public MathTransformFilter(MathTransform transform)
            => _transform = transform;

        public bool Done => false;
        public bool GeometryChanged => true;

        public void Filter(CoordinateSequence seq, int i)
        {
            var x = seq.GetX(i);
            var y = seq.GetY(i);
            var z = seq.GetZ(i);
            _transform.Transform(ref x, ref y, ref z);
            seq.SetX(i, x);
            seq.SetY(i, y);
            seq.SetZ(i, z);
        }
    }
}
