using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Aflevering_Del1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shared;
using Shared.Dto;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Aflevering_Del1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IAuthService _authService;

    public AuthController(IConfiguration config, IAuthService authService)
    {
        this._config = config;
        this._authService = authService;
    }

    private List<Claim> GenerateClaims(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim("DisplayName", user.Name),
            new Claim("Email", user.Email),
        };
        return claims.ToList();
    }

    private string GenerateJwt(User user)
    {
        List<Claim> claims = GenerateClaims(user);

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        JwtHeader header = new JwtHeader(signIn);

        JwtPayload payload = new JwtPayload(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            null,
            DateTime.UtcNow.AddMinutes(60));

        JwtSecurityToken token = new JwtSecurityToken(header, payload);

        string serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return serializedToken;
    }
    
    [HttpPost, Route("login")]
    public async Task<ActionResult> Login([FromBody] UserDto userLoginDto)
    {
        try
        {
            if (userLoginDto == null)
            {
                return BadRequest("Invalid request body");
            }
            User user = await _authService.GetUser(userLoginDto.Username, userLoginDto.Password);
            string token = GenerateJwt(user);

            return Ok(token);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

     
    [HttpPost, Route("register")]
    public async Task<ActionResult> RegisterUser([FromBody] User userRegistration)
    {
        try
        {
            // Create the new user account
            User newUser = new User
            {
                Username = userRegistration.Username,
                Password = userRegistration.Password,
                Email = userRegistration.Email,
                Name = userRegistration.Name
            };

            // Call your registration service to add the user to the database
            await _authService.RegisterUser(newUser);

            return Ok("User registered successfully");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}