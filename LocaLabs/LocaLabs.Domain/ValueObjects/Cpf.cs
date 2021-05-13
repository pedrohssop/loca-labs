#nullable enable
using System;

namespace LocaLabs.Domain.ValueObjects
{
    public struct Cpf
    {
        const char Dot = '.';
        const char Trace = '-';

        private static readonly Cpf InvalidRef =
            new Cpf(string.Empty, string.Empty);

        public static Cpf From(string? value)
        {
            if (!string.IsNullOrEmpty(value) 
            && (value.Length == 11 || value.Length == 14))
            {
                // Fomated CPF: xxx.xxx.xxx-xx
                if (value.Length == 14)
                     return new Cpf(string.Empty, value);

                // Unformated: xxxxxxxxxxx
                else return new Cpf(value, string.Empty);
            }

            return InvalidRef;
        }

        private Cpf(string unformated, string formated, bool? valid = null)
        {
            LazyValid = valid;
            LazyFormated = formated;
            LazyUnformated = unformated;
        }

        private bool? LazyValid { get; set; }
        public bool IsValid
        {
            get
            {
                if (!LazyValid.HasValue)
                {
                    if (string.IsNullOrEmpty(Unformated))
                         LazyValid = false;
                    else LazyValid = Validate(Unformated);
                }

                return LazyValid.Value;
            }
        }

        private string LazyFormated { get; set; }
        public string Formated 
        { 
            get
            {
                if (string.IsNullOrEmpty(LazyFormated))
                {
                    if (string.IsNullOrEmpty(LazyUnformated))
                        return string.Empty;

                    LazyFormated = Format(LazyUnformated);
                }

                return LazyFormated;
            }
        }

        private string LazyUnformated { get; set; }
        public string Unformated 
        { 
            get
            {
                if (string.IsNullOrEmpty(LazyUnformated))
                {
                    if (string.IsNullOrEmpty(LazyFormated))
                        return string.Empty;

                    LazyUnformated = RemoveFormatters(LazyFormated);
                }

                return LazyUnformated;
            }
        }

        /// <summary>
        /// Checa se o CPF é valido de acordo com as leis brazileiras
        /// </summary>
        /// <remarks>
        /// ! Codigo retirado do blog da EximiaCO, por sinal, artigo muito bom !
        /// </remarks>
        /// <see cref="https://www.eximiaco.tech/pt/2019/06/11/validacao-de-cpf/"/>
        /// <param name="value">apenas numeros do cpf</param>
        private static bool Validate(string value)
        {
            var dv1 = 0;
            var dv2 = 0;
            var posicao = 0;
            var totalDigito1 = 0;
            var totalDigito2 = 0;
            var ultimoDigito = -1;
            var digitosIdenticos = true;

            foreach (var c in value)
                if (char.IsDigit(c))
                {
                    var digito = c - '0';
                    if (posicao != 0 && ultimoDigito != digito)
                        digitosIdenticos = false;

                    ultimoDigito = digito;
                    if (posicao < 9)
                    {
                        totalDigito1 += digito * (10 - posicao);
                        totalDigito2 += digito * (11 - posicao);
                    }

                    else if (posicao == 9)
                        dv1 = digito;

                    else if (posicao == 10)
                        dv2 = digito;

                    posicao++;
                }

            if (posicao > 11 || digitosIdenticos)
                return false;

            var digito1 = totalDigito1 % 11;
                digito1 = digito1 < 2 ? 0 : 11 - digito1;

            if (dv1 != digito1)
                return false;

            totalDigito2 += digito1 * 2;
            var digito2 = totalDigito2 % 11;
                digito2 = digito2 < 2 ? 0 : 11 - digito2;

            return dv2 == digito2;
        }
        
        private static string Format(string value)
        {
            Span<char> formated = stackalloc char[14];

            formated[11] = Trace;
            formated[3] = formated[7] = Dot;

            for (int i = 0, j = 0; i < 14; i++)
                if (formated[i] != default(char))
                    formated[i] = value[j++]; 

            return formated.ToString();
        }

        private static string RemoveFormatters(string value)
        {
            Span<char> unformated = stackalloc char[11];
            for (int i = 0, j = 0; i < 14; i++)
                if (char.IsDigit(value[i]))
                    unformated[j++] = value[i];

            return unformated.ToString();
        }

        public static implicit operator Cpf(string value)
            => From(value);

        public override bool Equals(object? obj)
        {
            if (obj is Cpf cpf)
                return cpf.Unformated == Unformated;

            return false;
        }

        public override int GetHashCode() =>
            Unformated.GetHashCode();

        public override string ToString() =>
            Unformated.ToString();

        public static bool operator ==(Cpf left, Cpf right) =>
            left.Unformated == right.Unformated;

        public static bool operator !=(Cpf left, Cpf right) =>
            !(left == right);
    }
}