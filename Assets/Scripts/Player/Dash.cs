using System.Collections;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private const float DASH_TIME = 0.016f;

    [SerializeField]
    private float dashRange = 5f;

    private void Start()
    {
        GetComponent<ActiveCall>().OnActiveCall += StartDash;
    }

    private void StartDash()
    {
        Debug.Log("Dash");
        DashMove();
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
