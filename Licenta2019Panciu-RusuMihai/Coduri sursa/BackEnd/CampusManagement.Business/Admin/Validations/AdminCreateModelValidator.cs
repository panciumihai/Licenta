using CampusManagement.Business.Admin.Models;
using CampusManagement.Business.Person.Validations;
using FluentValidation;

namespace CampusManagement.Business.Admin.Validations
{
    public class AdminCreateModelValidator : AbstractValidator<AdminCreateModel>
    {
        public AdminCreateModelValidator()
        {
            RuleFor(s => s).SetValidator(new PersonCreateModelValidator());
        }
    }
}
