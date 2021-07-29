using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Webapi.Entities;
using Webapi.TokenOperations.Models;

namespace Webapi.TokenOperations
{
    public class TokenHandler
    {

        public IConfiguration Configuration { get; set; }


        public TokenHandler(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Token CreateAccessToken(User user)
        {
            Token tokenModel = new();
            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));

            SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);

            tokenModel.Expiration = DateTime.Now.AddMinutes(15);

            JwtSecurityToken securityToken = new(
                issuer: Configuration["Token:Issuer"],
                audience: Configuration["Token:Audience"],
                expires: tokenModel.Expiration,
                notBefore: DateTime.Now,
                signingCredentials: credentials
            );

            JwtSecurityTokenHandler tokenHandler = new();

            //Token created
            tokenModel.AccessToken = tokenHandler.WriteToken(securityToken);
            tokenModel.RefreshToken = CreateRefreshToken();

            return tokenModel;


        }

        public string CreateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }

    }
}