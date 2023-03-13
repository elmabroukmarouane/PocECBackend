using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poc.Ecare.Server.Extensions.Add
{
    public static class AddSwagger
    {
        public static void AddSWAGGER(this IServiceCollection self)
        {
            self.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ecare GP POC", Version = "v1", Description = "C'est le POC Ecare Gp pour implémenter les idées de la refonte" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "En-tête d'autorisation JWT utilisant le schéma Bearer. \r\n\r\n Entrez 'Bearer' [espace] puis votre jeton dans la zone de texte ci-dessous.\r\n\r\nExemple : \"Bearer 12345abcdef\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}

                    }
                });
            });
        }
    }
}
