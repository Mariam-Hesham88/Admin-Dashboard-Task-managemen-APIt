using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
	public interface IAuthService
	{
		Task<string?> LoginAsync(AdminLoginDto dto);
		Task<(bool Success, IEnumerable<string> Errors)> RegisterAsync(AdminRegisterDto dto);
	}
}
