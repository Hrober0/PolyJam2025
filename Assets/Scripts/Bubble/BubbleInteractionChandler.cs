using UnityEngine;

public class BubbleInteractionChandler : MonoBehaviour
{
    [SerializeField] private Bubble bubble;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponentInParent<Player>();
        if (player != null)
        {
            bubble.SetPlayer(player.Data);
        }
    }
}
