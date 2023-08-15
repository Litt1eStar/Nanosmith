public static class IntExtension
{
    public static string ToCurrency(this int i)
    {
        return i.ToString("#,##0");
    }
}
