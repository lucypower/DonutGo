using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterTrigger : MonoBehaviour
{
    GameManager m_gameManager;
    [SerializeField] List<CustomerAI> m_customerAI;

    private void Start()
    {
        m_gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void UpdateCustomerList(GameObject customer)
    {
        m_customerAI.Add(customer.GetComponent<CustomerAI>());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("enter");

            if (m_gameManager.m_spawnedCustomers.Count != 0)
            {
                GameObject customer = m_gameManager.m_spawnedCustomers[0];

                m_gameManager.m_spawnedCustomers.Remove(customer);
                m_customerAI.Remove(m_customerAI[0]);
                Destroy(customer);

                for (int i = 0; i < m_customerAI.Count; i++)
                {
                    m_customerAI[i].UpdateQueuePosition();
                }
            }
        }
    }
}
