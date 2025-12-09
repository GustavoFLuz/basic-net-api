using System;
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
public class AddressController : ControllerBase
{
  private readonly DatabaseContext _context;
  private readonly UserManager<User> _userManager;
  private readonly IMapper _mapper;

  public AddressController(DatabaseContext context, UserManager<User> userManager, IMapper mapper)
  {
    _context = context;
    _userManager = userManager;
    _mapper = mapper;
  }

  [HttpGet("")]
  public async Task<ActionResult<IEnumerable<Address>>> GetAddresss()
  {
    User? user = await _userManager.GetUserAsync(User);
    if (user == null)
      return Unauthorized();
    var addresses = _context.Addresses.Where(a => a.UserId ==  user.Id).ToList();
    return Ok(addresses);
  }
  [HttpPost("")]
  public async Task<ActionResult<Address>> AddAddress([FromBody] AddAddressDto newAddressDto)
  {
    var user = await _userManager.GetUserAsync(User);

    if (user == null)
      return Unauthorized();

    var address = _mapper.Map<Address>(newAddressDto);
    address.Id = Guid.NewGuid().ToString();
    address.UserId = user.Id;

    _context.Add(address);
    await _context.SaveChangesAsync();

    return Ok(address);
  }

}