using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poc.Ecare.Server.Extensions.Use
{
    public static class UseCors
    {
        public static void UseCORS(this IApplicationBuilder self, IConfiguration configuration)
        {
            self.UseCors(configuration.GetSection("CorsName").Value);
        }
    }
}
