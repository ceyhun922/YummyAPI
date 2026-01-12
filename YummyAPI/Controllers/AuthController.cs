using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using YummyAPI.Context;
using YummyAPI.DTOs.AuthDTO;
using YummyAPI.Entities;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly ApiContext _context;
    private readonly IConfiguration _config;

    public AuthController(ApiContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    [Authorize]
    [HttpPost("setup-admin")]
    public IActionResult SetupAdmin([FromBody] CreateUserDto dto)
    {
        var setupKey = Request.Headers["X-Setup-Key"].ToString();
        var expected = _config["Setup:Key"];

        if (string.IsNullOrWhiteSpace(expected) || setupKey != expected)
            return Unauthorized("Invalid setup key.");

        if (_context.Users.Any())
            return Conflict("Admin already exists!");

        var user = new User
        {
            Username = dto.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok("Admin created successfully.");
    }

    [Authorize]
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto dto)
    {
        var user = _context.Users.FirstOrDefault(x => x.Username == dto.Username);
        if (user == null)
            return Unauthorized("User not found");

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            return Unauthorized("Password is wrong");

        var key = _config["Jwt:Key"]!;
        var creds = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            SecurityAlgorithms.HmacSha256
        );

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim("UserId", user.UserId.ToString()),
            new Claim("IsAdmin", "true")
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddDays(int.Parse(_config["Jwt:ExpireDays"] ?? "7")),
            signingCredentials: creds
        );

        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
    }

}
