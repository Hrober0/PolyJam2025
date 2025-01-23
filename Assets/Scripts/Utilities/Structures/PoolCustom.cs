using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolCustom<T> : Pool<T>
{
    private Func<T> creation;

    public PoolCustom(Func<T> creation)
    {
        this.creation = creation;
    }

    public override T CreateNewItem()
    {
        return creation();
    }
}
