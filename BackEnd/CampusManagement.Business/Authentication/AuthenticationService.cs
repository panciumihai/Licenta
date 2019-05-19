using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampusManagement.Business.Generics;
using CampusManagement.Business.Person;
using CampusManagement.Business.Responses;
using CampusManagement.Business.Security;

namespace CampusManagement.Business.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenHandler _tokenHandler;

        public AuthenticationService(IGenericRepository genericRepository, 
            IPasswordHasher passwordHasher, ITokenHandler tokenHandler)
        {
            _tokenHandler = tokenHandler;
            _passwordHasher = passwordHasher;
            _genericRepository = genericRepository;
        }

        public async Task<TokenResponse> CreateAccessTokenAsync(string email, string password)
        {
            //var person = await _personService.FindPersonByEmailAsync(email);
            var result = await _genericRepository.FindAsync<Domain.Entities.Person>(x => x.Email == email,
                "PersonRoles.Role");
            var person = result.FirstOrDefault();

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

            var result = await _genericRepository.FindAsync<Domain.Entities.Person>(x => x.Email == personEmail,
                "PersonRoles.Role");
            var person = result.FirstOrDefault();

            if (person == null)
                return new TokenResponse(false, "Invalid refresh token", null);

            var accesToken = _tokenHandler.CreateAccessToken(person);

            return new TokenResponse(true, null, accesToken);
        }

        public async Task<IEnumerable<string>> GetRolesByPersonId(Guid personId)
        {
            var result = await _genericRepository.FindAsync<Domain.Entities.Person>(x => x.Id == personId,
                "PersonRoles.Role");
            var person = result.FirstOrDefault();
            var roles = person.PersonRoles.Select(r=>r.Role.Name);

            return roles;
        }

        public void RevokeRefreshToken(string refreshToken)
        {
            _tokenHandler.RevokeRefreshToken(refreshToken);
        }
    }
}
