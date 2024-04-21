using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerAI : MonoBehaviour
{
    CustomerStatistics m_statistics;
    GameManager m_gameManager;

    [SerializeField] GameObject m_counterLocation;
    [SerializeField] GameObject m_leaveLocation;

    Vector3 m_offset;

    [HideInInspector] public bool m_atCounter;
    bool m_inQueue;

    public List<GameObject> m_donutsHeld;
    public int m_orderTotal;

    private void Start()
    {
        m_statistics = GetComponent<CustomerStatistics>();
        m_gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        m_counterLocation = GameObject.FindGameObjectWithTag("Counter");
        m_leaveLocation = GameObject.FindGameObjectWithTag("LeaveLocation");

        m_offset = new Vector3(0, 0, (m_gameManager.m_spawnedCustomers.Count - 1) * 2);
        m_inQueue = true;

        m_orderTotal = Random.Range(1, 3);
    }

    private void Update() 
    {
        if (m_inQueue)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_counterLocation.transform.position - m_offset,
            m_statistics.m_movementSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, m_leaveLocation.transform.position, m_statistics.m_movementSpeed * Time.deltaTime);
            //transform.rotation = Vector3.RotateTowards(Vector3.forward);

            if (transform.position == m_leaveLocation.transform.position)
            {
                Destroy(gameObject);
            }
        }

        if (transform.position == m_counterLocation.transform.position)
        {
            m_atCounter = true;
        }
    }

    public void UpdateQueuePosition()
    {
        m_offset = m_offset + new Vector3(0, 0, -2);
    }

    public void LeaveQueue() // TODO: Make condition so they can't be served until they're at the counter
    {
        m_inQueue = false;
    }
}
