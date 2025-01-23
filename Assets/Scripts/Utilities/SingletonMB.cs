using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public interface ISingletonDontDestroy { }
public interface ISingletonAutoCreate { }

public class SingletonMB<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (typeof(T) is ISingletonAutoCreate)
            {
                var singleton = new GameObject();
                singleton.name = nameof(T);
                singleton.AddComponent<T>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        Assert.IsNull(Instance);
        Instance = this as T;
        if (typeof(T) is ISingletonDontDestroy)
        {
            DontDestroyOnLoad(Instance);
        }
    }
}
