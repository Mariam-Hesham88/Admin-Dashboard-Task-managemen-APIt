using DAL.Data;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
	public class DashboardRepository : IDashboardRepository
	{
		private readonly ApplicationDbContext _context;

		public DashboardRepository(ApplicationDbContext context)
		{
			_context = context;
		}


		public async Task<int> GetDepartmentsCountAsync()
		{
			return await _context.Departments.CountAsync();

		}

		public async Task<int> GetEmployeesCountAsync()
		{
			return await _context.Employees.CountAsync();
		}

		public async Task<int> GetCompletedTasksCountAsync()
		{
			return await _context.Tasks.Where(t => t.Status == "Completed").CountAsync();
		}

		public async Task<int> GetInProgressTasksCountAsync()
		{
			return await _context.Tasks.Where(t => t.Status == "InProgress").CountAsync();
		}

		public async Task<int> GetOverdueTasksCountAsync()
		{
			return await _context.Tasks.Where(
				t => t.DeadLine < DateTime.UtcNow &&
				t.Status != "Completed").CountAsync();
		}

		public async Task<string> GetTopDepartmentByTasksAsync()
		{
			var result = await _context.Tasks
		   .GroupBy(t => t.DepartmentId)
		   .OrderByDescending(g => g.Count())
		   .Select(g => g.Key)
		   .FirstOrDefaultAsync();

			var department = await _context.Departments.FindAsync(result);
			return department?.Name ?? "N/A";
		}
	}
}
