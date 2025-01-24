using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    private float direction;
    public int reverse = 1;

    public float left;
    public float right;

    public float rotationSpeed;//this is the speed of the rotation of the player
    public float rotationDirection;//this is the direction of the rotation of the player

    private void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * rotationDirection * reverse * Time.deltaTime);
        transform.Translate(Vector3.forward * moveSpeed * direction * reverse * Time.deltaTime);
        //Debug.Log($"left {left}, right {right}, moveSpeed {moveSpeed}, rotationDirection {rotationDirection}");
    }

    public void MoveLeft(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("MoveLeft");
            left = context.ReadValue<float>();
            SetMoveDirection();
            SetRotationDirection();
        }
    }

    public void MoveRight(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("MoveRight");
            right = context.ReadValue<float>();
            SetMoveDirection();
            SetRotationDirection();

        }
    }
    public void ChangeDirection(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            Debug.LogWarning("call");
            reverse *= -1;
        }
    }

    public void SetMoveDirection()
    {
        direction = Mathf.Clamp(left + right, 0, 1);
    }

    public void SetRotationDirection()
    {
        rotationDirection = left - right;
        rotationDirection *= -1;
    }
}
