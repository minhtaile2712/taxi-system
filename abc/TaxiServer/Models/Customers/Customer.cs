using NetTopologySuite.Geometries;

namespace TaxiServer.Models.Customers;

public class Customer
{
    public string Id { get; set; }

    public string PhoneNumber { get; set; }

    public Point? Location { get; set; }

    public Customer(string phoneNumber)
    {
        Id = phoneNumber;
        PhoneNumber = phoneNumber;
    }

    public void SetLocation(Point? location)
    {
        Location = location;
    }
}
