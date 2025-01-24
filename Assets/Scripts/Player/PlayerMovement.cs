using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;
    private float direction;

    private int left;
    private int right;
    private int reverse = 1;

    [SerializeField]
    private float rotationSpeed;
    private float rotationDirection;

    private void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * rotationDirection * reverse * Time.deltaTime);
        transform.Translate(Vector3.forward * moveSpeed * direction * reverse * Time.deltaTime);
    }

    public void MoveLeft(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            left = (int)context.ReadValue<float>();
            SetMoveDirection();
            SetRotationDirection();
        }
    }

    public void MoveRight(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            right = (int)context.ReadValue<float>();
            SetMoveDirection();
            SetRotationDirection();
        }
    }

    public void MoveBack(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            Debug.Log("MoveBack");
            reverse *= -1;
            SetMoveDirection();
            SetRotationDirection();
        }
    }

    public void SetMoveDirection()
    {
        direction = Mathf.Clamp(left + right, 0, 1);
        if(direction == 0 && reverse == -1)
        {
            direction = 1;
        }
    }

    public void SetRotationDirection()
    {
        rotationDirection = right - left;
        if (reverse == -1 && left == 1 && right == 1)
        {
            direction = 0;
            rotationDirection = -1;
        }
    }
}
