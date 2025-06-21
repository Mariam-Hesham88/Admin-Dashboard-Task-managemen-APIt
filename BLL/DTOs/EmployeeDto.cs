using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
	public class EmployeeDto
	{
		public int Id { get; set; }
		public string FullName { get; set; }
		public string Email { get; set; }
		public string JobTitle { get; set; }
		public int DepartmentId { get; set; }
	}
}
