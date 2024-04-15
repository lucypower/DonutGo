using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputActionReference m_moveJoystick;
    [SerializeField] float m_movementSpeed;

    [SerializeField] GameObject m_model;
    //Transform m_model;

    private void Start()
    {
        //m_model = GetComponentInChildren<Transform>();
    }

    private void Update()
    {
        Vector2 input = m_moveJoystick.action.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(input.x, 0, input.y);

        Vector3 rotateDirection = Vector3.RotateTowards(transform.forward, moveDirection, 10, 0);

        m_model.transform.rotation = Quaternion.LookRotation(rotateDirection);
        transform.Translate(moveDirection * m_movementSpeed * Time.deltaTime);
    }
}
