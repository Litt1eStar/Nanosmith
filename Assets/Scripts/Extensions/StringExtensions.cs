using System.Text.RegularExpressions;

public static class StringExtensions
{
    public static string UnEscapeString(this string str)
    {
        return Regex.Unescape(str);
    }
}
