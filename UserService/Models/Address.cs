using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Models;

public class Address
{
    [Key, Required]
    public required string Id { get; set; }

    [Required]
    public required string City { get; set; }
    [Required]
    public required string State { get; set; }

    public required string UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
}