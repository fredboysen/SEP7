using Microsoft.AspNetCore.Mvc;
using SEP7.Database.Data; 
using SEP7.WebAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SEP7.WebAPI.Services;


namespace SEP7.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class AuthController : ControllerBase
{
    private readonly IConfiguration config;
    private readonly IAuthService authService;
private readonly ApplicationDB _context;

    public AuthController(IConfiguration config, IAuthService authService, ApplicationDB context)
    {
        this.config = config;
        this.authService = authService;
        _context = context;
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        try
        {
            User user = await authService.UserValidation(loginDTO.username, loginDTO.password);
            string token = GenerateJwt(user);

            return Ok(token);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    private string GenerateJwt(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(config["Jwt:Key"] ?? "");

        List<Claim> claims = GenerateClaims(user);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = config["Jwt:Issuer"],
            Audience = config["Jwt:Audience"]
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private List<Claim> GenerateClaims(User user)
{
    var claims = new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub, config["Jwt:Subject"] ?? ""),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
        new Claim(ClaimTypes.Name, user.username),
        new Claim(ClaimTypes.NameIdentifier, user.user_id.ToString()),
        new Claim(ClaimTypes.Role, user.role),
    };
    return claims.ToList();
}

    

    
}


