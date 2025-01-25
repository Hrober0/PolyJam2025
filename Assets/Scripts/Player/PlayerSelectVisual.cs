using UnityEngine;

public class PlayerSelectVisual : MonoBehaviour
{
    [SerializeField] private int playerIndex = 0;
    [SerializeField] private Player player;
    [SerializeField] private GameObject readyObject;

    private void Start()
    {
        GameNM.Instance.OnPlayerDataListChanged += UpdateVisual;
        CharacterSelect.Instance.OnPlayerReadyChange += UpdateVisual;
        UpdateVisual();
    }
    private void OnDestroy()
    {
        GameNM.Instance.OnPlayerDataListChanged -= UpdateVisual;
        CharacterSelect.Instance.OnPlayerReadyChange -= UpdateVisual;
    }

    private void UpdateVisual()
    {
        if (!GameNM.Instance.IsPlayerConnected(playerIndex))
        {
            player.gameObject.SetActive(false);
            return;
        }
        player.gameObject.SetActive(true);
        var playerData = GameNM.Instance.GetPlayerData(playerIndex);
        readyObject.SetActive(CharacterSelect.Instance.IsPlayerReady(playerData.clientId));
        player.SetData(playerData);
    }
}
