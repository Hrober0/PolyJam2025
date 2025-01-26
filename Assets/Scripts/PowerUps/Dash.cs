using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dash : MonoBehaviour, IPowerUp
{
    private float dashRange;
    private Rigidbody rb;
    private ActiveCall call;

    public void SetUp(float dashRange)
    {
        this.dashRange = dashRange;
        rb = GetComponent<Rigidbody>();
        call = GetComponent<ActiveCall>();
        call.OnActiveCall += DashMove;
    }

    private void DashMove()
    {
        call.OnActiveCall -= DashMove;
        rb.AddForce(transform.forward * dashRange, ForceMode.Impulse);
        Destroy(this);
    }
}
