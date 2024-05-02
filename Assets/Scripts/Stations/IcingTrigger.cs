using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IcingTrigger : MonoBehaviour
{
    private IcingStation m_icingStation;
    public PlayerStatistics m_playerStats;
    private EmployeeStatistics m_employeeStats;

    [SerializeField] private Transform m_playerHold;
    private Transform m_employeeHold;
    [SerializeField] private Transform m_cookedDonutHold;

    private bool m_playerNear;

    private void Start()
    {
        Transform parent = transform.parent;
        m_icingStation = parent.Find("IcingCounter").GetComponent<IcingStation>();

        m_playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatistics>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Employee"))
        {
            StartCoroutine(Timer(1));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (m_playerNear)
            {
                if (m_playerStats.m_donutTypeHeld == "n" || m_playerStats.m_donutTypeHeld == "i")
                {
                    if (m_playerStats.m_donutsHeld.Count < m_playerStats.m_maxDonuts)
                    {
                        CollectIced(true);
                        RestartCoroutine();
                    }
                }
                else if (m_playerStats.m_donutTypeHeld == "c")
                {
                    DepositUnIced(true);
                    RestartCoroutine();
                }
            }
        }
        else if (other.CompareTag("Employee"))
        {
            m_employeeStats = other.GetComponent<EmployeeStatistics>();

            if (m_playerNear)
            {
                if (m_employeeStats.m_donutTypeHeld == "n" || m_employeeStats.m_donutTypeHeld == "i")
                {
                    if (m_employeeStats.m_donutsHeld.Count < m_employeeStats.m_maxDonuts && m_icingStation.m_icedDonuts.Count > 1)
                    {
                        CollectIced(true);
                        RestartCoroutine();
                    }                    
                }
                else if (m_employeeStats.m_donutTypeHeld == "c")
                {
                    DepositUnIced(true);
                    RestartCoroutine();
                }
            }
        }
    }

    public void CollectIced(bool isPlayer)
    {
        int donutToGo = m_icingStation.m_icedDonuts.Count - 1;
        GameObject donut = m_icingStation.m_icedDonuts[donutToGo];

        if (isPlayer)
        {
            m_playerStats.m_donutsHeld.Insert(0, donut);

            Vector3 offset = new Vector3(0, 0.25f * (m_playerStats.m_donutsHeld.Count - 1), 0);

            donut.transform.parent = m_playerHold.transform;
            donut.transform.position = m_playerHold.transform.position + offset;

            m_playerStats.m_donutTypeHeld = "i";
        }
        else
        {
            m_employeeStats.m_donutsHeld.Insert(0, donut);

            Vector3 offset = new Vector3(0, 0.25f * (m_employeeStats.m_donutsHeld.Count - 1), 0);

            m_employeeHold = m_employeeStats.transform.Find("Hold");

            donut.transform.parent = m_employeeHold;
            donut.transform.position = m_employeeHold.position + offset;

            m_playerStats.m_donutTypeHeld = "i";
        }

        m_icingStation.m_icedDonuts.Remove(donut);
    }

    public void DepositUnIced(bool isPlayer)
    {
        Vector3 offset = new Vector3(0, 0.25f * (m_icingStation.m_nonIcedDonuts.Count - 1), 0);

        if (isPlayer)
        {
            GameObject donut = m_playerStats.m_donutsHeld.First();

            donut.transform.parent = m_cookedDonutHold;
            donut.transform.position = m_cookedDonutHold.position + offset;

            m_icingStation.m_nonIcedDonuts.Add(donut);
            m_playerStats.m_donutsHeld.Remove(m_playerStats.m_donutsHeld[0]);

            if (m_playerStats.m_donutsHeld.Count == 0)
            {
                m_playerStats.m_donutTypeHeld = "n";
            }
        }
    }

    IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        m_playerNear = true;
    }

    public void RestartCoroutine()
    {
        m_playerNear = false;
        StartCoroutine(Timer(1));
    }
}
