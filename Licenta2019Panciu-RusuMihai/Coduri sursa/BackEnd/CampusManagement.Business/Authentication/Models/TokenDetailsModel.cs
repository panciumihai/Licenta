using System;
using System.Collections.Generic;
using CampusManagement.Business.Person.Models;

namespace CampusManagement.Business.Authentication.Models
{
    public class TokenDetailsModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public PersonDetailsModel Person { get; set; }
        public IEnumerable<string> PersonRoles { get; set; }
        public long Expiration { get; set; }
    }
}
