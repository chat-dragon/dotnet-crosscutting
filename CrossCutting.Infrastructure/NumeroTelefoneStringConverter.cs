using System.Linq.Expressions;
using CrossCutting.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CrossCutting.Infrastructure
{
    public class NumeroTelefoneStringConverter : ValueConverter<NumeroTelefone, string>
    {
        protected static readonly ConverterMappingHints _defaultHints
        = new ConverterMappingHints(size: 48);

        public NumeroTelefoneStringConverter()
        : base(ToString(), ToNumeroTelefone(), _defaultHints)
        {
        }

        protected new static Expression<Func<NumeroTelefone, string>> ToString()
        => v => NumeroTefoneToString(v);

        protected static Expression<Func<string, NumeroTelefone>> ToNumeroTelefone()
            => v => StringToNumeroTelefone(v);

        private static string NumeroTefoneToString(NumeroTelefone telefone)
        {
            return telefone.ToString();
        }

        private static NumeroTelefone StringToNumeroTelefone(string telefone)
        {
            return telefone;
        }
    }
}

