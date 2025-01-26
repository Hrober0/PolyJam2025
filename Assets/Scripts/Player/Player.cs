using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    [SerializeField] private Renderer rend;

    private MaterialPropertyBlock props;

    public PlayerData Data => playerData;

    public void SetData(PlayerData playerData)
    {
        this.playerData = playerData;
        UpdateColor();
    }

    private void Awake()
    {
        props = new();
        rend.GetPropertyBlock(props);
        UpdateColor();
    }

    private void UpdateColor()
    {
        props.SetColor("_BaseColor", playerData.color);
        rend.SetPropertyBlock(props, 1);
    }
}
