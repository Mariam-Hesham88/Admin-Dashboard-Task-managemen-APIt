using DAL.Data;
using DAL.Entites;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositoies
{
	public class UnitOfWork : IUnitOfWork
	{
		protected readonly ApplicationDbContext _context;

		public UnitOfWork(ApplicationDbContext context)
		{
			_context = context;
			Departments = new GenericRepository<Department>(_context);
			Employees = new GenericRepository<Employee>(_context);
			TaskItems = new GenericRepository<TaskItem>(_context);
		}

		public IGenericRepository<Department> Departments { get; private set; }
		public IGenericRepository<Employee> Employees { get; private set; }
		public IGenericRepository<TaskItem> TaskItems { get; private set; }
		public IDashboardRepository Dashboard { get; private set; }

		public async Task<int> CompleteAsync()
		{
			return await _context.SaveChangesAsync();
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
