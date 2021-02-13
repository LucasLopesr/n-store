using Microsoft.Extensions.DependencyInjection;
using System;

namespace NStore.MessageBus.Extensions
{
    public static class DependencyInjectionExtensons
    {
        public static IServiceCollection AddMessageBus(this IServiceCollection services, string connection) 
        {
            if (string.IsNullOrWhiteSpace(connection)) throw new ArgumentNullException(nameof(connection));

            services.AddSingleton<IMessageBus>(new MessageBus(connection));

            return services;
        }
    }
}
