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
	public class DepartmentService : IDepartmentService
	{
		private readonly IUnitOfWork _unitOfWork;

		public DepartmentService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<Department>> GetAllAsync()
		{
			return await _unitOfWork.Departments.GetAll();
		}

		public async Task<Department?> GetByIdAsync(int id)
		{
			return await _unitOfWork.Departments.GetByIdAsync(id);
		}

		public async Task AddAsync(Department department)
		{
			await _unitOfWork.Departments.AddAsync(department);
			await _unitOfWork.CompleteAsync();
		}

		public async Task UpdateAsync(Department department)
		{
			_unitOfWork.Departments.Update(department);
			await _unitOfWork.CompleteAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var dept = await _unitOfWork.Departments.GetByIdAsync(id);
			if (dept != null)
			{
				_unitOfWork.Departments.Delete(dept);
				await _unitOfWork.CompleteAsync();
			}
		}

	}
}
