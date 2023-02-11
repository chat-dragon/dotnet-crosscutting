namespace CrossCutting.Domain;

public static class DefaultValues
{
    public static DateTime SqlServerDefaultDateTime
    {
        get { return new DateTime(1970, 1, 1); }
    }
}
