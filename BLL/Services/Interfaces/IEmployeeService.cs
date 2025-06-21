using DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
	public interface IEmployeeService
	{
		Task<IEnumerable<Employee>> GetAllAsync();
		Task<Employee?> GetByIdAsync(int id);
		Task AddAsync(Employee employee);
		Task UpdateAsync(Employee employee);
		Task DeleteAsync(int id);
	}
}
