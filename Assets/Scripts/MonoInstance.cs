using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoInstance<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    private static object _lock = new object();

    public static T Instance
    {
        get
        {
            lock (_lock)
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));

                    if (FindObjectOfType(typeof(T)))
                    {
                        Debug.LogError("[MonoSingleton] Something went really wrong" +
                                       " - there should never be more than 1 singleton!" +
                                       " Reopenning the scene might fix it");
                        return instance;
                    }

                    if (instance == null)
                    {
                        GameObject singleton = new GameObject();
                        instance = singleton.AddComponent<T>();
                        singleton.name = "(MonoSingleton)" + typeof(T).ToString();
                        Debug.Log("[MonoInstance]: '" + singleton.name + " has been build");                      
                    }
                }

                return instance;
            }
        }
    }

    protected virtual void OnDestroy()
    {
        instance = null;
    }

}
