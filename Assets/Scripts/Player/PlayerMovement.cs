using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputActionReference m_moveJoystick;
    [SerializeField] GameObject m_model;

    PlayerStatistics m_statistics;
    public Rigidbody m_rigidbody;
    Vector2 m_input;

    [SerializeField] GameObject m_rightArm;
    [SerializeField] GameObject m_leftArm;

    private void Start()
    {
        m_statistics = GetComponent<PlayerStatistics>();
        m_rigidbody = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        //old movement
        //transform.Translate(m_statistics.m_movementSpeed * Time.deltaTime * moveDirection);

        m_input = m_moveJoystick.action.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(m_input.x, 0, m_input.y);
        
        //rotation

        if (m_input.x != 0 || m_input.y != 0)
        {
            Vector3 rotateDirection = Vector3.RotateTowards(transform.forward, moveDirection, 10, 0);

            m_model.transform.rotation = Quaternion.LookRotation(rotateDirection);
        }
    }

    private void FixedUpdate()
    {
        m_rigidbody.velocity = new Vector3(m_input.x * (m_statistics.m_walkLevel + 2), m_rigidbody.velocity.y, m_input.y * (m_statistics.m_walkLevel + 2));

        if (m_statistics.m_donutsHeld.Count > 0)
        {
            m_leftArm.transform.localRotation = Quaternion.Euler(78.0244064f, 343.627136f, 259.284485f);
            m_rightArm.transform.localRotation = Quaternion.Euler(85.3386765f, 333.392548f, 60.2258415f);
        }
        else
        {
            m_leftArm.transform.localRotation = Quaternion.Euler(54.3289757f, 355.844543f, 354.642822f);
            m_rightArm.transform.localRotation = Quaternion.Euler(53.8094521f, 1.7422266f, 2.54413462f);
        }
    }
}
