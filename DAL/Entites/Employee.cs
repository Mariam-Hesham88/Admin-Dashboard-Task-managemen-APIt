using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entites
{
	public class Employee
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MinLength(3)]
		public string FullName { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		public string JobTitle { get; set; }

		public Boolean IsActive { get; set; } = true;

		// FK
		[ForeignKey("Department")]
		public int DepartmentId { get; set; }

		// Navigation
		public Department? Department { get; set; }
		public ICollection<TaskItem>? Tasks { get; set; }

	}
}
