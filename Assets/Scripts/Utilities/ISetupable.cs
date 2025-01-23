using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISetupable<T>
{
    void Setup(T v);
}
