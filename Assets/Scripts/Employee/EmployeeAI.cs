using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EmployeeAI : MonoBehaviour
{
    private AIState m_state;

    DonutCounter m_donutCounter;
    IcingStation m_icingStation;
    CookingStation m_cookingStation;

    EmployeeStatistics m_statistics;
    GameManager m_gameManager;

    private NavMeshAgent m_agent;
    [SerializeField] private GameObject m_donutCounterPickup;
    [SerializeField] private GameObject m_customerCounter;
    [SerializeField] private GameObject m_cookCounter;
    [SerializeField] private GameObject m_icingCounter;

    private bool m_working;
    private bool m_completingTask;

    public enum AIState
    {
        IDLE,
        CHOOSING,
        COOK,
        ICE,
        SERVE
    };

    private void Start()
    {
        m_donutCounter = GameObject.FindGameObjectWithTag("DonutCounter").GetComponent<DonutCounter>();

        m_gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        m_statistics = GetComponent<EmployeeStatistics>();
        m_agent = GetComponent<NavMeshAgent>();

        m_donutCounterPickup = GameObject.Find("DonutCounterTrigger");
        m_customerCounter = GameObject.Find("CounterTrigger");
        m_cookCounter = GameObject.Find("CookingCounter");
        m_icingCounter = GameObject.Find("IcingCounter");

        m_cookingStation = m_cookCounter.GetComponent<CookingStation>();
        m_icingStation = m_icingCounter.GetComponent<IcingStation>();
        
        m_state = AIState.CHOOSING;
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

                //if (m_donutCounter.m_donuts.Count > 0)
                //{
                //    m_state = AIState.WORKING;
                //}

                break;

            case AIState.CHOOSING:

                int random = Random.Range(1, 4);

                if (random == 1)
                {
                    m_working = true;
                    m_state = AIState.COOK;
                }
                else if (random == 2)
                {
                    m_working = true;
                    m_state = AIState.ICE;
                }
                else
                {
                    m_working = true;
                    m_state = AIState.SERVE;
                }

                //if (m_working)
                //{
                //    m_state = AIState.WORKING;
                //}

                break;

            case AIState.COOK:

                CookDonuts();

                if (!m_working)
                {
                    m_state = AIState.CHOOSING;
                }

                //ServeDonuts();

                //if (m_donutCounter.m_donuts.Count == 0 && m_statistics.m_donutsHeld.Count == 0)
                //{
                //    m_state = AIState.IDLE;
                //}

                break;

            case AIState.ICE:

                IceDonuts();

                if (!m_working)
                {
                    m_state = AIState.CHOOSING;
                }

            break;

            case AIState.SERVE:

                ServeDonuts();

                if (!m_working)
                {
                    m_state = AIState.CHOOSING;
                }

            break;

            default:
                break;
        }
    }

    public void CookDonuts()
    {
        Debug.Log("cook");

        //if (!m_completingTask)
        //{
        //    if (m_statistics.m_donutsHeld.Count == 0 && m_donutCounter.m_donuts.Count > 0)
        //    {
        //        m_agent.SetDestination(m_donutCounter.transform.position);

        //        if (m_agent.transform.position == m_donutCounter.transform.position && m_statistics.m_donutsHeld.Count == m_gameManager.m_debugEmployee.m_maxDonuts)
        //        {
        //            m_completingTask = true;
        //        }
        //    }
        //}
        //else
        //{
        //    m_agent.SetDestination(m_cookCounter.transform.position);

        //    if (m_statistics.m_donutsHeld.Count == 0)
        //    {
        //        m_working = false;
        //    }
        //}




        //if (m_statistics.m_donutsHeld.Count == m_gameManager.m_debugEmployee.m_maxDonuts || m_donutCounter.m_donuts.Count == 0)
        //{
        //    m_agent.SetDestination(m_cookCounter.transform.position);

        //    //if (m_statistics.m_donutsHeld.Count == 0)
        //    //{
        //    //    m_working = false;
        //    //}
        //    //if (m_agent.transform.position == m_cookCounter.transform.position && m_statistics.m_donutsHeld.Count == 0)
        //    //{
        //    //    m_working = false;
        //    //}
        //}

    }

    public void IceDonuts()
    {
        Debug.Log("ice");

        if (m_statistics.m_donutsHeld.Count == 0)
        {
            m_agent.SetDestination(m_cookCounter.transform.position);
        }

        if (m_statistics.m_donutsHeld.Count == m_gameManager.m_debugEmployee.m_maxDonuts || m_cookingStation.m_cookedDonuts.Count == 0)
        {
            m_agent.SetDestination(m_icingCounter.transform.position);

            if (m_statistics.m_donutsHeld.Count == 0)
            {
                m_working = false;
            }
        }
    }

    public void ServeDonuts()
    {
        Debug.Log("serve");

        if (m_statistics.m_donutsHeld.Count == 0)
        {
            m_agent.SetDestination(m_icingCounter.transform.position);
        }

        if (m_statistics.m_donutsHeld.Count == m_gameManager.m_debugEmployee.m_maxDonuts || m_icingStation.m_icedDonuts.Count == 0)
        {
            m_agent.SetDestination(m_customerCounter.transform.position);

            if (m_statistics.m_donutsHeld.Count == 0)
            {
                m_working = false;
            }
        }
    }
}
