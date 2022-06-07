namespace Kernel.Common.Extensions;

public static class DateExt
{
    public static bool IsExpired(this DateTime dateTime, DateTime expirationTime)
    {
        return dateTime >= expirationTime;
    }
}