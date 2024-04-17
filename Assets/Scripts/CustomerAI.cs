using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerAI : MonoBehaviour
{
    CustomerStatistics m_statistics;
    GameManager m_gameManager;

    [SerializeField] GameObject m_counterLocation;

    Vector3 m_offset;

    private void Start()
    {
        m_statistics = GetComponent<CustomerStatistics>();
        m_gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        m_counterLocation = GameObject.FindGameObjectWithTag("Counter");

        m_offset = new Vector3(0, 0, (m_gameManager.m_spawnedCustomers.Count - 1) * 2);
    }

    private void Update() // will need a way to update position once a customer leaves the line wihtout all customers having the same new position
    {
        transform.position = Vector3.MoveTowards(transform.position, m_counterLocation.transform.position - m_offset, 
            m_statistics.m_movementSpeed * Time.deltaTime);
    }
}
