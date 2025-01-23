using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListBehavior<T> : IEnumerable where T : Behaviour
{
    public PoolComponent<T> Pool { get; }

    private readonly List<T> activeItems = new();

    public int Count => activeItems.Count;

    public ListBehavior(Transform parent, T pattern)
    {
        Pool = new PoolComponent<T>(pattern, parent);
    }


    public T this[int index] => activeItems[index];

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < Count; i++)
        {
            yield return activeItems[i];
        }
    }
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public T FindElement(Predicate<T> match) => activeItems.Find(match);
    public bool TryGetElement(Predicate<T> match, out T element)
    {
        element = activeItems.Find(match);
        return element != null;
    }

    /// <summary>
    /// Set active and return next element
    /// Create new if it is necessary
    /// </summary>
    public T ShowElement()
    {
        var item = Pool.Get();
        item.transform.SetAsLastSibling();
        activeItems.Add(item);

        return item;
    }

    public void SetElement(Action<T> setupMethod)
    {
        var element = ShowElement();
        setupMethod(element);
    }
    public void SetElement<TData>(TData data, Action<T, TData> setupMethod)
    {
        var element = ShowElement();
        setupMethod(element, data);
    }
    public void SetElements<TData>(IEnumerable<TData> data, Action<T, TData> setupMethod)
    {
        var numberOfVisableElements = 0;

        foreach (var d in data)
        {
            var element = numberOfVisableElements < activeItems.Count ? activeItems[numberOfVisableElements] : ShowElement();
            setupMethod(element, d);
            numberOfVisableElements++;
        }

        for (int i = activeItems.Count - 1; i >= numberOfVisableElements; i--)
        {
            Pool.Return(activeItems[i]);
            activeItems.RemoveAt(i);
        }
    }

    public void Clear()
    {
        for (int i = 0; i < activeItems.Count; i++)
        {
            Pool.Return(activeItems[i]);
        }
    }
}
