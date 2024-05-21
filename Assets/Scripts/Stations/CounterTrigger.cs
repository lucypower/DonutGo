using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CounterTrigger : MonoBehaviour
{
    GameManager m_gameManager;
    PlayerStatistics m_playerStatistics;
    EmployeeStatistics m_employeeStatistics;
    UpgradeManager m_upgradeManager;

    [SerializeField] AudioSource m_audioSource;

    [SerializeField] private List<CustomerAI> m_customerAI;
    public bool m_playerNear;

    private void Start()
    {
        m_gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        m_playerStatistics = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatistics>();
        m_upgradeManager = GameObject.FindGameObjectWithTag("UpgradeManager").GetComponent<UpgradeManager>();
    }

    public void UpdateCustomerList(GameObject customer)
    {
        m_customerAI.Add(customer.GetComponent<CustomerAI>());
    }

    public void ServeCustomer(bool player)
    {
        // gets first item in list, donuts being inserted into list at index 0
        
        Transform customerHold = m_customerAI[0].transform.Find("Hold");

        Vector3 offset = new Vector3(0, 0.2f * (m_customerAI[0].m_donutsHeld.Count - 1), 0);

        if (player)
        {
            GameObject donut = m_playerStatistics.m_donutsHeld.First();

            donut.transform.parent = customerHold;
            donut.transform.position = customerHold.position + offset;
        
            m_customerAI[0].m_donutsHeld.Add(donut);
            m_playerStatistics.m_donutsHeld.Remove(m_playerStatistics.m_donutsHeld[0]);            

            if (m_playerStatistics.m_donutsHeld.Count == 0)
            {
                m_playerStatistics.m_donutTypeHeld = "n";
            }
        }
        else
        {
            GameObject donut = m_employeeStatistics.m_donutsHeld.First();

            donut.transform.parent = customerHold;
            donut.transform.position = customerHold.position + offset;

            m_customerAI[0].m_donutsHeld.Add(donut);
            m_employeeStatistics.m_donutsHeld.Remove(m_employeeStatistics.m_donutsHeld[0]);

            if (m_employeeStatistics.m_donutsHeld.Count == 0)
            {
                m_employeeStatistics.m_donutTypeHeld = "n";
            }
        }

        int random = Random.Range(5, 16);
        int money;

        if (m_playerStatistics.m_xProfitActive)
        {
            money = Mathf.FloorToInt(random * ((1 + (m_playerStatistics.m_profitLevel / 10) + (m_upgradeManager.m_customerCounterLevel / 5)) * 2));
        }
        else
        {
            money = Mathf.FloorToInt(random * (1 + (m_playerStatistics.m_profitLevel / 10) + (m_upgradeManager.m_customerCounterLevel / 5)));
        }

        m_playerStatistics.m_money += money;

        if (m_customerAI[0].m_donutsHeld.Count >= m_customerAI[0].m_orderTotal)
        {
            m_customerAI[0].LeaveQueue();            
            RemoveCustomer();

            if (m_gameManager.m_soundOn)
            {
                m_audioSource.Play();
            }
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
        if (other.CompareTag("Player") || other.CompareTag("Employee"))
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
                            if (m_playerStatistics.m_donutTypeHeld == "i")
                            {
                                m_playerNear = false;
                                StartCoroutine(Timer(.75f));
                                ServeCustomer(true);
                            }
                        }
                    }
                }
            }
        }

        if (other.CompareTag("Employee")) 
        {
            m_employeeStatistics = other.GetComponent<EmployeeStatistics>();

            if (m_playerNear)
            {
                if (m_gameManager.m_spawnedCustomers.Count != 0)
                {
                    if (m_customerAI[0].m_atCounter)
                    {
                        if (m_employeeStatistics.m_donutsHeld.Count > 0)
                        {
                            if (m_employeeStatistics.m_donutTypeHeld == "i")
                            {
                                m_playerNear = false;
                                StartCoroutine(Timer(.75f));
                                ServeCustomer(false);
                            }                                
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Employee"))
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
