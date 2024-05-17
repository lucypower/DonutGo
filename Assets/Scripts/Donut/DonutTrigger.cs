using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DonutTrigger : MonoBehaviour
{
    PlayerStatistics m_playerStatistics;
    EmployeeStatistics m_employeeStatistics;
    [SerializeField] private GameObject m_playerHold;
    [SerializeField] private GameObject m_employeeHold;

    DonutCounter m_donutCounter;

    bool m_playerNear;

    private void Start()
    {
        m_playerStatistics = m_playerHold.GetComponentInParent<PlayerStatistics>();
        m_donutCounter = GetComponentInParent<DonutCounter>();
    }

    public void DonutPickup(bool player)
    {
        int donutToGo = m_donutCounter.m_donuts.Count - 1;
        GameObject donut = m_donutCounter.m_donuts[donutToGo];

        if (player)
        {
            m_playerStatistics.m_donutsHeld.Insert(0, donut);

            Vector3 offset = new Vector3(0, 0.25f * (m_playerStatistics.m_donutsHeld.Count - 1), 0); // TODO: will need to change when a new model is used

            donut.transform.parent = m_playerHold.transform;
            donut.transform.position = m_playerHold.transform.position + offset;

            m_playerStatistics.m_donutTypeHeld = "u";
        }
        else
        {
            m_employeeStatistics.m_donutsHeld.Insert(0, donut);

            Vector3 offset = new Vector3(0, 0.25f * (m_employeeStatistics.m_donutsHeld.Count - 1), 0); // TODO: will need to change when a new model is used

            m_employeeHold = m_employeeStatistics.transform.Find("Hold").gameObject;

            donut.transform.parent = m_employeeHold.transform;
            donut.transform.position = m_employeeHold.transform.position + offset;

            m_employeeStatistics.m_donutTypeHeld = "u";
        }        

        m_donutCounter.m_donuts.Remove(donut);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Employee"))
        {
            StartCoroutine(Timer(1f));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (m_playerNear)
            {
                if (m_donutCounter.m_donuts.Count > 0 && m_playerStatistics.m_donutsHeld.Count < (m_playerStatistics.m_holdLevel * 2))
                {
                    if (m_playerStatistics.m_donutTypeHeld == "n" || m_playerStatistics.m_donutTypeHeld == "u")
                    {
                        m_playerNear = false;
                        StartCoroutine(Timer(1f));
                        DonutPickup(true);
                    }
                }
            }
        }

        if (other.CompareTag("Employee"))
        {
            m_employeeStatistics = other.GetComponent<EmployeeStatistics>();

            if (m_playerNear)
            {
                if (m_donutCounter.m_donuts.Count > 0)
                {
                    if (m_employeeStatistics.m_donutsHeld.Count < (m_employeeStatistics.m_holdLevel * 2))
                    {
                        if (m_employeeStatistics.m_donutTypeHeld == "n" || m_employeeStatistics.m_donutTypeHeld == "u")
                        {
                            m_playerNear = false;
                            StartCoroutine(Timer(1f));
                            DonutPickup(false);
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
            StopCoroutine(Timer(1f));
        }
    }

    IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        m_playerNear = true;
    }
}
