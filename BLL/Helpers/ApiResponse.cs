using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Helpers
{
	public class ApiResponse<T>
	{
		public bool Success { get; set; } = true;
		public string Message { get; set; }
		public T Data { get; set; }

		public ApiResponse() { }

		public ApiResponse(T data, string message = null)
		{
			Success = true;
			Message = message;
			Data = data;
		}

		public ApiResponse(string message)
		{
			Success = false;
			Message = message;
		}
	}
}
