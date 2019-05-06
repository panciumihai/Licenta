using System.Threading.Tasks;
using CampusManagement.Business.Person;
using CampusManagement.Business.Responses;
using CampusManagement.Business.Security;

namespace CampusManagement.Business.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IPersonService _personService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenHandler _tokenHandler;

        public AuthenticationService(IPersonService personService, 
            IPasswordHasher passwordHasher, ITokenHandler tokenHandler)
        {
            _tokenHandler = tokenHandler;
            _passwordHasher = passwordHasher;
            _personService = personService;
        }

        public async Task<TokenResponse> CreateAccessTokenAsync(string email, string password)
        {
            var person = await _personService.FindPersonByEmailAsync(email);

            if (person == null || !_passwordHasher.PasswordMatches(password, person.Password))
                return new TokenResponse(false, "Invalid email or password!", null);

            var token = _tokenHandler.CreateAccessToken(person);

            return new TokenResponse(true, null, token);
        }

        public async Task<TokenResponse> RefreshTokenAsync(string refreshToken, string personEmail)
        {
            var token = _tokenHandler.TakeRefreshToken(refreshToken);

            if (token == null)
                return new TokenResponse(false, "Invalid refresh token.", null);

            if(token.IsExpired())
                return new TokenResponse(false, "Expired refresh token.", null);

            var person = await _personService.FindPersonByEmailAsync(personEmail);
            if(person == null)
                return new TokenResponse(false, "Invalid refresh token", null);

            var accesToken = _tokenHandler.CreateAccessToken(person);

            return new TokenResponse(true, null, accesToken);
        }

        public void RevokeRefreshToken(string refreshToken)
        {
            _tokenHandler.RevokeRefreshToken(refreshToken);
        }
    }
}
