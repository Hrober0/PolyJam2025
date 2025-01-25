using System.Collections;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class Dash : MonoBehaviour
{
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
            transform.Translate(direction * 0.016f * dashRange * 4f, Space.World);
            dashRange -= 0.016f * dashRange * 4f;
            await Task.Delay(16);
        }
        Destroy(this);
    }
}
