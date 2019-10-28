﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Shared;
using Shared.interfaces;

namespace BLLNetCore.Security
{
    public class GeneratorHelper : IGenerator
    {
        private IPassword passwordHelper = null;

        public GeneratorHelper(IPassword passwordHelper)
        {
            this.passwordHelper = passwordHelper;
        }

        public string GetJwtPrivateKey() 
        {
            // TODO - re test this after .net core 3 is installed
            //RSA rsa = new RSACryptoServiceProvider(2048); // Generate a new 2048 bit RSA key
            //string key = rsa.ToXmlString(true);

            //NOTE: Temporary key generated from NetClassicUtility...replace when .net core has 
            //      same functionality as .Net Classic
            string key = Credentials.GetJwtPrivateKey();

            return key;
        }

        public string GetJwtIssuer() 
        {
            var issuer = Credentials.GetJwtIssuer();

            return issuer;
        }

        public string GetJwtToken(string jwtPrivateKey, string jwtIssuer)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtPrivateKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();
            var token = new JwtSecurityToken(jwtIssuer,
              jwtIssuer,
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtSecurityTokenHandler.WriteToken(token);

            return jwtToken;
        }

        public bool IsValidUserToRegister(string user, string email, string password)
        {
            bool valid = true;

            if (string.IsNullOrEmpty(user) || user.Equals("null"))
                valid = false;
            else if (string.IsNullOrEmpty(email) || email.Equals("null"))
                valid = false;
            else if (string.IsNullOrEmpty(password) || password.Equals("null"))
                valid = false;
            else if (user.Length < Shared.Constants.REGISTRATION_VALUE_LENGTH)
                valid = false;
            else if (password.Length < Shared.Constants.REGISTRATION_VALUE_LENGTH)
                valid = false;
            else if (!this.passwordHelper.ContainsOneNumber(password))
                valid = false;
            else if (email.IndexOf(Shared.Constants.EMAIL_AT_SIGN) < 1)
                valid = false;

            return valid;
        }
    
        public string[] GetValidTokenResponse()
        {
            string[] result = null;

            result = new string[1];
            result[0] = Shared.Constants.TOKEN_VALID;

            return result;
        }

        public string[] GetInValidTokenResponse()
        {
            string[] result = null;

            result = new string[1];
            result[0] = Constants.TOKEN_IN_VALID;

            return result;
        }
    }
}
