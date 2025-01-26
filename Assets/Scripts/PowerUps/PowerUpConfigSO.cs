using UnityEngine;

public abstract class PowerUpConfigSO : ScriptableObject
{
    public abstract void Apply(GameObject target);
}
