using FluentValidation;
using KUSYS.Data.DTO;
using KUSYS.Data.Entity;

namespace KUSYS.Api.Validation
{
	public class StudentValidator:AbstractValidator<StudentDTO>
	{
		public StudentValidator()
		{
			RuleFor(i => i.BirthDate).LessThan(DateTime.Now).WithMessage("BirthDate must be less than now");
		}
	}
}
