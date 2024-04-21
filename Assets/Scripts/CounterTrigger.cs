using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterTrigger : MonoBehaviour
{
    GameManager m_gameManager;
    PlayerStatistics m_playerStatistics;

    [SerializeField] private List<CustomerAI> m_customerAI;
    

    private void Start()
    {
        m_gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        m_playerStatistics = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatistics>();
    }

    public void UpdateCustomerList(GameObject customer)
    {
        m_customerAI.Add(customer.GetComponent<CustomerAI>());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (m_gameManager.m_spawnedCustomers.Count != 0)
            {
                if (m_playerStatistics.m_donutsHeld.Count > 0)
                {
                    ServeCustomer();
                    RemoveCustomer();
                }
            }
        }
    }

    public void ServeCustomer()
    {
        int donutNo = m_playerStatistics.m_donutsHeld.Count - 1;
        GameObject donut = m_playerStatistics.m_donutsHeld[donutNo];
        Transform customerHold = m_customerAI[0].transform.Find("Hold");

        donut.transform.position = customerHold.position; // TODO: Phantom donut, everything working as expected apart from the model remains for the second donut
        donut.transform.parent = customerHold;

        m_playerStatistics.m_donutsHeld.Remove(m_playerStatistics.m_donutsHeld[0]);
        m_customerAI[0].LeaveQueue();
    }

    public void RemoveCustomer()
    {
        GameObject customer = m_gameManager.m_spawnedCustomers[0];
        
        m_gameManager.m_spawnedCustomers.Remove(customer);
        m_customerAI.Remove(m_customerAI[0]);
        //Destroy(customer);

        for (int i = 0; i < m_customerAI.Count; i++)
        {
            m_customerAI[i].UpdateQueuePosition();
        }
    }
}
