using System.Collections.Specialized;

namespace LocaLabs.Domain.ValueObjects
{
    public struct State
    {
        #region .:: State Names Lookup ::.

        private static readonly StringDictionary NamesToInitial =
            new StringDictionary
            {
                { "acre", "AC" },
                { "alagoas", "AL" },
                { "amapá", "AP" },
                { "amapa", "AP" },
                { "amazonas", "AM" },
                { "bahia", "BA" },
                { "ceará", "CE" },
                { "ceara", "CE" },
                { "espírito santo", "ES" },
                { "espirito santo", "ES" },
                { "goiás", "GO" },
                { "goias", "GO" },
                { "maranhão", "MA" },
                { "mato grosso", "MT" },
                { "mato grosso do sul", "MS" },
                { "minas gerais", "MG" },
                { "pará", "PA" },
                { "para", "PA" },
                { "paraíba", "PB" },
                { "paraiba", "PB" },
                { "paraná", "PR" },
                { "parana", "PR" },
                { "pernambuco", "PE" },
                { "piauí", "PI" },
                { "piaui", "PI" },
                { "rio de janeiro", "RJ" },
                { "rio grande do norte", "RN" },
                { "rio grande do sul", "RS" },
                { "rondônia", "RO" },
                { "rondonia", "RO" },
                { "roraima", "RR" },
                { "santa Catarina", "SC" },
                { "são paulo", "SP" },
                { "sao paulo", "SP" },
                { "sergipe", "SE" },
                { "tocantins", "TO" },
                { "distrito federal", "DF" },
            };

        private static readonly StringDictionary InitialsToName =
            new StringDictionary
            {
                { "AC", "Acre" },
                { "AL", "Alagoas" },
                { "AP", "Amapá" },
                { "AM", "Amazonas" },
                { "BA", "Bahia" },
                { "CE", "Ceará" },
                { "ES", "Espírito Santo" },
                { "GO", "Goiás" },
                { "MA", "Maranhão" },
                { "MT", "Mato Grosso" },
                { "MS", "Mato Grosso do Sul" },
                { "MG", "Minas Gerais" },
                { "PA", "Pará" },
                { "PB", "Paraíba" },
                { "PR", "Paraná" },
                { "PE", "Pernambuco" },
                { "PI", "Piauí" },
                { "RJ", "Rio de Janeiro" },
                { "RN", "Rio Grande do Norte" },
                { "RS", "Rio Grande do Sul" },
                { "RO", "Rondônia" },
                { "RR", "Roraima" },
                { "SC", "Santa Catarina" },
                { "SP", "São Paulo" },
                { "SE", "Sergipe" },
                { "TO", "Tocantins" },
                { "DF", "Distrito Federal" },
            };

        #endregion

        public string Name { get; }
        public bool IsValid { get; }
        public string Initials { get; }

        public State(string value)
        {
            Name = Initials = value ??= string.Empty;
            if (value.Length < 2)
                IsValid = false;

            else if (value.Length == 2)
            {
                Initials = value.ToUpper();
                IsValid = InitialsToName.ContainsKey(Initials);
                Name = IsValid ? InitialsToName[Initials] : string.Empty;
            }
            else
            {
                var aux = Name.ToLower();

                IsValid = NamesToInitial.ContainsKey(aux);
                Initials = IsValid ? NamesToInitial[aux] : string.Empty;
                Name = IsValid ? InitialsToName[Initials] : Name; 
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is not null && obj is State state)
                return this == state;

            return false;
        }

        public override int GetHashCode() =>
            ToString().GetHashCode();

        public static bool operator ==(State left, State right) =>
            left.Initials == right.Initials && left.Name == right.Name;

        public static bool operator !=(State left, State right) =>
            !(left == right);

        public override string ToString() =>
            $"{Name} ({Initials})";

        public static implicit operator State(string value) =>
            new State(value);
    }
}