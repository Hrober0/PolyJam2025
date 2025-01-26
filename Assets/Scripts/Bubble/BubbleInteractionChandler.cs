using UnityEngine;

public class BubbleInteractionChandler : MonoBehaviour
{
    [SerializeField] private Bubble bubble;

    //private void OnTriggerEnter(Collider other)
    //{
    //    var player = other.GetComponentInParent<Player>();
    //    if (player != null)
    //    {
    //        Debug.Log("huj2");
    //        bubble.SetPlayer(player.Data);
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        var player = collision.collider.GetComponentInParent<Player>();
        if (player != null)
        {
            Debug.Log("huj");
            bubble.SetPlayer(player.Data);
        }
    }
}
