using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace UserService.Models;

public class User : IdentityUser<string>
{
  public List<Address>? Addresses { get; set; }
}