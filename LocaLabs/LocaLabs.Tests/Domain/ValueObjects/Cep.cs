using LocaLabs.Domain.ValueObjects;
using Xunit;

namespace LocaLabs.Tests.Domain.ValueObjects
{
    [Trait("Domain Value Objects", "CEP")]
    public class CepCase
    {
        [Theory(DisplayName = "Is Cep Valid? ", Timeout = 5)]
        [InlineData("123", false)]
        [InlineData("123123123", false)]
        [InlineData("blablabla", false)]
        [InlineData("31150000", true)]
        [InlineData("31.150-000", true)]
        public void CheckCepValidation(string cep, bool expectedValid)
        {
            Cep _cep = cep;
            Assert.Equal(_cep.IsValid, expectedValid);
        }

        [Fact(DisplayName = "Cep Equals")]
        public void CheckCepEquals()
        {
            Cep cep1 = "31150000";
            Cep cep2 = "31.150-000";

            Assert.True(cep1 == cep2);
        }

        [Fact(DisplayName = "Cep Not Equals")]
        public void CheckCepNotEquals()
        {
            Cep cep1 = "31150000";
            Cep cep2 = "31.610-420";

            Assert.True(cep1 != cep2);
        }

        [Fact(DisplayName = "Cep Not Equals")]
        public void CheckCepFormat()
        {
            var formated = "31.150-000";
            var unformated = "31150000";

            Cep cepFormated = formated;
            Cep cepUnformated = unformated;

            Assert.Equal(cepUnformated.Formated, formated);
            Assert.Equal(cepFormated.Unformated, unformated);
        }
    }
}
