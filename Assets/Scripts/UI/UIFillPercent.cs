using TMPro;
using UnityEngine;

public class UIFillPercent : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;

    private void OnEnable()
    {
        FloorPainter.Instance.OnFillPercentChange += UpdateScore;
        UpdateScore();
    }

    private void OnDisable()
    {
        FloorPainter.Instance.OnFillPercentChange -= UpdateScore;
    }

    private void UpdateScore()
    {
        label.text = $"Fill percent: {Mathf.RoundToInt(FloorPainter.Instance.FillPercent * 100)}%";
    }
}
