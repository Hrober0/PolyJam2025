using UnityEngine;

public class Repaint : MonoBehaviour
{
    [SerializeField]
    private FloorPainter floorPainter;

    public void RepaintFloor()
    {
        floorPainter.FillTxt();
    }
}
