using DAL.Entites;
using DAL.Repositoies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
	public interface ITaskItemService
	{
		Task<IEnumerable<TaskItem>> GetAllAsync();
		Task<TaskItem?> GetByIdAsync(int id);
		Task AddAsync(TaskItem taskItem);
		Task UpdateAsync(TaskItem taskItem);
		Task DeleteAsync(int id);
	}
}
