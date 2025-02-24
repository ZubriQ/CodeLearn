namespace CodeLearn.Api.Common;

public static class DateTimeExtensions
{
    public static string ToIso8601(this DateTime dateTime)
    {
        return dateTime.ToUniversalTime().ToString("u").Replace(" ", "T");
    }
}