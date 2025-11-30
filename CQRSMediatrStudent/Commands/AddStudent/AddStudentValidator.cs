using FluentValidation;

namespace CQRSMediatrStudent.Commands.AddStudent
{
    public class AddStudentValidator : AbstractValidator<AddStudentCommand>
    {
        public AddStudentValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Student name is required.")
                .MinimumLength(3).WithMessage("Student name must be at least 3 characters long.");

            RuleFor(x => x.Age)
                .NotEmpty().WithMessage("Student age is required.")
                .GreaterThan(0).WithMessage("Student age must be greater than zero.");
        }
    }

}
