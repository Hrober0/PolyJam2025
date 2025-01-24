using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public interface ISingletonDontDestroy { }
public interface ISingletonAutoCreate { }
public interface ISingletonAutoFind { }

public class SingletonMB<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null && typeof(ISingletonAutoFind).IsAssignableFrom(typeof(T)))
            {
                instance = FindFirstObjectByType<T>();
            }
            if (instance == null && typeof(ISingletonAutoCreate).IsAssignableFrom(typeof(T)))
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
        Assert.IsFalse(Instance && Instance != this);
        Instance = this as T;
        if (typeof(T) is ISingletonDontDestroy)
        {
            DontDestroyOnLoad(Instance);
        }
    }
}
