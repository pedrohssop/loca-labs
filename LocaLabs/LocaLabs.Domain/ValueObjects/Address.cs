namespace LocaLabs.Domain.ValueObjects
{
    public record Address(Cep Cep, string Street, short Number, string City, State State, string Complement = "")
    {
        public bool IsValid() =>
            Cep.IsValid
            && State.IsValid
            && !string.IsNullOrEmpty(Street)
            && Number != 0;
    }
}
