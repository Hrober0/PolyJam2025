using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    void Start()
    {
        GameObject.DontDestroyOnLoad(gameObject);
    }
}
