public static class Extensions
{
	public static bool IsNull(this object obj)
	{
		return obj == null;
	}

	public static bool IsNotNull(this object obj)
	{
		return obj != null;
	}

    public static bool IsTypeof<T>(this object t)
    {
        return (t is T);
    }
}
