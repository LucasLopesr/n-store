using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NStore.Cliente.API.Application.Commands;
using NStore.Cliente.API.Data;
using NStore.Cliente.API.Data.Repository;
using NStore.Cliente.API.Models;
using NStore.Core.Mediator;

namespace NStore.Cliente.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterService(this IServiceCollection services) 
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<RegistrarClienteCommand, ValidationResult>, ClienteCommandHandler>();

            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<ClientesContext>();
        }
    }
}
