using Pro.Ecare.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Poc.Ecare.Infrastructure.Models.Classes;
using Poc.Ecare.Server.GenericController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Poc.Ecare.Server.DtoModel.Models;
using AutoMapper;
using Pro.Ecare.Business.Cqrs.Commands.Interfaces;
using Pro.Ecare.Business.Cqrs.Queries.Interfaces;
using Poc.Ecare.Server.RealTime.Hubs.Interface;

namespace Poc.Ecare.Server.Controllers
{
    public class UserController : GenericController<User, UserViewModel>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IRealTimeHub _realTimeHub;
        public UserController(
            IGenericService<User> genericService,
            IMapper mapper,
            IAuthenticationService authenticationService,
            IRealTimeHub realTimeHub
            ) : base(genericService, 
                mapper) 
        {
            _authenticationService = authenticationService ?? throw new ArgumentException(nameof(authenticationService));
            _realTimeHub = realTimeHub ?? throw new ArgumentException(nameof(_realTimeHub));
        }

        public async Task GetConnectedUsers()
        {
            var ConnectedUsersCount = await _authenticationService.GetConnectedUsers();
            await _realTimeHub.GetConnectedUsersList(ConnectedUsersCount);
        }
    }
}
