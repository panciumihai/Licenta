﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CampusManagement.Business.Generics;
using CampusManagement.Business.Student.Models;

namespace CampusManagement.Business.Student
{
    public interface IStudentService : 
        IDetailsService<StudentDetailsModel>, 
        ICreateService<StudentCreateModel>
    {
        //Task<IEnumerable<StudentDetailsModel>> FindAsync(StudentFilterModel filterModel);
        Task<StudentDetailsModel> GetStudentByPersonId(Guid id, params string[] includes);

        Task<Guid> Confirmation(StudentConfirmationModel studentConfirmationModel);
    }
}
