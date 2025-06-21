using BLL.Services.Interfaces;
using DAL.Entites;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
	public class EmployeeService : IEmployeeService
	{
		private readonly IUnitOfWork _unitOfWork;

		public EmployeeService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task AddAsync(Employee employee)
		{
			await _unitOfWork.Employees.AddAsync(employee);
			await _unitOfWork.CompleteAsync();
		}

		public async Task<IEnumerable<Employee>> GetAllAsync()
		{
			return await _unitOfWork.Employees.GetAll();
		}

		public async Task<Employee?> GetByIdAsync(int id)
		{
			return await _unitOfWork.Employees.GetByIdAsync(id);
		}

		public async Task UpdateAsync(Employee employee)
		{
			_unitOfWork.Employees.Update(employee);
			await _unitOfWork.CompleteAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var emp = await _unitOfWork.Employees.GetByIdAsync(id);
			if (emp != null)
			{
				_unitOfWork.Employees.Delete(emp);
				await _unitOfWork.CompleteAsync();
			}
		}
	}
}
