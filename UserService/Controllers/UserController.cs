using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserService.Data;
using UserService.Dtos;
using UserService.Models;

namespace UserService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{

  private readonly DatabaseContext _context;
  private readonly IMapper _mapper;
  public UserController(DatabaseContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  [HttpGet("")]
  public ActionResult<IEnumerable<User>> GetUsers()
  {
    return Ok(_context.Users.ToList());
  }

  [HttpGet("{id}")]
  public ActionResult<User> GetUserById(int id)
  {
    var user = _context.Users.Find(id);
    return user != null ? Ok(user) : NotFound();
  }

  [HttpPost("")]
  public ActionResult<User> CreateUser([FromBody] CreateUserDTO userDto)
  {
    User user = _mapper.Map<User>(userDto);

    _context.Users.Add(user);
    _context.SaveChanges();
    return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
  }
}