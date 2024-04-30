using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EmployeeAI : MonoBehaviour
{
    private AIState m_state;

    DonutCounter m_donutCounter;
    EmployeeStatistics m_statistics;
    GameManager m_gameManager;

    private NavMeshAgent m_agent;
    [SerializeField] private GameObject m_donutCounterPickup;
    [SerializeField] private GameObject m_customerCounter;

    private enum AIState
    {
        IDLE,
        WORKING
    };

    private void Start()
    {
        m_donutCounter = GameObject.FindGameObjectWithTag("DonutCounter").GetComponent<DonutCounter>();
        m_gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        m_statistics = GetComponent<EmployeeStatistics>();
        m_agent = GetComponent<NavMeshAgent>();

        m_donutCounterPickup = GameObject.Find("DonutCounterTrigger");
        m_customerCounter = GameObject.Find("CounterTrigger");

        //m_state = AIState.IDLE;
    }

    private void Update()
    {
        m_statistics.m_walkLevel = m_gameManager.m_debugEmployee.m_walkLevel;
        m_statistics.m_holdLevel = m_gameManager.m_debugEmployee.m_holdLevel;

        m_agent.speed = m_gameManager.m_debugEmployee.m_walkLevel + 1;
    }

    private void FixedUpdate()
    {
        switch (m_state)
        {
            case AIState.IDLE:

                if (m_donutCounter.m_donuts.Count > 0)
                {
                    m_state = AIState.WORKING;
                }

                break;

            case AIState.WORKING:

                ServeDonuts();

                if (m_donutCounter.m_donuts.Count == 0 && m_statistics.m_donutsHeld.Count == 0)
                {
                    m_state = AIState.IDLE;
                }

                break;

            default:
                break;
        }
    }

    public void ServeDonuts()
    {
        if (m_statistics.m_donutsHeld.Count == 0)
        {
            m_agent.SetDestination(m_donutCounterPickup.transform.position);
        }

        if (m_statistics.m_donutsHeld.Count == (m_gameManager.m_debugEmployee.m_holdLevel * 2) || m_donutCounter.m_donuts.Count == 0)
        {
            m_agent.SetDestination(m_customerCounter.transform.position);
        }
    }
}
