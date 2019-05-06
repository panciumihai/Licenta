﻿using CampusManagement.Business.Security;

namespace CampusManagement.Business.Responses
{
    public class TokenResponse : BaseResponse
    {
        public AccessToken Token { get; set; }

        public TokenResponse(bool success, string message, AccessToken token) 
            : base(success, message)
        {
            Token = token;
        }
    }
}
