using BLL.DTOs;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Admin_dashboard.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] AdminRegisterDto dto)
		{
			var token = await _authService.RegisterAsync(dto);
			if (token == null) return BadRequest("Email already exists.");
			return Ok(new { token });
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] AdminLoginDto dto)
		{
			var token = await _authService.LoginAsync(dto);
			if (token == null) return Unauthorized("Invalid email or password.");
			return Ok(new { token });
		}
	}
}

