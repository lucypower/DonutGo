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


        //Debug.Log(m_rigidbody.velocity.magnitude);
    }

    private void FixedUpdate()
    {
        m_rigidbody.velocity = new Vector3(m_input.x * (m_statistics.m_walkLevel + 2), m_rigidbody.velocity.y, m_input.y * (m_statistics.m_walkLevel + 2));
    }
}
