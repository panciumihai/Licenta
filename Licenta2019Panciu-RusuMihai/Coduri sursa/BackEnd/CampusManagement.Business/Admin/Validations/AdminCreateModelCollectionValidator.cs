using System.Collections.Generic;
using CampusManagement.Business.Admin.Models;
using FluentValidation;

namespace CampusManagement.Business.Admin.Validations
{
    public class AdminCreateModelCollectionValidator : AbstractValidator<IEnumerable<AdminCreateModel>>
    {
        public AdminCreateModelCollectionValidator()
        {
            RuleForEach(m => m).SetValidator(new AdminCreateModelValidator());
        }
    }
}
