using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entites
{
	public class TaskItem
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MinLength(3)]
		public string Title { get; set; }

		public string Description { get; set; }

		[MaxLength(20)]
		public string Priority { get; set; }

		[MaxLength(20)]
		public string Status { get; set; }

		[Required]
		[DataType(DataType.DateTime)]
		public DateTime? StartDate { get; set; }

		[Required]
		[DataType(DataType.DateTime)]
		public DateTime DeadLine { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		// Foreign Keys
		public int AssignedToId { get; set; }

		public int DepartmentId { get; set; }

		// Navigation Properties
		[ForeignKey("AssignedToId")]
		public Employee AssignedTo { get; set; }

		[ForeignKey("DepartmentId")]
		public Department Department { get; set; }

	}
}
