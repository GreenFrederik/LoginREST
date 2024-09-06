using LoginREST;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
	[HttpPost]
	[Route("create")]
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
	
	[HttpPost]
	[Route("login")]
	public IActionResult Login(UserDTO user)
	{
		return Ok();
	}
}