using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using UnitOfWork.UnitOfWork.Classe;
using UnitOfWork.UnitOfWork.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Poc.Ecare.Infrastructure.Models.DatabaseContext;
using Pro.Ecare.Business.Services.Interfaces;
using Pro.Ecare.Business.Services.Classes;
using Poc.Ecare.Infrastructure.Models.Classes;
using Pro.Ecare.Business.Cqrs.Queries.Interfaces;
using Pro.Ecare.Business.Cqrs.Queries.Classes;
using Pro.Ecare.Business.Cqrs.Commands.Interfaces;
using Pro.Ecare.Business.Cqrs.Commands.Classes;
using Poc.Ecare.Business.Services.Classe;
using Ocelot.Values;
using Pro.Ecare.Business.RedisConnection.Classes;
using Pro.Ecare.Business.RedisConnection.Interfaces;
using Poc.Ecare.Server.RealTime.Hubs.Classe;
using Poc.Ecare.Server.RealTime.Hubs.Interface;

namespace Poc.Ecare.Server.Extensions.Add
{
    public static class AddServices
    {
        public static void AddSERVICES(this IServiceCollection self, IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            self.AddTransient<IUnitOfWork<DatabaseContext>, UnitOfWork<DatabaseContext>>();
            self.AddTransient<IGenericGetAllQuery<AccessTemp>, GenericGetAllQuery<AccessTemp>>();
            self.AddTransient<IGenericGetByIdQuery<AccessTemp>, GenericGetByIdQuery<AccessTemp>>();
            self.AddTransient<IGenericCreateCommand<AccessTemp>, GenericCreateCommand<AccessTemp>>();
            self.AddTransient<IGenericUpdateCommand<AccessTemp>, GenericUpdateCommand<AccessTemp>>();
            self.AddTransient<IGenericDeleteQuery<AccessTemp>, GenericDeleteQuery<AccessTemp>>();
            self.AddTransient<IGenericService<AccessTemp>, GenericService<AccessTemp>>();
            self.AddTransient<IGenericGetAllQuery<User>, GenericGetAllQuery<User>>();
            self.AddTransient<IGenericGetByIdQuery<User>, GenericGetByIdQuery<User>>();
            self.AddTransient<IGenericCreateCommand<User>, GenericCreateCommand<User>>();
            self.AddTransient<IGenericUpdateCommand<User>, GenericUpdateCommand<User>>();
            self.AddTransient<IGenericDeleteQuery<User>, GenericDeleteQuery<User>>();
            self.AddTransient<IGenericService<User>, GenericService<User>>();
            self.AddTransient<IAuthenticationService, AuthenticationService>();
            self.AddTransient<IRedisService, RedisService>();
            self.AddTransient<IRealTimeHub, RealTimeHub>();

            self.AddSingleton(configuration);
            self.AddSingleton(hostEnvironment);
            self.AddSingleton<IRedisConnectionFactory>(new RedisConnectionFactory(configuration.GetSection("Redis:Host").Value + ":" + configuration.GetSection("Redis:Port").Value));
        }
    }
}
