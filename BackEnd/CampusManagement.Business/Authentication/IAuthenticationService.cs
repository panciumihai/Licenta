using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CampusManagement.Business.Authentication.Models;

namespace CampusManagement.Business.Authentication
{
    public interface IAuthenticationService
    {
        Task<IEnumerable<string>> GetRolesByPersonId(Guid personId);
        Task<TokenDetailsModel> Authenticate(string email, string password);
    }
}
