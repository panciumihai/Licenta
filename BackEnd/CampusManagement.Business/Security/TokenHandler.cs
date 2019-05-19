using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using CampusManagement.Business.Person.Models;
using Microsoft.Extensions.Options;

namespace CampusManagement.Business.Security
{
    public class TokenHandler : ITokenHandler
    {
        private readonly ISet<RefreshToken> _refreshTokens = new HashSet<RefreshToken>();

        private readonly TokenOptions _tokenOptions;
        private readonly SigningConfigurations _signingConfigurations;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;

        public TokenHandler(IOptions<TokenOptions> tokenOptionsSnapshot, 
            SigningConfigurations signingConfigurations, IPasswordHasher passwordHasher, IMapper mapper)
        {
            _tokenOptions = tokenOptionsSnapshot.Value;
            _signingConfigurations = signingConfigurations;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        public AccessToken CreateAccessToken(Domain.Entities.Person person)
        {
            var refreshToken = BuildRefreshToken(person);
            var accessToken = BuildAccessToken(person, refreshToken);

            _refreshTokens.Add(refreshToken);

            return accessToken;
        }

        public RefreshToken TakeRefreshToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return null;

            var refreshToken = _refreshTokens.SingleOrDefault(t => t.Token == token);
            if (refreshToken != null)
                _refreshTokens.Remove(refreshToken);

            return refreshToken;
        }

        public void RevokeRefreshToken(string token)
        {
            TakeRefreshToken(token);
        }

        private RefreshToken BuildRefreshToken(Domain.Entities.Person person)
        {
            var refreshToken = new RefreshToken
                (
                    token: _passwordHasher.HashPassword(Guid.NewGuid().ToString()),
                    expiration: DateTime.UtcNow.AddSeconds(_tokenOptions.RefreshTokenExpiration).Ticks
                );

            return refreshToken;
        }

        private AccessToken BuildAccessToken(Domain.Entities.Person person, RefreshToken refreshToken)
        {
            var accessTokenExpiration = DateTime.UtcNow.AddSeconds(_tokenOptions.AccessTokenExpiration);

            var securityToken = new JwtSecurityToken(
                issuer:_tokenOptions.Issuer,
                audience:_tokenOptions.Audience,
                claims: GetClaims(person),
                expires: accessTokenExpiration,
                notBefore: DateTime.UtcNow,
                signingCredentials:_signingConfigurations.SigningCredentials
                );

            var handler = new JwtSecurityTokenHandler();
            var accessToken = handler.WriteToken(securityToken);

            return new AccessToken(accessToken, accessTokenExpiration.Ticks, 
                _mapper.Map<PersonDetailsModel>(person), refreshToken);
        }

        private IEnumerable<Claim> GetClaims(Domain.Entities.Person person)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, person.Email),
                new Claim("PersonId", person.Id.ToString())
            };

            foreach (var personRole in person.PersonRoles)
            {
                claims.Add(
                    new Claim(ClaimTypes.Role, personRole.Role.Name)
                    );
            }

            return claims;
        }
    }
}
