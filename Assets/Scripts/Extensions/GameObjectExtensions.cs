using UnityEngine;

public static class GameObjectExtensions
{
    /// <summary>
    /// Checks if a GameObject has been destroyed.
    /// </summary>
    /// <param name="gameObject">GameObject reference to check for destructedness</param>
    /// <returns>If the game object has been marked as destroyed by UnityEngine</returns>
    public static bool IsDestroyed(this GameObject gameObject)
    {
        return gameObject.IsNull() && !ReferenceEquals(gameObject, null);
    }

    public static bool IsNotDestroyed(this GameObject gameObject)
    {
        return gameObject.IsNotNull() && !ReferenceEquals(gameObject, null);
    }
}
