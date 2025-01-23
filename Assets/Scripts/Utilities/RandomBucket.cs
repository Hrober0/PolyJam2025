using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBucket<T>
{
    private readonly List<T> defaultValues;
    private readonly List<T> bucket = new();

    public RandomBucket(IEnumerable<T> defaultValues)
    {
        this.defaultValues = new(defaultValues);
    }

    public T Get()
    {
        if (bucket.Count == 0)
        {
            bucket.AddRange(defaultValues);
        }
        var index = Random.Range(0, bucket.Count);
        var item = bucket[index];
        bucket.RemoveAt(index);
        return item;
    }
}
