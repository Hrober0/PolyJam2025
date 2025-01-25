using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pool<T>
{
    protected readonly Stack<T> pool = new();

    public int Taken { get; protected set; }

    public virtual T Get()
    {
        if (!pool.TryPop(out var item))
            item = CreateNewItem();

        if (item is IPoolAutoReturn<T> autoReturn)
        {
            autoReturn.OnReturnToPool += Return;
        }

        Taken++;
        return item;
    }

    public virtual void Return(T item)
    {
        if (pool.Contains(item))
            return;

        pool.Push(item);

        if (item is IPoolAutoReturn<T> autoReturn)
        {
            autoReturn.OnReturnToPool -= Return;
        }

        Taken--;
    }

    public abstract T CreateNewItem();
}
