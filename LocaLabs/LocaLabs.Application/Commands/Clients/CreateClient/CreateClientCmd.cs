using LocaLabs.Application.Commands.Base;
using LocaLabs.Domain.Entities;
using System;
using System.Collections.Generic;

namespace LocaLabs.Application.Commands.Clients.CreateClient
{
    public class CreateClientCmd : Command<Client>
    {
        public string Cpf { get; set; }
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public string Cep { get; set; }
        public short Number { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Street { get; set; }
        public string Complement { get; set; }

        internal override IEnumerable<string> Validate()
        {
            if (string.IsNullOrEmpty(Name))
                yield return "Name is required";

            if (string.IsNullOrEmpty(Cep))
                yield return "Cep is required";

            if (string.IsNullOrEmpty(State))
                yield return "State is required";

            if (string.IsNullOrEmpty(Cpf))
                yield return "Cpf is required";
        }
    }
}