using Unity.Netcode;
using UnityEngine;

public class PlayerDataNetworkBinder : NetworkBehaviour
{
    [SerializeField] private Player player;

    public override void OnNetworkSpawn()
    {
        var playerData = GameNM.Instance.GetPlayerDataById(OwnerClientId);
        player.SetData(playerData);
    }
}
