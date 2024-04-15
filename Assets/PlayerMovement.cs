using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputActionReference m_moveJoystick;
    [SerializeField] float m_movementSpeed;

    private void Update()
    {
        Vector2 input = m_moveJoystick.action.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(input.x, 0, input.y);

        transform.Translate(moveDirection * m_movementSpeed * Time.deltaTime);
    }
}
