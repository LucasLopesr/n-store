using Microsoft.EntityFrameworkCore;
using NStore.Core.DomainObjects;
using NStore.Core.Mediator;
using System.Linq;
using System.Threading.Tasks;

namespace NStore.Cliente.API.Extensions
{
    public static class MediatorExtension
    {
        public static async Task PublicarEventos<T>(this IMediatorHandler mediatorHandler, T context) where T : DbContext
        {
            var domainEntities = context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Notificacoes != null && x.Entity.Notificacoes.Any());

            var domainEvents = domainEntities.SelectMany(e => e.Entity.Notificacoes).ToList();
            domainEntities.ToList().ForEach(entity => entity.Entity.LimparEventos());

            var tasks = domainEvents.Select(async (domainEvent) =>
            {
                await mediatorHandler.PublicarEvento(domainEvent);
            });

            await Task.WhenAll(tasks);

        }
    }
}
