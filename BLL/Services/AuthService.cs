using BCrypt.Net;
using BLL.DTOs;
using BLL.Services.Interfaces;
using DAL.Data;
using DAL.Entites;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
	public class AuthService : IAuthService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IConfiguration _configuration;

		public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration)
		{
			_unitOfWork = unitOfWork;
			_configuration = configuration;
		}

		public async Task<string?> RegisterAsync(AdminRegisterDto dto)
		{
			var exists = await _unitOfWork.Admins.GetByEmailAsync(dto.Email);
			if (exists != null) return null;

			var admin = new Admin
			{
				UserName = dto.UserName,
				Email = dto.Email,
				Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
			};

			await _unitOfWork.Admins.AddAsync(admin);
			await _unitOfWork.CompleteAsync();

			return GenerateToken(admin);
		}

		public async Task<string?> LoginAsync(AdminLoginDto dto)
		{
			var admin = await _unitOfWork.Admins.GetByEmailAsync(dto.Email);
			if (admin == null || !BCrypt.Net.BCrypt.Verify(dto.Password, admin.Password))
				return null;

			return GenerateToken(admin);
		}

		private string GenerateToken(Admin admin)
		{
			var claims = new[]
			{
			new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()),
			new Claim(ClaimTypes.Name, admin.UserName),
			new Claim(ClaimTypes.Email, admin.Email)
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var token = new JwtSecurityToken(
				issuer: _configuration["Jwt:Issuer"],
				audience: _configuration["Jwt:Audience"],
				claims: claims,
				expires: DateTime.Now.AddDays(7),
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}


}
