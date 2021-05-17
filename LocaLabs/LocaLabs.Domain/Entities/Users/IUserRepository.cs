using LanguageExt;
using LocaLabs.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace LocaLabs.Domain.Entities
{
    public interface IUserRepository
    {
        ValueTask<User> AddClient(Cpf cpf, string password, CancellationToken token);
        ValueTask<User> AddOperator(string login, string password, CancellationToken token);
        ValueTask<Option<User>> FindUserByLogin(string login, string password, CancellationToken token);
    }
}