using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
	public class DepartmentDtoValidator : AbstractValidator<DepartmentDto>
	{
		public DepartmentDtoValidator()
		{
			RuleFor(d => d.Name)
				.NotEmpty().WithMessage("Name is required")
				.MinimumLength(2).WithMessage("Name must be at least 2 characters");

			RuleFor(d => d.Description)
				.MaximumLength(500).WithMessage("Description must be less than 500 characters");
		}
	}
}
