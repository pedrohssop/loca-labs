using LanguageExt;
using LocaLabs.Application.Services;
using LocaLabs.Domain.Entities;
using LocaLabs.Domain.Enums;
using LocaLabs.Domain.ValueObjects;
using LocaLabs.Infra.Data.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LocaLabs.Infra.Data.Entities.Users
{
    internal class UserRepository : BaseRepository<Domain.Entities.User>, IUserRepository
    {
        public UserRepository(INotificationService notifier, DataContext ctx)
            : base(notifier, ctx) { }

        public async ValueTask<Domain.Entities.User> AddClient(Cpf cpf, string password, CancellationToken token)
        {
            var newUser = new Domain.Entities.User(cpf.Unformated, password, UserProfiles.Client);
            if (CheckEntity(newUser))
                await SaveEntity(newUser, token);

            return newUser;
        }

        public async ValueTask<Domain.Entities.User> AddOperator(string login, string password, CancellationToken token)
        {
            var newUser = new Domain.Entities.User(login, password, UserProfiles.Operator);
            if (CheckEntity(newUser))
                await SaveEntity(newUser, token);

            return newUser;
        }

        public async ValueTask<Option<Domain.Entities.User>> FindUserByLogin(string login, string password, CancellationToken token) =>
            await Ctx.Users
                     .Where(w => w.Login == login && w.Password == password)
                     .FirstOrDefaultAsync(token);
    }
}