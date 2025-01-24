using HCore;
using UnityEngine;

public class BubblePainter : MonoBehaviour
{
    [SerializeField] private float paintTreshold = 0.05f;
    [SerializeField] private float range = 0.5f;

    private Vector3 lastPos = Vector3.zero;
    private bool isGrounded = false;

    private void Update()
    {
        if ((lastPos - transform.position).sqrMagnitude > paintTreshold)
        {
            Paint();
            lastPos = transform.position;
        }
    }

    private void Paint()
    {
        if (isGrounded)
        {
            FloorPainter.Instance.ClearFloor(transform.position.To2D(), range);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out FloorPainter _))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out FloorPainter _))
        {
            isGrounded = false;
        }
    }
}
