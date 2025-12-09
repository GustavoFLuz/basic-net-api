using System.ComponentModel.DataAnnotations;
using UserService.Enums;

namespace UserService.Dtos;

public class AssignRoleDto
{
  public required string UserId { get; set; }
  [EnumDataType(typeof(RolesEnum))]
  public required string Role { get; set; }
}