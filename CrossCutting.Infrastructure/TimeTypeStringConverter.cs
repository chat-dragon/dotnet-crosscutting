using System.Linq.Expressions;
using CrossCutting.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CrossCutting.Infrastructure
{
    public class TimeTypeStringConverter : ValueConverter<TimeType, string>
    {
        protected static readonly ConverterMappingHints _defaultHints = new ConverterMappingHints(size: 48);

        public TimeTypeStringConverter()
        : base(ToString(), ToTimeType(), _defaultHints)
        {
        }

        protected new static Expression<Func<TimeType, string>> ToString()
        => v => TimeTypeToString(v);

        protected static Expression<Func<string, TimeType>> ToTimeType()
            => v => StringToTimeType(v);

        private static string TimeTypeToString(TimeType timeType)
        {
            return timeType.ToString();
        }

        private static TimeType StringToTimeType(string timeType)
        {
            return timeType;
        }
    }
}

