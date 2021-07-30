
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NStore.Carrinho.API.Services;
using NStore.Core.Utils;
using NStore.MessageBus.Extensions;

namespace NStore.Carrinho.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                .AddHostedService<CarrinhoIntegrationHandler>();
        }
    }
}
