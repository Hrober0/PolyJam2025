using UnityEngine;

public class BubbleColorChanger : MonoBehaviour
{
    [SerializeField] private Renderer rend;

    private MaterialPropertyBlock props;

    private void Awake()
    {
        props = new();
        rend.GetPropertyBlock(props);
    }

    public void SetColor(Color color)
    {
        props.SetColor("_BaseColor", color);
        rend.SetPropertyBlock(props);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerColor playerColor))
        {
            SetColor(playerColor.Color);
        }
    }
}
