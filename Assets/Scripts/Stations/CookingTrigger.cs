using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CookingTrigger : MonoBehaviour
{
    private CookingStation m_cooker;
    private PlayerStatistics m_playerStats;
    private EmployeeStatistics m_employeeStats;

    private EmployeeAI m_employeeAI;

    [SerializeField] private GameObject m_playerHold;
    private Transform m_employeeHold;
    [SerializeField] private Transform m_uncookedDonutHold;

    private bool m_playerNear;

    private void Start()
    {
        Transform parent = transform.parent;

        m_cooker = parent.Find("CookingCounter").GetComponent<CookingStation>();
        m_playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatistics>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Employee"))
        {
            StartCoroutine(Timer(2));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (m_playerNear)
            {
                if (m_playerStats.m_donutTypeHeld == "n" || m_playerStats.m_donutTypeHeld == "c")
                {
                    if (m_playerStats.m_donutsHeld.Count < m_playerStats.m_maxDonuts)
                    {
                        CollectCooked(true);
                        RestartCoroutine();
                    }
                }
                else if (m_playerStats.m_donutTypeHeld == "u")
                {
                    DepositUncooked(true);
                    RestartCoroutine();
                }
            }
        }
        else if (other.CompareTag("Employee"))
        {
            m_employeeStats = other.GetComponent<EmployeeStatistics>();
            m_employeeHold = other.transform.GetChild(0);
            m_employeeAI = other.GetComponent<EmployeeAI>();

            if (m_playerNear)
            {
                if (m_employeeStats.m_donutTypeHeld == "n" || m_employeeStats.m_donutTypeHeld == "c")
                {
                    if (m_employeeStats.m_donutsHeld.Count < m_employeeStats.m_maxDonuts)
                    {

                        if (m_employeeAI.m_state == EmployeeAI.AIState.ICING)
                        {
                            CollectCooked(false);
                            RestartCoroutine();
                        }
                    }
                }
                else if (m_employeeStats.m_donutTypeHeld == "u")
                {
                    DepositUncooked(false);
                    RestartCoroutine();
                }    
            }
        }
    }

    IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        m_playerNear = true;
    }

    public void CollectCooked(bool isPlayer)
    {
        int donutToGo = m_cooker.m_cookedDonuts.Count - 1;
        GameObject donut = m_cooker.m_cookedDonuts[donutToGo];

        if (isPlayer)
        {
            m_playerStats.m_donutsHeld.Insert(0, donut);

            Vector3 offset = new Vector3(0, 0.25f * (m_playerStats.m_donutsHeld.Count -1), 0);

            donut.transform.parent = m_playerHold.transform;
            donut.transform.position = m_playerHold.transform.position + offset;

            m_playerStats.m_donutTypeHeld = "c";
        }
        else
        {
            m_employeeStats.m_donutsHeld.Insert(0, donut);

            Vector3 offset = new Vector3(0, 0.25f * (m_employeeStats.m_donutsHeld.Count - 1), 0);

            donut.transform.parent = m_employeeHold;
            donut.transform.position = m_employeeHold.position + offset;

            m_employeeStats.m_donutTypeHeld = "c";
        }

        m_cooker.m_cookedDonuts.Remove(donut);
    }

    public void DepositUncooked(bool isPlayer)
    {
        Vector3 offset = new Vector3(0, 0.25f * (m_cooker.m_uncookedDonuts.Count - 1), 0);

        if (isPlayer)
        {
            GameObject donut = m_playerStats.m_donutsHeld.First();

            donut.transform.parent = m_uncookedDonutHold;
            donut.transform.position = m_uncookedDonutHold.position + offset;

            m_cooker.m_uncookedDonuts.Add(donut);
            m_playerStats.m_donutsHeld.Remove(m_playerStats.m_donutsHeld[0]);

            if (m_playerStats.m_donutsHeld.Count == 0)
            {
                m_playerStats.m_donutTypeHeld = "n";
            }
        }
        else
        {
            GameObject donut = m_employeeStats.m_donutsHeld.First();

            donut.transform.parent = m_uncookedDonutHold;
            donut.transform.position = m_uncookedDonutHold.position + offset;

            m_cooker.m_uncookedDonuts.Add(donut);
            m_employeeStats.m_donutsHeld.Remove(m_employeeStats.m_donutsHeld[0]);

            if (m_employeeStats.m_donutsHeld.Count == 0)
            {
                m_employeeStats.m_donutTypeHeld = "n";
            }
        }
    }

    public void RestartCoroutine()
    {
        m_playerNear = false;
        StartCoroutine(Timer(1));
    }
}
