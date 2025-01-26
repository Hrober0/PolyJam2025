using UnityEngine;

public class BubbleColorChanger : MonoBehaviour
{
    [SerializeField] private Bubble bubble;
    [SerializeField] private Renderer rend;

    [SerializeField] private ParticleSystem parsys;

    private MaterialPropertyBlock props;

    private void Awake()
    {
        props = new();
        rend.GetPropertyBlock(props);
    }

    private void OnEnable()
    {
        bubble.OnPlayerChanged += UpdateColor;
        UpdateColor();
    }

    private void OnDisable()
    {
        bubble.OnPlayerChanged -= UpdateColor;
    }

    private void UpdateColor()
    {
        if (bubble.PlayerData != null)
        {
            SetColor(bubble.PlayerData.Value.color);
        }
    }

    private void SetColor(Color color)
    {
        props.SetColor("_BaseColor", color);
        rend.SetPropertyBlock(props);
        var x = parsys.main;
        x.startColor = color;
    }
}
