using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListBehaviour<RecordT, ValueT> where RecordT : Behaviour, ISetupable<ValueT>
{
    public PoolComponent<RecordT> Pool { get; }
    public List<RecordT> ActiveItems { get; }

    public ListBehaviour(Transform parent, RecordT pattern)
    {
        Pool = new PoolComponent<RecordT>(pattern, parent);
        ActiveItems = new List<RecordT>();
    }

    public void SetAll(IEnumerable<ValueT> values)
    {
        foreach (var record in ActiveItems)
        {
            Pool.Return(record);
        }
        foreach (var value in values)
        {
            Add(value);
        }
    }

    public void Add(ValueT value)
    {
        var item = Pool.Get();
        item.transform.SetAsLastSibling();
        item.Setup(value);
        ActiveItems.Add(item);
    }
}
