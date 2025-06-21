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
	public class TaskItemsRepository : GenericRepository<TaskItem> , ITaskItemsRepository
	{
		private readonly ApplicationDbContext _context;

		public TaskItemsRepository(ApplicationDbContext context): base(context) 
		{
			_context = context;
		}
	}
}
