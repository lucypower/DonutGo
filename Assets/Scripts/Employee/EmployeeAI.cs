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

    public AIState m_state;

    private NavMeshAgent m_agent;
    EmployeeStatistics m_stats;

    GameManager m_gameManager;

    DonutCounter m_donutCounter;
    CookingStation m_cookingStation;
    IcingStation m_icingStation;
    Transform m_customerCounter;

    [SerializeField] GameObject m_rightArm;
    [SerializeField] GameObject m_leftArm;

    public bool m_travelling;

    private void Start()
    {
        m_gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        m_customerCounter = GameObject.Find("CounterTrigger").transform;

        m_donutCounter = GameObject.FindGameObjectWithTag("DonutCounter").GetComponent<DonutCounter>();

        m_cookingStation = GameObject.FindGameObjectWithTag("CookingCounter").GetComponent<CookingStation>();

        m_icingStation = GameObject.FindGameObjectWithTag("IcingCounter").GetComponent<IcingStation>();

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

                CheckCounters();

            break; 
            
            case AIState.COOKING:

                CookDonuts();

            break;

            case AIState.ICING:

                IceDonuts();

            break;

            case AIState.SERVING:

                ServeDonuts();

            break;
        }

        if (m_stats.m_donutsHeld.Count > 0)
        {
            m_leftArm.transform.localRotation = Quaternion.Euler(78.0244064f, 343.627136f, 259.284485f);
            m_rightArm.transform.localRotation = Quaternion.Euler(85.3386765f, 333.392548f, 60.2258415f);
        }
        else
        {
            m_leftArm.transform.localRotation = Quaternion.Euler(54.3289757f, 355.844543f, 354.642822f);
            m_rightArm.transform.localRotation = Quaternion.Euler(53.8094521f, 1.7422266f, 2.54413462f);
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
                //m_state = AIState.COOKING;
                //m_travelling = true;

                int random = Random.Range(0, 4);

                if (random == 0)
                {
                    m_state = AIState.COOKING;
                }
                if (random == 1)
                {
                    m_state = AIState.ICING;
                }
                else
                {
                    m_state = AIState.SERVING;
                }

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
}
