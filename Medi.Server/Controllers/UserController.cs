using Microsoft.AspNetCore.Mvc;
using Medi.Server.Services;

using Microsoft.AspNetCore.Identity.Data;
using Medi.Server.Models.DTOs;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;
    private readonly ILogger<UsersController> _logger;

    public UsersController(UserService userService, ILogger<UsersController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] AuthRequest model)
    {
        _logger.LogInformation("User registration attempt: {Email}", model.Email);

        var result = _userService.Register(model);

        if (!result.Success)
        {
            _logger.LogWarning("User registration failed: {Reason}", result.Message);

            return result.ErrorType switch
            {
                ErrorType.Duplicate => Conflict(new { message = result.Message }),// 409
                ErrorType.Validation => UnprocessableEntity(new { message = result.Message }),// 422
                ErrorType.Database => StatusCode(500, new { message = result.Message }),// 500
                _ => BadRequest(new { message = result.Message })// 400
            };
        }

        _logger.LogInformation("User registered successfully: {Email}", model.Email);
        return Ok(new { message = result.Message });
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
        var users = _userService.GetUsers();
        return Ok(users);
    }
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest model)
    {
        try
        {
            _logger.LogInformation("User attempting to login with email: {Email}", model.Email);

            var result = _userService.Login(model);

            if (!result.Success)
            { 
                _logger.LogInformation("Failed login attempt with email: {Email}. Error: {Error}", model.Email, result.Message);
                return result.ErrorType switch
                {
                    ErrorType.Validation => UnprocessableEntity(new { message = result.Message }),
                    ErrorType.Database => StatusCode(500, new { message = result.Message }),
                    _ => BadRequest(new { message = result.Message })
                };
            }

            _logger.LogInformation("User logged in successfully with email: {Email}", model.Email);
            return Ok(new
            {
                message = result.Message,
                token = result.Data?.Token
            });

        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex, "An error occurred during login attempt for email: {Email}", model.Email);
            return StatusCode(500, new { message = "Internal server error." });
        }
    }
}
