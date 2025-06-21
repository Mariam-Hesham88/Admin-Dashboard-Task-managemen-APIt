using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
	public class TaskItemDtoValidator : AbstractValidator<TaskItemDto>
	{
		public TaskItemDtoValidator()
		{
			RuleFor(t => t.Title)
			.NotEmpty().WithMessage("Title is required")
			.MinimumLength(3).WithMessage("Title must be at least 3 characters");

			RuleFor(t => t.Priority)
				.NotEmpty().WithMessage("Priority is required")
				.MaximumLength(20).WithMessage("Priority must not exceed 20 characters");

			RuleFor(t => t.Status)
				.NotEmpty().WithMessage("Status is required")
				.MaximumLength(20).WithMessage("Status must not exceed 20 characters");

			RuleFor(t => t.StartDate)
				.NotNull().WithMessage("Start date is required")
				.LessThan(t => t.DeadLine).WithMessage("Start date must be before deadline");

			RuleFor(t => t.DeadLine)
				.NotEmpty().WithMessage("Deadline is required");

			RuleFor(t => t.AssignedTo)
				.GreaterThan(0).WithMessage("AssignedTo must be a valid employee ID");

			RuleFor(t => t.DepartmentId)
				.GreaterThan(0).WithMessage("DepartmentId must be a valid department ID");
		}
	}
}
