using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToolsController : MonoBehaviour
{
    public enum Tool
    {
        Blower,
    }

    [System.Serializable]
    public struct TypeAndValue<TKey, TValue>
    {
        public TKey Key;
        public TValue Value;
    }

    public event Action<Tool, bool> OnToolChanged;

    [SerializeField] private List<TypeAndValue<Tool, GameObject>> tools;

    public void SetTool(Tool tool, bool state)
    {
        tools.Find(t => t.Key == tool).Value.SetActive(state);
        OnToolChanged?.Invoke(tool, state);
    }
}
