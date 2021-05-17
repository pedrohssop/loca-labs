using LocaLabs.Domain.Entities.Base;
using LocaLabs.Domain.Enums;
using System;
using System.Collections.Generic;

namespace LocaLabs.Domain.Entities
{
    public class User : Entity
    {
        public string Login { get; }
        public string Password { get; }
        public UserProfiles Type { get; }

        public User(string login, string password, UserProfiles type) : base() =>
            (Login, Password, Type) = (login, password, type);

        public User(Guid id, DateTime createdAt, string login, string password, UserProfiles type)
            : base(id, createdAt) => (Login, Password, Type) = (login, password, type);

        public override IEnumerable<EntityValidation> IsValid()
        {
            if (string.IsNullOrEmpty(Login))
                yield return new EntityValidation("Is required", nameof(Login));

            if (string.IsNullOrEmpty(Password))
                yield return new EntityValidation("Is required", nameof(Password));
        }
    }
}
