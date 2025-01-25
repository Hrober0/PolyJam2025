using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    [SerializeField] private Renderer rend;

    private MaterialPropertyBlock props;

    public Color Color => playerData.color;
    public int Id => (int)playerData.clientId;

    public void SetData(PlayerData playerData)
    {
        this.playerData = playerData;
        UpdateColor();
    }

    private void Start()
    {
        props = new();
        rend.GetPropertyBlock(props);
    }

    private void UpdateColor()
    {
        props.SetColor("_BaseColor", Color);
        rend.SetPropertyBlock(props, 1);
    }
}
