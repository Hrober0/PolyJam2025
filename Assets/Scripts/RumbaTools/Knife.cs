using UnityEngine;

public class Knife : MonoBehaviour
{
    public void Hit(Player player)
    {
        var newKnife = Instantiate(gameObject);
        newKnife.GetComponentInChildren<Collider>().enabled = false;
        newKnife.transform.parent = player.transform;
        newKnife.transform.SetPositionAndRotation(
            transform.position + (player.transform.position - newKnife.transform.position).normalized * 0.5f + Vector3.down * 0.1f,
            transform.rotation
            );
        gameObject.SetActive(false);
    }
}
