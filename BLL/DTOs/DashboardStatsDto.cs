using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
	public class DashboardStatsDto
	{
		public int DepartmentsCount { get; set; }
		public int EmployeesCount { get; set; }
		public int CompletedTasks { get; set; }
		public int InProgressTasks { get; set; }
		public int OverdueTasks { get; set; }
		public string TopDepartmentByTasks { get; set; }
	}
}
