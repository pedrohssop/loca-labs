using LocaLabs.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LocaLabs.Api.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// App Secret Key
        /// </summary>
        byte[] Key { get; }

        /// <summary>
        /// Generate an user authentication token
        /// </summary>
        /// <param name="user">the user</param>
        /// <returns>token</returns>
        string GenerateToken(User user);
    }

    public class AuthService : IAuthService
    {
        private const string SecretKey = "Auth:Secret";
        private const string ExpiresKey = "Auth:ExpiresInMinutes";

        private IConfiguration Config { get; }

        public AuthService(IConfiguration config)
        {
            Config = config;
        }

        private byte[] AppSecret { get; set; }
        public byte[] Key
        {
            get
            {
                if (AppSecret is null)
                {
                    var secret = Config[SecretKey];
                    return Encoding.UTF8.GetBytes(secret);
                }

                return AppSecret;
            }
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(GetTokenConfig(user));

            return tokenHandler.WriteToken(token);
        }

        private SecurityTokenDescriptor GetTokenConfig(User user)
        {
            var minutesExpire = Config[ExpiresKey];

            // TODO: add checks

            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Login),
                    new Claim(ClaimTypes.Role, user.Type.ToString().ToLower()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(minutesExpire)),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256)
            };
        }
    }
}