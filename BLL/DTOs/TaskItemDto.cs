using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
	public class TaskItemDto
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public string Priority { get; set; }

		public string Status { get; set; }

		public DateTime? StartDate { get; set; }

		public DateTime DeadLine { get; set; }

		public DateTime CreatedAt { get; set; }

		public int AssignedTo { get; set; }

		public int DepartmentId { get; set; }
	}
}
