﻿using LocaLabs.Application.Services;
using LocaLabs.Domain.Entities.Base;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LocaLabs.Infra.Data.Base
{
    /// <summary>
    /// Just mark all repositories for the injection
    /// </summary>
    public abstract class Repository
    { 
    }

    public abstract class BaseRepository<TEntity> : Repository where TEntity : Entity
    {
        protected DataContext Ctx { get; }
        protected INotificationService Notifier { get; }

        public BaseRepository(INotificationService notifier, DataContext ctx)
        {
            Ctx = ctx;
            Notifier = notifier;
        }

        protected bool CheckEntity(Entity entity)
        {
            var messages = entity.IsValid();
            if (!messages.Any())
            {
                Notifier.AddNotification(messages.ToArray());
                return true;
            }

            return false;
        }

        public async ValueTask SaveEntity(TEntity entity, CancellationToken token)
        {
            var set = Ctx.Set<TEntity>();

            await set.AddAsync(entity, token);
            await Ctx.SaveChangesAsync(token);

            entity.Persisted();
        }
    }
}