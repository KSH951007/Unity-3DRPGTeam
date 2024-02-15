using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    protected static T instance;
    public static T Instance { get { return instance; } }

    protected bool Init()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return false;
        }
        instance = (T)this;
        DontDestroyOnLoad(gameObject);

        return true;
    }
}
