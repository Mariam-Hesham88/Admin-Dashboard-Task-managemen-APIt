using BLL.DTOs;
using BLL.Services.Interfaces;
using DAL.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BLL.Services
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IConfiguration _configuration;

		public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
		{
			Console.WriteLine("AuthService is being constructed");
			_userManager = userManager;
			_configuration = configuration;
		}

		public async Task<(bool Success, IEnumerable<string> Errors)> RegisterAsync(AdminRegisterDto dto)
		{
			var user = new ApplicationUser { UserName = dto.Email, Email = dto.Email };
			var result = await _userManager.CreateAsync(user, dto.Password);

			if (result.Succeeded)
			{
				await _userManager.AddToRoleAsync(user, "Admin");
				return (true, Enumerable.Empty<string>());
			}

			return (false, result.Errors.Select(e => e.Description));
		}

		public async Task<string?> LoginAsync(AdminLoginDto dto)
		{
			var user = await _userManager.FindByEmailAsync(dto.Email);
			if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
				return null;

			var roles = await _userManager.GetRolesAsync(user);
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id),
				new Claim(ClaimTypes.Name, user.UserName)
			};

			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: _configuration["JWT:Issuer"],
				claims: claims,
				expires: DateTime.Now.AddDays(7),
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}