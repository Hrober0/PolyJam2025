using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class UIRumbaSelect : MonoBehaviour
{
    [SerializeField] private Slider redSlider;
    [SerializeField] private Slider greenSlider;
    [SerializeField] private Slider bluelider;
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
        bluelider.onValueChanged.AddListener((value) =>
        {
            UpdateColor(new Color(color.r, color.g, value));
        });

        GameNM.Instance.OnPlayerDataListChanged += UpdateClientsList;
        UpdateClientsList();
    }
    private void OnDestroy()
    {
        GameNM.Instance.OnPlayerDataListChanged -= UpdateClientsList;
    }

    private void UpdateClientsList()
    {
        
    }

    private void UpdateColor(Color color)
    {
        this.color = color;
        redSlider.value = color.r;
        greenSlider.value = color.g;
        bluelider.value = color.b;
    }
}
