using System;

public interface IPoolAutoReturn<T>
{
    event Action<T> OnReturnToPool;
}
