using LocaLabs.Domain.ValueObjects;
using Xunit;

namespace LocaLabs.Tests.Domain.ValueObjects
{
    [Trait("Domain Value Objects", "State")]
    public class StateCase
    {
        [Theory(DisplayName = "Is State Valid? ", Timeout = 10)]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("a", false)]
        [InlineData("hu", false)]
        [InlineData("bla bla bla", false)]
        [InlineData("mg", true)]
        [InlineData("BA", true)]
        [InlineData("Ms", true)]
        [InlineData("ceara", true)]
        [InlineData("RondôNia", true)]
        public void StateValidationCheck(string state, bool expected)
        {
            State _state = state;
            Assert.Equal(expected, _state.IsValid);
        }

        [Fact(DisplayName = "Check State Formater")]
        public void StateFormatCheck()
        {
            State the_best_place_of_the_world = "mg";

            Assert.Equal("MG", the_best_place_of_the_world.Initials);
            Assert.Equal("Minas Gerais", the_best_place_of_the_world.Name);
        }

        [Fact(DisplayName = "Check State Equals Comparer")]
        public void StateEquals()
        {
            State minas = "mg";
            State minasGerais = "minas GERAIS";

            Assert.True(minas == minasGerais);
        }

        [Fact(DisplayName = "Check State Not Equals Comparer")]
        public void StateNotEquals()
        {
            State minas = "mg";
            State bahia = "bahia";

            Assert.True(minas != bahia);
        }
    }
}
