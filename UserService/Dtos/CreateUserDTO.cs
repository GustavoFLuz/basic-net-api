using System.ComponentModel.DataAnnotations;

namespace UserService.Dtos;

public class CreateUserDTO
{

  [Required]
  [StringLength(100)]
  public required string UserName { get; set; }
  [Required]
  [StringLength(100)]
  public required string Email { get; set; }
  [Required]
  [StringLength(100)]
  public required string Password { get; set; }
}