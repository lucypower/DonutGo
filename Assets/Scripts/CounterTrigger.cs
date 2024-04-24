using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CounterTrigger : MonoBehaviour
{
    GameManager m_gameManager;
    PlayerStatistics m_playerStatistics;

    [SerializeField] private List<CustomerAI> m_customerAI;
    bool m_playerNear;

    private void Start()
    {
        m_gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        m_playerStatistics = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatistics>();
    }

    public void UpdateCustomerList(GameObject customer)
    {
        m_customerAI.Add(customer.GetComponent<CustomerAI>());
    }

    public void ServeCustomer()
    {
        //int donutNo = m_playerStatistics.m_donutsHeld.Count - 1;
        //GameObject donut = m_playerStatistics.m_donutsHeld[donutNo]

        // gets first item in list, donuts being inserted into list at index 0

        GameObject donut = m_playerStatistics.m_donutsHeld.First();
        Transform customerHold = m_customerAI[0].transform.Find("Hold");

        Vector3 offset = new Vector3(0, 0.25f * (m_customerAI[0].m_donutsHeld.Count - 1), 0);

        donut.transform.parent = customerHold;
        donut.transform.position = customerHold.position + offset;

        m_customerAI[0].m_donutsHeld.Add(donut);
        m_playerStatistics.m_donutsHeld.Remove(m_playerStatistics.m_donutsHeld[0]);

        m_playerStatistics.m_money += 10;

        if (m_customerAI[0].m_donutsHeld.Count >= m_customerAI[0].m_orderTotal)
        {
            m_customerAI[0].LeaveQueue();
            RemoveCustomer();
        }

    }

    public void RemoveCustomer()
    {
        GameObject customer = m_gameManager.m_spawnedCustomers[0];

        m_gameManager.m_spawnedCustomers.Remove(customer);
        m_customerAI.Remove(m_customerAI[0]);

        for (int i = 0; i < m_customerAI.Count; i++)
        {
            m_customerAI[i].UpdateQueuePosition();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Timer(.5f));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (m_playerNear)
            {
                if (m_gameManager.m_spawnedCustomers.Count != 0)
                {
                    if (m_customerAI[0].m_atCounter)
                    {
                        if (m_playerStatistics.m_donutsHeld.Count > 0)
                        {
                            m_playerNear = false;
                            StartCoroutine(Timer(.75f));
                            ServeCustomer();
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopCoroutine(Timer(.75f));
        }
    }

    IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        m_playerNear = true;
    }
}
