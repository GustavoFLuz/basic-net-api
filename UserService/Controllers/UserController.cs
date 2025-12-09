using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserService.Data;
using UserService.Dtos;
using UserService.Models;

namespace UserService.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{

  private readonly DatabaseContext _context;
  private readonly IMapper _mapper;
  private readonly UserManager<User> _userManager;

  public UserController(DatabaseContext context, IMapper mapper, UserManager<User> userManager)
  {
    _context = context;
    _mapper = mapper;
    _userManager = userManager;
  }

  [HttpGet("me")]
  [Authorize]
  public async Task<ActionResult<User>> GetMe()
  {
    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    if (userId == null)
      return Unauthorized();

    var user = await _userManager.FindByIdAsync(userId);

    if (user == null)
      return NotFound();

    var roles = await _userManager.GetRolesAsync(user);

    return Ok(new
    {
      user.Id,
      user.UserName,
      user.Email,
      user.Addresses,
      Roles = roles,
    });
  }

  [HttpGet("")]
  [Authorize(Roles = "ADMIN")]
  public ActionResult<IEnumerable<User>> GetUsers()
  {
    return Ok(_userManager.Users.ToList());
  }

  [HttpGet("{id}")]
  [Authorize(Roles = "ADMIN")]
  public async Task<ActionResult<User>> GetUserById(string id)
  {
    var user = await _userManager.FindByIdAsync(id);
    return user != null ? Ok(user) : NotFound();
  }

  [HttpPost("assign-role")]
  [Authorize(Roles = "ADMIN")]
  public async Task<ActionResult> AssignRole([FromBody] AssignRoleDto assignRoleDTO)
  {
    var user = await _userManager.FindByIdAsync(assignRoleDTO.UserId);
    if (user == null)
      return NotFound("User not found");

    var result = await _userManager.AddToRoleAsync(user, assignRoleDTO.Role);
    if (!result.Succeeded)
      return BadRequest(result.Errors);

    return Ok("Role assigned");
  }
}