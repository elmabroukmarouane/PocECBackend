using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.UnitOfWork.Interface;
using Newtonsoft.Json;
using Poc.Ecare.Infrastructure.Models.Classes;
using Pro.Ecare.Business.Services.Interfaces;
using System.Security.Cryptography;
using Poc.Ecare.Business.Helpers;
using Pro.Ecare.Business.Cqrs.Queries.Interfaces;

namespace Poc.Ecare.Business.Services.Classe
{
    public class AuthenticationService : IAuthenticationService
    {
        #region ATTRIBUTE
        private readonly IGenericService<User> _genericService;
        #endregion

        #region CONTRUCTOR
        public AuthenticationService(IGenericService<User> genericQueryService)
        {
            _genericService = genericQueryService ?? throw new ArgumentException(nameof(genericQueryService));
        }
        #endregion

        #region METHODS
        public async Task<User> Authenticate(UserLogin UserLogin)
        {
            if (string.IsNullOrEmpty(UserLogin.Email) || string.IsNullOrEmpty(UserLogin.Password))
                return null;
            var AttempingUser = await _genericService.GetFirstOrDefaultTEntity(
                predicate: u => u.Email.Trim().ToLower().Equals(UserLogin.Email.Trim().ToLower()));
            if (AttempingUser == null)
            {
                return null;
            }
            var passwordHash = Helper.CreateHashPassword(UserLogin.Password.Trim());
            var passwordAuth = AttempingUser.Password;
            if (passwordAuth!.Equals(passwordHash))
            {
                AttempingUser.IsConnected = 1;
                AttempingUser = _genericService.UpdateTEntity(AttempingUser);
                return AttempingUser;
            }
            return null;
        }

        public async Task<bool> Logout(int id)
        {
            var LoggedUser = await _genericService.GetTEntityById(id);
            if (LoggedUser == null)
            {
                return false;
            }
            LoggedUser.IsConnected = 0;
            _genericService.UpdateTEntity(LoggedUser);
            return true;
        }

        public string CreateToken(User user, string keyString, string issuerString, string audienceString)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName + " " + user.LastName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(keyString));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                issuer: issuerString,
                audience: audienceString,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<int> GetConnectedUsers()
        {
            var ConnectedUsers = await _genericService.GetTEntitys(predicate: u => u.IsConnected == 1);
            return ConnectedUsers.Count;
        }
        #endregion
    }
}
