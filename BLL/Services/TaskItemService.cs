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
	public class TaskItemService : ITaskItemService
	{
		private readonly IUnitOfWork _unitOfWork;

		public TaskItemService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task AddAsync(TaskItem taskItem)
		{
			await _unitOfWork.TaskItems.AddAsync(taskItem);
			await _unitOfWork.CompleteAsync();
		}

		public async Task<IEnumerable<TaskItem>> GetAllAsync()
		{
			return await _unitOfWork.TaskItems.GetAll();
		}

		public async Task<TaskItem?> GetByIdAsync(int id)
		{
			return await _unitOfWork.TaskItems.GetByIdAsync(id);
		}

		public async Task UpdateAsync(TaskItem taskItem)
		{
			_unitOfWork.TaskItems.Update(taskItem);
			await _unitOfWork.CompleteAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var taskItem = await _unitOfWork.TaskItems.GetByIdAsync(id);
			if (taskItem != null)
			{
				_unitOfWork.TaskItems.Delete(taskItem);
				await _unitOfWork.CompleteAsync();
			}
		}

		public async Task<IEnumerable<TaskItem>> GetTasksByDepartmentAsync(int departmentId)
		{
			return await _unitOfWork.TaskItems.FindAsync(t => t.DepartmentId == departmentId);
		}

	}
}
