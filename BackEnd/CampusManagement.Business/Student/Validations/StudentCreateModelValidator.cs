using CampusManagement.Business.Person.Validations;
using CampusManagement.Business.Student.Models;
using FluentValidation;

namespace CampusManagement.Business.Student
{
    public class StudentCreateModelValidator : AbstractValidator<StudentCreateModel>
    {
        public StudentCreateModelValidator()
        {
            RuleFor(s => s).SetValidator(new PersonCreateModelValidator());
            RuleFor(s => s.Year).InclusiveBetween((short)1, (short)3);
        }
    }
}
