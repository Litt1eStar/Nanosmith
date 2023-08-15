using System.Collections;
using System.Collections.Generic;

public static class IsNullOrEmptyExtension
{
    public static bool IsNullOrEmpty(this IEnumerable source)
    {
        if (source != null)
        {
            foreach (object obj in source)
            {
                return false;
            }
        }
        return true;
    }

    public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
    {
        if (source != null)
        {
            foreach (T obj in source)
            {
                return false;
            }
        }
        return true;
    }

    public static bool IsNotNullOrEmpty(this IEnumerable source)
    {
        return !IsNullOrEmpty(source);
    }

    public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> source)
    {
        return !IsNullOrEmpty<T>(source);
    }
}