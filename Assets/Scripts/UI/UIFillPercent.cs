using System.Collections.Generic;
using System.Linq;
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
        var scoresCopy = new Dictionary<int,int>(FloorPainter.Instance.playerScores);
        var scoresArr = scoresCopy.OrderBy((kvp) => -kvp.Value).ToArray();

        var dirt = 100-Mathf.RoundToInt(FloorPainter.Instance.FillPercent * 100);

        label.text = $"Dirt left: {dirt}%";

        var players = FindObjectsByType<Player>(FindObjectsSortMode.None).ToList();

        for(int i = 0; i<scoresArr.Length; i++){
            if(scoresArr[i].Key == -1){
                continue;
            }
            var col = players.Find((p) => p.Data.clientId==(ulong)scoresArr[i].Key).Data.color;
            label.text += $"\n<color=#{UnityEngine.ColorUtility.ToHtmlStringRGB(col)}>Player {scoresArr[i].Key+1}'s score: {Mathf.RoundToInt(scoresArr[i].Value/(float)FloorPainter.Instance.txtValues.Length*100)}%</color>";
        }
    }
}
