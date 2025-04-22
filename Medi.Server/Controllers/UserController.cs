using Microsoft.AspNetCore.Mvc;
using Medi.Server.Services;
using Medi.Server.Models;
using System.Collections.Generic;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] AuthRequest model)
    {
        var result = _userService.Register(model);
        if (!result) { 
            return BadRequest("Rejestracja nie powiodła się."); 
        }
        return Ok(new { message = "Rejestracja zakończona sukcesem" });
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
        var users = _userService.GetUsers();
        return Ok(users);
    }
}
