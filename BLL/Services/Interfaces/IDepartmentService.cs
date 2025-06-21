using DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
	public interface IDepartmentService
	{
		Task<IEnumerable<Department>> GetAllAsync();
		Task<Department?> GetByIdAsync(int id);
		Task AddAsync(Department department);
		Task UpdateAsync(Department department);
		Task DeleteAsync(int id);
	}
}
