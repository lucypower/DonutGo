using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EmployeeAI : MonoBehaviour
{
    public enum AIState
    {
        CHECKING,
        COOKING,
        ICING,
        SERVING
    };

    private AIState m_state;

    private NavMeshAgent m_agent;
    EmployeeStatistics m_stats;

    GameManager m_gameManager;

    DonutCounter m_donutCounter;
    Transform m_donutTrigger;

    CookingStation m_cookingStation;
    Transform m_cookingTrigger;

    IcingStation m_icingStation;
    Transform m_icingTrigger;

    Transform m_customerCounter;

    [SerializeField] private bool m_travelling;

    private void Start()
    {
        m_gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        m_customerCounter = GameObject.Find("CounterTrigger").transform;

        m_donutCounter = GameObject.FindGameObjectWithTag("DonutCounter").GetComponent<DonutCounter>();
        m_donutTrigger = GameObject.Find("DonutCounterTrigger").transform;

        m_cookingStation = GameObject.FindGameObjectWithTag("CookingCounter").GetComponent<CookingStation>();
        m_cookingTrigger = GameObject.Find("CookingCounterTrigger").transform;

        m_icingStation = GameObject.FindGameObjectWithTag("IcingCounter").GetComponent<IcingStation>();
        m_icingTrigger = GameObject.Find("IcingCounterTrigger").transform;

        m_agent = GetComponent<NavMeshAgent>();
        m_stats = GetComponent<EmployeeStatistics>();

        m_state = AIState.CHECKING;
    }

    private void Update()
    {
        m_stats.m_walkLevel = m_gameManager.m_debugEmployee.m_walkLevel;
        m_stats.m_holdLevel = m_gameManager.m_debugEmployee.m_holdLevel;

        m_agent.speed = m_gameManager.m_debugEmployee.m_walkLevel + 1;
    }

    private void FixedUpdate()
    {
        switch (m_state)
        {
            case AIState.CHECKING:

                Debug.Log("checking");

                CheckCounters();

            break; 
            
            case AIState.COOKING:

                Debug.Log("cooking");

                CookDonuts();

            break;

            case AIState.ICING:

                Debug.Log("icing");

                IceDonuts();

            break;

            case AIState.SERVING:

                Debug.Log("serving");

                ServeDonuts();

            break;
        }
    }

    public void CheckCounters()
    {
        int donutCounter = m_donutCounter.m_donuts.Count;
        int cookCounter = m_cookingStation.m_cookedDonuts.Count;
        int icingCounter = m_icingStation.m_icedDonuts.Count;

        if (!m_travelling)
        {
            if (donutCounter > cookCounter && donutCounter > icingCounter)
            {
                m_state = AIState.COOKING;
                m_travelling = true;
            }
            else if (cookCounter > donutCounter && cookCounter > icingCounter)
            {
                m_state = AIState.ICING;
                m_travelling = true;
            }
            else if (icingCounter > donutCounter && icingCounter > cookCounter)
            {
                m_state = AIState.SERVING;
                m_travelling = true;
            }
            else
            {
                m_state = AIState.COOKING;
                m_travelling = true;
            }
        }
    }

    public void CookDonuts()
    {
        if (m_travelling)
        {
            m_agent.SetDestination(m_donutCounter.transform.position);

            if (m_agent.remainingDistance <= m_agent.stoppingDistance)
            {
                m_travelling = false;
            }
        }

        if (!m_travelling)
        {
            if (m_stats.m_donutsHeld.Count == m_stats.m_maxDonuts || m_donutCounter.m_donuts.Count == 0)
            {
                m_agent.SetDestination(m_cookingStation.transform.position);
            }

            if (m_stats.m_donutsHeld.Count == 0)
            {
                m_state = AIState.CHECKING;
            }
        }
    }

    public void IceDonuts()
    {
        if (m_travelling)
        {
            m_agent.SetDestination(m_cookingStation.transform.position);

            if (m_agent.remainingDistance <= m_agent.stoppingDistance)
            {
                m_travelling = false;
            }
        }

        if (!m_travelling)
        {
            if (m_stats.m_donutsHeld.Count == m_stats.m_maxDonuts || m_cookingStation.m_cookedDonuts.Count == 0)
            {
                m_agent.SetDestination(m_icingStation.transform.position);
            }

            if (m_stats.m_donutsHeld.Count == 0)
            {
                m_state = AIState.CHECKING;
            }
        }
    }

    public void ServeDonuts()
    {
        if (m_travelling)
        {
            m_agent.SetDestination(m_icingStation.transform.position);

            if (m_agent.remainingDistance <= m_agent.stoppingDistance)
            {
                m_travelling = false;
            }
        }

        if (!m_travelling)
        {
            if (m_stats.m_donutsHeld.Count == m_stats.m_maxDonuts || m_icingStation.m_icedDonuts.Count == 0)
            {
                m_agent.SetDestination(m_customerCounter.position);
            }

            if (m_stats.m_donutsHeld.Count == 0)
            {
                m_state = AIState.CHECKING;
            }
        }
    }





















    //private AIState m_state;

    //DonutCounter m_donutCounter;
    //IcingStation m_icingStation;
    //CookingStation m_cookingStation;

    //EmployeeStatistics m_statistics;
    //GameManager m_gameManager;

    //private NavMeshAgent m_agent;
    //[SerializeField] private GameObject m_donutCounterPickup;
    //[SerializeField] private GameObject m_customerCounter;
    //[SerializeField] private GameObject m_cookCounter;
    //[SerializeField] private GameObject m_icingCounter;

    //private bool m_working;
    //private bool m_completingTask;

    //public enum AIState
    //{
    //    IDLE,
    //    CHOOSING,
    //    COOK,
    //    ICE,
    //    SERVE
    //};

    //private void Start()
    //{
    //    m_donutCounter = GameObject.FindGameObjectWithTag("DonutCounter").GetComponent<DonutCounter>();

    //    m_gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

    //    m_statistics = GetComponent<EmployeeStatistics>();
    //    m_agent = GetComponent<NavMeshAgent>();

    //    m_donutCounterPickup = GameObject.Find("DonutCounterTrigger");
    //    m_customerCounter = GameObject.Find("CounterTrigger");
    //    m_cookCounter = GameObject.Find("CookingCounter");
    //    m_icingCounter = GameObject.Find("IcingCounter");

    //    m_cookingStation = m_cookCounter.GetComponent<CookingStation>();
    //    m_icingStation = m_icingCounter.GetComponent<IcingStation>();

    //    m_state = AIState.CHOOSING;
    //}

    //private void Update()
    //{
    //    m_statistics.m_walkLevel = m_gameManager.m_debugEmployee.m_walkLevel;
    //    m_statistics.m_holdLevel = m_gameManager.m_debugEmployee.m_holdLevel;

    //    m_agent.speed = m_gameManager.m_debugEmployee.m_walkLevel + 1;
    //}

    //private void FixedUpdate()
    //{
    //    switch (m_state)
    //    {
    //        case AIState.IDLE:

    //            //if (m_donutCounter.m_donuts.Count > 0)
    //            //{
    //            //    m_state = AIState.WORKING;
    //            //}

    //            break;

    //        case AIState.CHOOSING:

    //            int random = Random.Range(1, 4);

    //            if (random == 1)
    //            {
    //                m_working = true;
    //                m_state = AIState.COOK;
    //            }
    //            else if (random == 2)
    //            {
    //                m_working = true;
    //                m_state = AIState.ICE;
    //            }
    //            else
    //            {
    //                m_working = true;
    //                m_state = AIState.SERVE;
    //            }

    //            //if (m_working)
    //            //{
    //            //    m_state = AIState.WORKING;
    //            //}

    //            break;

    //        case AIState.COOK:

    //            CookDonuts();

    //            if (!m_working)
    //            {
    //                m_state = AIState.CHOOSING;
    //            }

    //            //ServeDonuts();

    //            //if (m_donutCounter.m_donuts.Count == 0 && m_statistics.m_donutsHeld.Count == 0)
    //            //{
    //            //    m_state = AIState.IDLE;
    //            //}

    //            break;

    //        case AIState.ICE:

    //            IceDonuts();

    //            if (!m_working)
    //            {
    //                m_state = AIState.CHOOSING;
    //            }

    //        break;

    //        case AIState.SERVE:

    //            ServeDonuts();

    //            if (!m_working)
    //            {
    //                m_state = AIState.CHOOSING;
    //            }

    //        break;

    //        default:
    //            break;
    //    }
    //}

    //public void CookDonuts()
    //{
    //    Debug.Log("cook");

    //    //if (!m_completingTask)
    //    //{
    //    //    if (m_statistics.m_donutsHeld.Count == 0 && m_donutCounter.m_donuts.Count > 0)
    //    //    {
    //    //        m_agent.SetDestination(m_donutCounter.transform.position);

    //    //        if (m_agent.transform.position == m_donutCounter.transform.position && m_statistics.m_donutsHeld.Count == m_gameManager.m_debugEmployee.m_maxDonuts)
    //    //        {
    //    //            m_completingTask = true;
    //    //        }
    //    //    }
    //    //}
    //    //else
    //    //{
    //    //    m_agent.SetDestination(m_cookCounter.transform.position);

    //    //    if (m_statistics.m_donutsHeld.Count == 0)
    //    //    {
    //    //        m_working = false;
    //    //    }
    //    //}




    //    //if (m_statistics.m_donutsHeld.Count == m_gameManager.m_debugEmployee.m_maxDonuts || m_donutCounter.m_donuts.Count == 0)
    //    //{
    //    //    m_agent.SetDestination(m_cookCounter.transform.position);

    //    //    //if (m_statistics.m_donutsHeld.Count == 0)
    //    //    //{
    //    //    //    m_working = false;
    //    //    //}
    //    //    //if (m_agent.transform.position == m_cookCounter.transform.position && m_statistics.m_donutsHeld.Count == 0)
    //    //    //{
    //    //    //    m_working = false;
    //    //    //}
    //    //}

    //}

    //public void IceDonuts()
    //{
    //    Debug.Log("ice");

    //    if (m_statistics.m_donutsHeld.Count == 0)
    //    {
    //        m_agent.SetDestination(m_cookCounter.transform.position);
    //    }

    //    if (m_statistics.m_donutsHeld.Count == m_gameManager.m_debugEmployee.m_maxDonuts || m_cookingStation.m_cookedDonuts.Count == 0)
    //    {
    //        m_agent.SetDestination(m_icingCounter.transform.position);

    //        if (m_statistics.m_donutsHeld.Count == 0)
    //        {
    //            m_working = false;
    //        }
    //    }
    //}

    //public void ServeDonuts()
    //{
    //    Debug.Log("serve");

    //    if (m_statistics.m_donutsHeld.Count == 0)
    //    {
    //        m_agent.SetDestination(m_icingCounter.transform.position);
    //    }

    //    if (m_statistics.m_donutsHeld.Count == m_gameManager.m_debugEmployee.m_maxDonuts || m_icingStation.m_icedDonuts.Count == 0)
    //    {
    //        m_agent.SetDestination(m_customerCounter.transform.position);

    //        if (m_statistics.m_donutsHeld.Count == 0)
    //        {
    //            m_working = false;
    //        }
    //    }
    //}
}
