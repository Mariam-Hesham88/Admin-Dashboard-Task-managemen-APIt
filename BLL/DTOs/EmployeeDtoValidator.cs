using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
	public class EmployeeDtoValidator : AbstractValidator<EmployeeDto>
	{
		public EmployeeDtoValidator()
		{
			RuleFor(e => e.FullName)
			.NotEmpty().WithMessage("Full name is required")
			.MinimumLength(3).WithMessage("Full name must be at least 3 characters");

			RuleFor(e => e.Email)
				.NotEmpty().WithMessage("Email is required")
				.EmailAddress().WithMessage("A valid email is required");

			RuleFor(e => e.JobTitle)
				.NotEmpty().WithMessage("Job title is required");

			RuleFor(e => e.DepartmentId)
				.GreaterThan(0).WithMessage("DepartmentId must be greater than 0");
		}
	}
}
