using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace UserService.Models;

public class User : IdentityUser<string>
{
  [Required]
  public required List<Address> Addresses { get; set; }
}