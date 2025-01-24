using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int playerId = 1;
    [SerializeField] private Color color = Color.white;

    [SerializeField] private Renderer rend;

    private MaterialPropertyBlock props;

    public Color Color => color;
    public int Id => playerId;

    private void Start()
    {
        props = new();
        rend.GetPropertyBlock(props);
        props.SetColor("_BaseColor", color);
        rend.SetPropertyBlock(props, 1);
    }
}
