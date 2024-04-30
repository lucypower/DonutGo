using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomerAI : MonoBehaviour
{
    CustomerStatistics m_statistics;
    GameManager m_gameManager;

    [SerializeField] GameObject m_counterLocation;
    [SerializeField] GameObject m_leaveLocation1;
    [SerializeField] GameObject m_leaveLocation2;

    Vector3 m_offset;

    [HideInInspector] public bool m_atCounter;
    bool m_inQueue;
    bool m_leavingBuilding;

    public List<GameObject> m_donutsHeld;
    public int m_orderTotal;
    public GameObject m_orderUI;
    public TextMeshProUGUI m_orderText;

    private void Start()
    {
        m_statistics = GetComponent<CustomerStatistics>();
        m_gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        m_counterLocation = GameObject.FindGameObjectWithTag("Counter");
        m_leaveLocation1 = GameObject.FindGameObjectWithTag("LeaveLocation");
        m_leaveLocation2 = GameObject.FindGameObjectWithTag("LeaveLocation2");

        m_offset = new Vector3(0, 0, (m_gameManager.m_spawnedCustomers.Count - 1) * 2);
        m_inQueue = true;

        m_orderTotal = Random.Range(1, 4);
        m_orderUI = transform.Find("Order").gameObject;
        m_orderText = m_orderUI.GetComponentInChildren<TextMeshProUGUI>();
        m_orderText.text = m_orderTotal.ToString();
    }

    private void Update() 
    {
        if (m_inQueue) // could come back and make this a switch statement with an enum for states
        {
            transform.position = Vector3.MoveTowards(transform.position, m_counterLocation.transform.position - m_offset,
            m_statistics.m_movementSpeed * Time.deltaTime);
        }
        else
        {
            if (!m_leavingBuilding)
            {
                transform.position = Vector3.MoveTowards(transform.position, m_leaveLocation1.transform.position, m_statistics.m_movementSpeed * Time.deltaTime);

                if (transform.position == m_leaveLocation1.transform.position)
                {
                    m_leavingBuilding = true;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, m_leaveLocation2.transform.position, m_statistics.m_movementSpeed * Time.deltaTime);

                if (transform.position == m_leaveLocation2.transform.position)
                {
                    Destroy(gameObject);
                }
            }
        }

        if (transform.position == m_counterLocation.transform.position)
        {
            m_atCounter = true;
            m_orderUI.SetActive(true);
        }
    }

    public void UpdateQueuePosition()
    {
        m_offset = m_offset + new Vector3(0, 0, -2);
    }

    public void LeaveQueue() 
    {
        m_inQueue = false;
        m_orderUI.SetActive(false);
    }
}
