using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputActionReference m_moveJoystick;
    [SerializeField] GameObject m_model;

    PlayerStatistics m_statistics;

    private void Start()
    {
        m_statistics = GetComponent<PlayerStatistics>();
    }

    private void Update()
    {
        //movement

        Vector2 input = m_moveJoystick.action.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(input.x, 0, input.y);

        transform.Translate(m_statistics.m_movementSpeed * Time.deltaTime * moveDirection);
        
        //rotation

        Vector3 rotateDirection = Vector3.RotateTowards(transform.forward, moveDirection, 10, 0);

        m_model.transform.rotation = Quaternion.LookRotation(rotateDirection);
    }
}
