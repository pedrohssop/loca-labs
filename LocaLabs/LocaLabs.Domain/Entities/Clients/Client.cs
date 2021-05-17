using LocaLabs.Domain.Constants;
using LocaLabs.Domain.Entities.Base;
using LocaLabs.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace LocaLabs.Domain.Entities
{
    public class Client : Entity
    {
        public Cpf Cpf { get; }
        public string Name { get; }
        public DateTime Dob { get; }

        public Cep Cep { get; }
        public int Number { get; }
        public string City { get; }
        public State State { get; }
        public string Street { get; }
        public string Complement { get; }

        public Client(Cpf cpf, string name, DateTime dob, Address address) : base()
        {
            Cpf = cpf;
            Dob = dob;
            Name = name;
            Cep = address.Cep;
            City = address.City;
            State = address.State;
            Number = address.Number;
            Street = address.Street;
            Complement = address.Complement;
        }

        public Client(Guid id, DateTime createdAt, 
            Cpf cpf, string name, DateTime dob,
            Cep cep, int number, string city, State state, string street, string complement) : base(id, createdAt) =>
            (Cpf, Name, Dob, Cep, Number, City, State, Street, Complement) = (cpf, name, dob, cep, number, city, state, street, complement);

        public override IEnumerable<EntityValidation> IsValid()
        {
            if (!Cpf.IsValid)
                yield return new EntityValidation(EntityValidations.Invalid, nameof(Cpf));

            if (string.IsNullOrEmpty(Name))
                yield return new EntityValidation(EntityValidations.Required, nameof(Name));

            if (!Cep.IsValid)
                yield return new EntityValidation(EntityValidations.Invalid, nameof(Cep));

            if (Number == 0)
                yield return new EntityValidation(EntityValidations.Required, nameof(Number));

            if (!State.IsValid)
                yield return new EntityValidation(EntityValidations.Invalid, nameof(State));
        }
    }
}
