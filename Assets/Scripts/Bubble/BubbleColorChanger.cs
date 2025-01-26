using UnityEngine;

public class BubbleColorChanger : MonoBehaviour
{
    [SerializeField] private Bubble bubble;
    [SerializeField] private Renderer rend;

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
            var cOL = gameObject.transform.GetChild(0).GetComponent<ParticleSystem>().colorOverLifetime;
            cOL.color = bubble.PlayerData.Value.color;
        }
    }

    private void SetColor(Color color)
    {
        props.SetColor("_BaseColor", color);
        rend.SetPropertyBlock(props);
    }
}
