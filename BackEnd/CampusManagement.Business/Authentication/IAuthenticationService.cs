using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CampusManagement.Business.Responses;

namespace CampusManagement.Business.Authentication
{
    public interface IAuthenticationService
    {
        Task<TokenResponse> CreateAccessTokenAsync(string email, string password);
        Task<TokenResponse> RefreshTokenAsync(string refreshToken, string personEmail);
        Task<IEnumerable<string>> GetRolesByPersonId(Guid personId);
        void RevokeRefreshToken(string refreshToken);
    }
}
