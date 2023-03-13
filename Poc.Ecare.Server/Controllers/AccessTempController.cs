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

namespace Poc.Ecare.Server.Controllers
{
    public class AccessTempController : GenericController<AccessTemp, AccessTempViewModel>
    {
        public AccessTempController(
            IGenericService<AccessTemp> genericService,
            IMapper mapper
            ) : base(genericService, 
                mapper) { }
    }
}
