using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using CampusManagement.Business.Person.Models;

namespace CampusManagement.Business.Security
{
    public class AccessToken : JsonWebToken
    {
        public RefreshToken RefreshToken { get; private set; }

        public PersonDetailsModel Person { get; private set; }



        public AccessToken(string token, long expiration, PersonDetailsModel person, RefreshToken refreshToken) : base(token, expiration)
        {
            RefreshToken = refreshToken ?? throw new ArgumentException("Specify a valid refresh token.");
            Person = person;
        }
    }
}
