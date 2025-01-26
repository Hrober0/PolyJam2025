using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
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
        var scoresCopy = new Dictionary<int,int>(FloorPainter.Instance.playerScores);
        scoresCopy.OrderBy((kvp) => kvp.Value);
        while(scoresCopy.Count>0) {
            var record = scoresCopy.Last();
            scoresCopy.Remove(record.Key);
            if(record.Key == -1){
                continue;
            }
            label.text += $"\nPlayer {record.Key+1}'s score: {Mathf.RoundToInt(record.Value/(float)FloorPainter.Instance.txtValues.Length*100)}%";
        }
    }
}
