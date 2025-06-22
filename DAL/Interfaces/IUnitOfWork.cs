using DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		IGenericRepository<Department> Departments { get; }
		IGenericRepository<Employee> Employees { get; }
		IGenericRepository<TaskItem> TaskItems { get; }
		IDashboardRepository Dashboard { get; }
		IAdminRepository Admins { get; }

		Task<int> CompleteAsync(); //save changes
	}
}
