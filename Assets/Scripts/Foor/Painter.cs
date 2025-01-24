using HCore;
using UnityEngine;

public class Painter : MonoBehaviour
{
    [SerializeField] private Camera raycastCamera;
    [SerializeField] private FloorPainter floorPainter;
    [SerializeField] private float range;

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            var direction = raycastCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(direction, out RaycastHit hit))
            {
                floorPainter.ClearFloor(hit.point.To2D(), range);
            }
        }
    }
}
