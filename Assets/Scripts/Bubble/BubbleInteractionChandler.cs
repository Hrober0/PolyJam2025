using UnityEngine;

public class BubbleInteractionChandler : MonoBehaviour
{
    [SerializeField] private Bubble bubble;

    private void OnCollisionEnter(Collision collision)
    {
        var player = collision.collider.GetComponentInParent<Player>();
        if (player != null)
        {
            bubble.SetPlayer(player.Data);
        }
    }
}
