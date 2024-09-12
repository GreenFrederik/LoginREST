using LoginREST;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("Register")]
    public IActionResult CreateUser(UserDTO userDTO)
    {
        if (!UserDatabase.IsNameValid(userDTO.Name))
            return BadRequest("Your name is too long, buster. (Max 32)");

        User user = new(userDTO.Name, userDTO.Password);
        if (UserDatabase.AddUser(user))
        {
            return Ok("User created: " + userDTO.Name);
        }

        return Conflict("User already exists");
    }

    [HttpPost("Login")]
    public IActionResult Login(UserDTO user)
    {
        if (!UserDatabase.IsUserValid(user))
            return BadRequest();

        string token = CreateToken(user);
        return Ok(token);
    }

    private string CreateToken(UserDTO userDTO)
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.Name, userDTO.Name),
            new Claim(ClaimTypes.Role, "user"),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("DANIATOKEN")));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
}