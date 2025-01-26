using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class UIRumbaSelect : MonoBehaviour
{
    [SerializeField] private Slider redSlider;
    [SerializeField] private Slider greenSlider;
    [SerializeField] private Slider blueSlider;
    [SerializeField] private Button readyButton;

    private Color color;

    private void Start()
    {
        readyButton.onClick.AddListener(() =>
        {
            var playerData = GameNM.Instance.GetCurrentPlayerData();
            CharacterSelect.Instance.SetPlayerReadyServerRpc(
                !CharacterSelect.Instance.IsPlayerReady(playerData.clientId));
        });

        redSlider.onValueChanged.AddListener((value) =>
        {
            UpdateColor(new Color(value, color.g, color.b));
        });
        greenSlider.onValueChanged.AddListener((value) =>
        {
            UpdateColor(new Color(color.r, value, color.b));
        });
        blueSlider.onValueChanged.AddListener((value) =>
        {
            UpdateColor(new Color(color.r, color.g, value));
        });

        if (GameNM.Instance.IsCurrentPlayerDataSet())
        {
            UpdateSlider();
        }
        else
        {
            GameNM.Instance.OnPlayerDataListChanged += UpdateSlider;
        }
    }

    private void UpdateSlider()
    {
        GameNM.Instance.OnPlayerDataListChanged -= UpdateSlider;

        var playerData = GameNM.Instance.GetCurrentPlayerData();
        color = playerData.color;
        //Debug.Log($"{color.r} {color.g} {color.b}");
        redSlider.SetValueWithoutNotify(color.r);
        greenSlider.SetValueWithoutNotify(color.g);
        blueSlider.SetValueWithoutNotify(color.b);
    }

    private void UpdateColor(Color color)
    {
        this.color = color;
        GameNM.Instance.SetPlayerColorServerRpc(color);
    }
}
