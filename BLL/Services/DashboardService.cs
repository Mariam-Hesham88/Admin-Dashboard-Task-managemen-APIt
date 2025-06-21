using BLL.DTOs;
using BLL.Services.Interfaces;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
	public class DashboardService : IDashboardService
	{
		private readonly IUnitOfWork _unitOfWork;
		public DashboardService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<DashboardStatsDto> GetStatisticsAsync()
		{
			return new DashboardStatsDto
			{
				DepartmentsCount = await _unitOfWork.Dashboard.GetDepartmentsCountAsync(),
				EmployeesCount = await _unitOfWork.Dashboard.GetEmployeesCountAsync(),
				CompletedTasks = await _unitOfWork.Dashboard.GetCompletedTasksCountAsync(),
				InProgressTasks = await _unitOfWork.Dashboard.GetInProgressTasksCountAsync(),
				OverdueTasks = await _unitOfWork.Dashboard.GetOverdueTasksCountAsync(),
				TopDepartmentByTasks = await _unitOfWork.Dashboard.GetTopDepartmentByTasksAsync()
			};
		}
	}
}

