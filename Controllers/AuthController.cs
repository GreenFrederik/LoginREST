using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
	[HttpPost]
	public IActionResult CreateUser(string name)
	{
		return Ok(name);
	}
}