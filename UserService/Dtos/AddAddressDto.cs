using System.ComponentModel.DataAnnotations;

namespace UserService.Dtos;

public class AddAddressDto
{
  [Required]
  public required string City { get; set; }
  [Required]
  public required string State { get; set; }
}