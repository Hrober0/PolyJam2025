using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private Rigidbody rb;

    private int left = 0;
    private int right = 0;
    private int reverse = 1;
    private float direction = 0f;
    private float rotationDirection = 0f;

    private void FixedUpdate()
    {
        Vector3 move = transform.forward * moveSpeed * direction * reverse * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + move);

        float rotationAmount = rotationSpeed * rotationDirection * reverse * Time.fixedDeltaTime;

        Quaternion deltaRotation = Quaternion.Euler(0f, rotationAmount, 0f);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }

    public void MoveLeft(InputAction.CallbackContext context) => MoveLeft(context.phase);
    public void MoveLeft(InputActionPhase action)
    {
        Debug.Log($"{name} left {action}");
        if (action == InputActionPhase.Performed)
        {
            left = 1;
        }
        else if (action == InputActionPhase.Canceled)
        {
            left = 0;
        }
        SetMoveDirection();
        SetRotationDirection();
    }

    public void MoveRight(InputAction.CallbackContext context) => MoveRight(context.phase);
    public void MoveRight(InputActionPhase action)
    {
        Debug.Log($"{name} right {action}");
        if (action == InputActionPhase.Performed)
        {
            right = 1;
        }
        else if (action == InputActionPhase.Canceled)
        {
            right = 0;
        }
        SetMoveDirection();
        SetRotationDirection();
    }

    public void MoveBack(InputAction.CallbackContext context) => MoveBack(context.phase);
    public void MoveBack(InputActionPhase action)
    {
        if (action == InputActionPhase.Performed)
        {
            reverse *= -1;
            SetMoveDirection();
            SetRotationDirection();
        }
    }

    private void SetMoveDirection()
    {
        direction = Mathf.Clamp(left + right, 0, 1);
        if (direction == 0 && reverse == -1)
        {
            direction = 1;
        }
    }

    private void SetRotationDirection()
    {
        rotationDirection = right - left;

        if (reverse == -1 && left == 1 && right == 1)
        {
            direction = 0;
            rotationDirection = -1;
        }
    }
}
