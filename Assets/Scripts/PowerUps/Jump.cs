using UnityEngine;

public class Jump : MonoBehaviour
{
    public async void SetUp(float force)
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * force, ForceMode.Impulse);
        await Awaitable.WaitForSecondsAsync(0.1f);
        Destroy(this);
    }
}
