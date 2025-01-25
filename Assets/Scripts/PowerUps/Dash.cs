using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class Dash : MonoBehaviour, IPowerUp
{
    private const float DASH_SPEED = 4f;

    private float dashRange;
    private Rigidbody rb;

    public void SetUp(float dashRange)
    {
        this.dashRange = dashRange;
        rb = GetComponent<Rigidbody>();
        GetComponent<ActiveCall>().OnActiveCall += () => DashMove();
    }

    private async Task DashMove()
    {
        Vector3 direction = transform.forward;
        var remRange = dashRange;

        while (remRange > 0)
        {
            rb.MovePosition(transform.position + direction * Time.fixedDeltaTime * remRange * DASH_SPEED);
            remRange -= Time.fixedDeltaTime * dashRange * DASH_SPEED;
            await Awaitable.WaitForSecondsAsync(Time.fixedDeltaTime);
        }
        Destroy(this);
    }
}
