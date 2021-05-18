using LanguageExt;
using LocaLabs.Domain.ValueObjects;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LocaLabs.Domain.Entities
{
    public interface IClientRepository
    {
        /// <summary>
        /// Add a new client
        /// </summary>
        /// <param name="client">The client that will be saved</param>
        /// <returns>true if ok</returns>
        public ValueTask AddClient(Client client, CancellationToken token);

        public ValueTask<Option<Client>> FindByCpf(Cpf cpf, CancellationToken token);

        public ValueTask<Option<Client>> FindById(Guid id, CancellationToken token);
    }
}
