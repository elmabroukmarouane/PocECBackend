using Microsoft.Extensions.Configuration;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.IO;
using Pro.Ecare.Business.Services.Interfaces;
using Moq;
using Poc.Ecare.Infrastructure.Models.Classes;
using Poc.Ecare.Business.Services.Classe;

namespace Poc.Ecare.Server.Test.DatabaseConnectionTests
{
    public class AuthenticationServiceTest
    {
        protected readonly IAuthenticationService _authenticationService;
        protected readonly Mock<IGenericService<User>> _mockGenericService;
        public AuthenticationServiceTest()
        {
            _mockGenericService = new Mock<IGenericService<User>>();
            _authenticationService = new AuthenticationService(_mockGenericService.Object);
        }

        //[Fact]
        //public async Task Authentication_ShouldAthenticateUser_WhenUserIsRegisteredInDatabaseAsync()
        //{
        //    // Arrange 
        //    var userLogin = new UserLogin()
        //    {
        //        Email = "user1@mail.com",
        //        Password = "123456"
        //    };
        //    var user = new User()
        //    {
        //        UserId = 1,
        //        FirstName = "FirstName1",
        //        LastName = "LastName 1",
        //        Email = "user1@mail.com",
        //        Password = "ba3253876aed6bc22d4a6ff53d8406c6ad864195ed144ab5c87621b6c233b548baeae6956df346ec8c17f5ea10f35ee3cbc514797ed7ddd3145464e2a0bab413",
        //        IsConnected = 0
        //    };
        //    _mockGenericService.Setup(pl => pl.GetFirstOrDefaultTEntity(u => u.Email.ToLower() == userLogin.Email.ToLower(), "", true)).ReturnsAsync(user);

        //    // Act
        //    var userLogged = await _authenticationService.Authenticate(userLogin);

        //    // Assert
        //    Assert.Equal(user, userLogged);
        //}
    }
}
