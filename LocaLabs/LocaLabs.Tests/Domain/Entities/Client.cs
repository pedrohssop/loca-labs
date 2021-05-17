using FluentAssertions;
using LocaLabs.Domain.Entities;
using LocaLabs.Domain.Entities.Base;
using LocaLabs.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LocaLabs.Tests.Domain.Entities
{
    [Trait("Entities Validation", "Client")]
    public class ClientCase
    {
        private string ValidCpf ="049.315.570-82";

        private Address NiceAddress =
            new Address("31.150-000", "Avenida Bernardo Vasconcelos", 377, "Belo Horizonte", "MG");

        private void SingleValidationAssert(IEnumerable<EntityValidation> validations, string field)
        {
            validations.Should().HaveCount(1, "this test have just one error");
            validations.First().Field.Should().Be(field, "this is the test field");
        }

        [Fact(DisplayName = "Check Client CPF Check")]
        public void CheckUserCpfInvalid()
        {
            var client = new Client("110.357.435-71", "Name", DateTime.UtcNow, NiceAddress);
            SingleValidationAssert(client.IsValid(), nameof(client.Cpf));
        }

        [Fact(DisplayName = "Check Client Name Check")]
        public void CheckUserNameInvalid()
        {
            var client = new Client(ValidCpf, string.Empty, DateTime.UtcNow, NiceAddress);
            SingleValidationAssert(client.IsValid(), nameof(client.Name));
        }

        [Fact(DisplayName = "Check Client Number Check")]
        public void CheckUserAddressNumberInvalid()
        {
            var badAddress = NiceAddress with { Number = 0 };
            var client = new Client(ValidCpf, "Name", DateTime.UtcNow, badAddress);

            SingleValidationAssert(client.IsValid(), nameof(client.Number));
        }

        [Fact(DisplayName = "Check Client State Check")]
        public void CheckUserAddressStateInvalid()
        {
            var badAddress = NiceAddress with { State = "ZZ" };
            var client = new Client(ValidCpf, "Name", DateTime.UtcNow, badAddress);

            SingleValidationAssert(client.IsValid(), nameof(client.State));
        }

        [Fact(DisplayName = "Check Client Cep Check")]
        public void CheckUserAddressCepInvalid()
        {
            var badAddress = NiceAddress with { Cep = "31.150-000.87 " };
            var client = new Client(ValidCpf, "Name", DateTime.UtcNow, badAddress);

            SingleValidationAssert(client.IsValid(), nameof(client.Cep));
        }
    }
}