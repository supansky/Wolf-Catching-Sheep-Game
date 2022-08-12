using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    public static T Instance
    {
        get
        { 
            if (instance != null)
                return instance;
            T[] instances = FindObjectsOfType<T>();
            if (instances != null)
            {
                if (instances.Length == 1)
                {
                    instance = instances[0];
                    return instance;
                }
                else
                {
                    Debug.LogError("More than 1 instance of type " + typeof(T).Name);
                }
            }
            return null;
        }
    }
}
