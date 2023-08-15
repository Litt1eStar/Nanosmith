using System;

public static class TryCatchExtensions
{
    public static void TryCatch(this object obj, Action action)
    {
        try
        {
            action();
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public static T TryCatch<T>(this object obj, Func<T> func)
    {
        try
        {
            return func();
        }
        catch (Exception e)
        {
            throw e;
        }
    }
}
