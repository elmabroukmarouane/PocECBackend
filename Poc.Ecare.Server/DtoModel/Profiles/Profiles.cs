using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Poc.Ecare.Infrastructure.Models.Classes;
using Poc.Ecare.Server.DtoModel.Models;

namespace Poc.Ecare.Server.DtoModel.Profiles
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<AccessTemp, AccessTempViewModel>();
            CreateMap<AccessTempViewModel, AccessTemp>();
            CreateMap<User, UserViewModel>();
            CreateMap<UserViewModel, User>();
        }
    }
}
