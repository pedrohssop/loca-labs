using LocaLabs.Domain.ValueObjects;
using Xunit;

namespace LocaLabs.Tests.Domain.ValueObjects
{
    [Trait("Domain Value Objects", "CPF")]
    public class CpfCase
    {
        [Theory(DisplayName = "Is CPF valid? ", Timeout = 50)]

        // CPF teoricamente validos... mas invalidos...
        [InlineData("11111111111", false)]
        [InlineData("22222222222", false)]
        [InlineData("33333333333", false)]
        [InlineData("44444444444", false)]
        [InlineData("55555555555", false)]
        [InlineData("66666666666", false)]
        [InlineData("88888888888", false)]
        [InlineData("99999999999", false)]
        [InlineData("00000000000", false)]

        // Invalidos por tamanho
        [InlineData("1", false)]
        [InlineData("123", false)]
        [InlineData("123456", false)]
        [InlineData("123456678", false)]
        [InlineData("123456789", false)]
        [InlineData("123456789123", false)]
        [InlineData("123.456.789.123", false)]

        // Invalidos por criatividade :)
        [InlineData("blablablaba", false)]
        [InlineData("asd87as98d7", false)]
        [InlineData("123.123.123", false)]

        // Invalidos por calculo
        [InlineData("246.784.440-48", false)]
        [InlineData("689.384.640-17", false)]
        [InlineData("123.005.730-67", false)]

        // Validos
        [InlineData("246.534.440-48", true)]
        [InlineData("689.384.640-16", true)]
        [InlineData("457.005.730-67", true)]
        public void ValidCheckCpf(string cpf, bool validExpected)
        {
            Cpf _cpf = cpf;
            Assert.Equal(validExpected, _cpf.IsValid);
        }
        
        [Fact(DisplayName = "Cpf equals operator")]
        public void CpfEquals()
        {
            Cpf cpf1 = "110.357.466.35";
            Cpf cpf2 = "11035746635";

            Assert.True(cpf1 == cpf2);
        }

        [Fact(DisplayName = "Cpf not equals operator")]
        public void CpfNotEquals()
        {
            Cpf cpf1 = "110.357.466.35";
            Cpf cpf2 = "11035746641";

            Assert.True(cpf1 != cpf2);
        }

        [Fact(DisplayName = "Cpf formatter and unformatter")]
        public void CpfFormat()
        {
            var unformated = "11035746641";
            var formated = "110.357.466-41";

            Cpf cpfFormated = formated;
            Cpf cpfUnformated = unformated;

            Assert.Equal(cpfUnformated.Formated, formated);
            Assert.Equal(cpfFormated.Unformated, unformated);
        }
    }
}