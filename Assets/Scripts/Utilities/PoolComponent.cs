using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolComponent<T> : Pool<T> where T : Component
{
    public T Prefab { get; }
    public Transform Parent { get; }

    public PoolComponent(T prefab, Transform parent = null)
    {
        Prefab = prefab;
        Parent = parent;
    }

    public override T Get()
    {
        var item = base.Get();
        item.gameObject.SetActive(true);
        return item;
    }

    public override void Return(T item)
    {
        base.Return(item);
        item.gameObject.SetActive(false);
    }

    public override T CreateNewItem()
    {
        var item = Object.Instantiate(Prefab);
        if (Parent != null)
        {
            item.transform.SetParent(Parent, false);
        }
        return item;
    }
}
