using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserService.Data;
using UserService.Dtos;
using UserService.Models;

namespace UserService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
  private readonly DatabaseContext _context;
  private readonly UserManager<User> _userManager;
  private readonly SignInManager<User> _signInManager;
  private readonly IMapper _mapper;

  public AuthController(DatabaseContext context, UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
  {
    _context = context;
    _userManager = userManager;
    _signInManager = signInManager;
    _mapper = mapper;
  }

  [HttpPost("register")]
  public async Task<ActionResult> Register([FromBody] CreateUserDto userDTO)
  {
    var user = _mapper.Map<User>(userDTO);
    if (string.IsNullOrEmpty(user.Id))
      user.Id = System.Guid.NewGuid().ToString();

    var result = await _userManager.CreateAsync(user, userDTO.Password);

    if (!result.Succeeded)
      return BadRequest(result.Errors);

    await _userManager.AddToRoleAsync(user, "USER");

    return Ok("User registered");
  }

  [HttpPost("login")]
  public async Task<ActionResult> Login([FromBody] LoginUserDto loginDTO)
  {
    var result = await _signInManager.PasswordSignInAsync(loginDTO.Username, loginDTO.Password, false, false);
    if (!result.Succeeded)
      return Unauthorized("Invalid username or password");

    return Ok("User logged in");
  }

  [HttpPost("logout")]
  [Authorize]
  public async Task<ActionResult> Logout()
  {
    await _signInManager.SignOutAsync();
    return Ok("User logged out");
  }
}