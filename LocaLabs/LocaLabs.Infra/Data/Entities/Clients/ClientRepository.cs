using LanguageExt;
using LocaLabs.Application.Services;
using LocaLabs.Domain.Entities;
using LocaLabs.Domain.ValueObjects;
using LocaLabs.Infra.Data.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LocaLabs.Infra.Data.Entities.Clients
{
    internal class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(INotificationService notifier, DataContext ctx)
            : base(notifier, ctx) { }

        public async ValueTask AddClient(Client client, CancellationToken token)
        {
            if (CheckEntity(client))
                await SaveEntity(client, token);
        }

        public async ValueTask<Option<Client>> FindByCpf(Cpf cpf, CancellationToken token)
        {
            if (!cpf.IsValid)
                return Option<Client>.None;

            return await Ctx.Clients
                .Where(w => w.Cpf == cpf.Unformated)
                .FirstOrDefaultAsync(token);
        }
    }
}