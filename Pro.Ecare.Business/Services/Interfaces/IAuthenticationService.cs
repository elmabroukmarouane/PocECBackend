using Poc.Ecare.Infrastructure.Models.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pro.Ecare.Business.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<User> Authenticate(UserLogin UserLogin);
        Task<bool> Logout(int id);
        Task<int> GetConnectedUsers();
        string CreateToken(User user, string keyString, string issuerString, string audienceString);
    }
}
