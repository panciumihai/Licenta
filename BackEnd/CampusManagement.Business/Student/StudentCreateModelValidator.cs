using CampusManagement.Business.Student.Models;
using FluentValidation;

namespace CampusManagement.Business.Student
{
    public class StudentCreateModelValidator : AbstractValidator<StudentCreateModel>
    {
        public StudentCreateModelValidator()
        {
            RuleFor(s => s.LastName).NotEmpty().Length(2, 20).Matches(@"[A-Za-z]+");
            RuleFor(s => s.FirstName).NotEmpty().Length(2, 20).Matches(@"[A-Za-z]+");
            RuleFor(s => s.Email).NotEmpty().EmailAddress();
            RuleFor(s => s.Year).InclusiveBetween((short)1, (short)3);
            RuleFor(s => s.Password).NotEmpty().MinimumLength(6);
        }
    }
}
