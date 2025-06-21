using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entites
{
	public class Department
	{
		[Key]
		public int Id { get; set; }

		[MinLength(2)]
		[MaxLength(250)]
		public string Name { get; set; }

		[Required]
		public string Description { get; set; }

		// Navigation
		public ICollection<Employee>? Employees { get; set; }
		public ICollection<TaskItem>? Tasks { get; set; }
	}
}
