using LocaLabs.Domain.Entities.Base;
using LocaLabs.Domain.Enums;
using LocaLabs.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace LocaLabs.Domain.Entities
{
    public class User : Entity
    {
        public string Password { get; }
        public UserProfiles Type { get; }
        public string Login { get; private set; }

        public User(string login, string password, UserProfiles type) : base() =>
            (Login, Password, Type) = (login, password, type);

        public User(Guid id, DateTime createdAt, string login, string password, UserProfiles type)
            : base(id, createdAt) => (Login, Password, Type) = (login, password, type);

        public override IEnumerable<EntityValidation> IsValid()
        {
            if (string.IsNullOrEmpty(Login) || string.IsNullOrWhiteSpace(Login))
                yield return new EntityValidation("Is required", nameof(Login));

            if (string.IsNullOrEmpty(Password) || string.IsNullOrWhiteSpace(Password))
                yield return new EntityValidation("Is required", nameof(Password));

            if (Type == UserProfiles.Client)
            {
                Cpf cpf = Login;
                if (!cpf.IsValid)
                    yield return new EntityValidation("Login CPF is invalid", nameof(Login));

                if (Login != cpf.Unformated)
                    Login = cpf.Unformated;
            }
        }
    }
}
