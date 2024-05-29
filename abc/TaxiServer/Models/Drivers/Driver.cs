using NetTopologySuite.Geometries;

namespace TaxiServer.Models.Drivers;

public class Driver
{
    public string Id { get; set; }

    public string PhoneNumber { get; set; }

    public string Name { get; set; }

    public string AvatarUrl { get; set; }

    public Point? Location { get; set; }

    public Driver(string phoneNumber, string name, string avatarUrl)
    {
        Id = phoneNumber;
        PhoneNumber = phoneNumber;
        Name = name;
        AvatarUrl = avatarUrl;
    }

    public void SetLocation(Point? location)
    {
        Location = location;
    }

    public void Abc()
    {
        var seattle = new Point(-122.333056, 47.609722) { SRID = 4326 };
        var redmond = new Point(-122.123889, 47.669444) { SRID = 4326 };

        var distanceInDegrees = seattle.Distance(redmond);
        var distanceInMeters = seattle.ProjectTo(2855).Distance(redmond.ProjectTo(2855));
    }
}
