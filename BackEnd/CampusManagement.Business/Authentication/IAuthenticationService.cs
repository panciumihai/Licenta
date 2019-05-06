using System.Threading.Tasks;
using CampusManagement.Business.Responses;

namespace CampusManagement.Business.Authentication
{
    public interface IAuthenticationService
    {
        Task<TokenResponse> CreateAccessTokenAsync(string email, string password);
        Task<TokenResponse> RefreshTokenAsync(string refreshToken, string personEmail);
        void RevokeRefreshToken(string refreshToken);
    }
}
