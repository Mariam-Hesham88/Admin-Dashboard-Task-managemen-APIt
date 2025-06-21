using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
	public interface IDashboardRepository
	{
		Task<int> GetDepartmentsCountAsync();
		Task<int> GetEmployeesCountAsync();
		Task<int> GetCompletedTasksCountAsync();
		Task<int> GetInProgressTasksCountAsync();
		Task<int> GetOverdueTasksCountAsync();
		Task<string> GetTopDepartmentByTasksAsync();
	}
}
