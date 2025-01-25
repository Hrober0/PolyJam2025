using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class Dash : MonoBehaviour, IPowerUp
{
    private const float DASH_TIME = 0.016f;

    private float dashRange;

    public void SetUp(float dashRange)
    {
        this.dashRange = dashRange;
        GetComponent<ActiveCall>().OnActiveCall += () => DashMove();
    }

    private async Task DashMove()
    {
        Vector3 direction = transform.forward;

        while (dashRange > 0.2f)
        {
            transform.Translate(direction * DASH_TIME * dashRange * 4f, Space.World);
            dashRange -= DASH_TIME * dashRange * 4f;
            await Awaitable.WaitForSecondsAsync(DASH_TIME);
        }
        Destroy(this);
    }
}
