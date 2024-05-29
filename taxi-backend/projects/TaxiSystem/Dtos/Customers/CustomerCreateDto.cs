using System.ComponentModel.DataAnnotations;

namespace TaxiSystem.Dtos.Customers;

public class CustomerCreateDto
{
  [Required]
  public string PhoneNumber { get; set; } = null!;
}
