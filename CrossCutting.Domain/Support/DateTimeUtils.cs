
namespace CrossCutting.Domain.Support;

public static class DateTimeUtils
{
    public static DateOnly Today { get => DateOnly.FromDateTime(DateTime.Now); }
    public static DateTime SetKindUtc(this DateTime dateTime)
    {
        if (dateTime.Kind == DateTimeKind.Utc) { return dateTime; }
        return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
    }
}

