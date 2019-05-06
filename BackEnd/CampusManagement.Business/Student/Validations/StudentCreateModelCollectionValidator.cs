using System.Collections.Generic;
using CampusManagement.Business.Student;
using CampusManagement.Business.Student.Models;
using FluentValidation;

namespace CampusManagement.Business.Validations
{
    public class StudentCreateModelCollectionValidator : AbstractValidator<IEnumerable<StudentCreateModel>>
    {
        public StudentCreateModelCollectionValidator()
        {
            RuleForEach(m => m).SetValidator(new StudentCreateModelValidator());
        }
    }
}
