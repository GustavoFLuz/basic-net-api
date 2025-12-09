using System.ComponentModel.DataAnnotations;

namespace UserService.Models;

public class Address
{
    [Key, Required]
    public int Id { get; set; }

    [Required]
    public required string City { get; set; }
    [Required]
    public required string State { get; set; }
}