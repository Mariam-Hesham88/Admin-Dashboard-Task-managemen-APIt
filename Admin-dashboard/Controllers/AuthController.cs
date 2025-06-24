using BLL.DTOs;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Admin_dashboard.Controllers
{
	[AllowAnonymous]
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
		public async Task<IActionResult> Register(AdminRegisterDto dto)
		{
			var result = await _authService.RegisterAsync(dto);
			if (result.Success)
				return Ok("User registered successfully.");
			return BadRequest(result.Errors);
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(AdminLoginDto dto)
		{
			var token = await _authService.LoginAsync(dto);
			if (token == null)
				return Unauthorized("Invalid credentials");

			return Ok(new { token });
		}
	}
}

