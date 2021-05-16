using System;
using System.Linq;

#nullable enable
namespace LocaLabs.Domain.ValueObjects
{
    public struct Cep
    {
        private const char Dot = '.';
        private const char Trace = '-';

        private static readonly Cep InvalidRef =
            new Cep(string.Empty, string.Empty);

        private string _Formated { get; set; }
        public string Formated
        {
            get
            {
                if (string.IsNullOrEmpty(_Formated))
                {
                    if (string.IsNullOrEmpty(_Unformated))
                        return string.Empty;

                    _Formated = Format(_Unformated);
                }

                return _Formated;
            }
        }

        private string _Unformated { get; set; }
        public string Unformated
        {
            get
            {
                if (string.IsNullOrEmpty(_Unformated))
                {
                    if (string.IsNullOrEmpty(_Formated))
                        return string.Empty;

                    _Unformated = RemoveFormat(_Formated);
                }

                return _Unformated;
            }
        }

        private bool? _IsValid { get; set; }
        public bool IsValid
        {
            get
            {
                if (!_IsValid.HasValue)
                    _IsValid = Validate(Unformated);

                return _IsValid.Value;
            }
        }

        public static Cep From(string? value)
        {
            if (string.IsNullOrEmpty(value))
                return InvalidRef;

            if (value.Length == 8)
                return new Cep(value, string.Empty);

            if (value.Length == 10)
                return new Cep(string.Empty, value);

            return InvalidRef;
        }

        public Cep(string unformated, string formated)
        {
            _IsValid = null;
            _Formated = formated;
            _Unformated = unformated;

            if (string.IsNullOrEmpty(formated) 
            &&  string.IsNullOrEmpty(unformated))
                _IsValid = false;
        }

        private static bool Validate(string unformatedValue) =>
            unformatedValue.Length == 8 && unformatedValue.All(a => char.IsDigit(a));

        private static string Format(string unformated)
        {
            if (unformated.Length != 8)
                return string.Empty;

            Span<char> formated = stackalloc char[10];
            char replace = formated[0];

            formated[2] = Dot;
            formated[6] = Trace;

            for (int i = 0, j = 0; i < 10; i++)
                if (formated[i] == replace)
                    formated[i] = unformated[j++];

            return formated.ToString();
        }

        private static string RemoveFormat(string formated)
        {
            Span<char> unformated = stackalloc char[8];
            for (int i = 0, j = 0; i < 10; i++)
                if (char.IsDigit(formated[i]))
                    unformated[j++] = formated[i];

            return unformated.ToString();
        }

        public static implicit operator Cep(string value) =>
            From(value);

        public override bool Equals(object? obj)
        {
            if (obj is not null && obj is Cep cep)
                return Unformated == cep.Unformated;

            return false;
        }

        public static bool operator ==(Cep left, Cep right) =>
            left.Unformated == right.Unformated;

        public static bool operator !=(Cep left, Cep right) =>
            !(left == right);

        public override int GetHashCode() =>
            Unformated.GetHashCode();

        public override string ToString() =>
            Unformated.ToString();
    }
}