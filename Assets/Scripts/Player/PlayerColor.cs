using UnityEngine;

public class PlayerColor : MonoBehaviour
{
    [SerializeField] private Color color = Color.white;

    [SerializeField] private Renderer rend;

    private MaterialPropertyBlock props;

    public Color Color => color;

    private void Start()
    {
        props = new();
        rend.GetPropertyBlock(props);
        props.SetColor("_BaseColor", color);
        rend.SetPropertyBlock(props, 1);
    }
}
