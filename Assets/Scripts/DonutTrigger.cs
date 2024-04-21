using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutTrigger : MonoBehaviour
{
    PlayerStatistics m_playerStatistics;
    [SerializeField] private GameObject m_playerHold;

    DonutCounter m_donutCounter;

    private void Start()
    {
        m_playerStatistics = m_playerHold.GetComponentInParent<PlayerStatistics>();
        m_donutCounter = GetComponentInParent<DonutCounter>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // TODO: update to take holding capacity into account
        {
            if (m_donutCounter.m_donuts.Count > 0)
            {
                if (m_playerStatistics.m_donutsHeld.Count < m_playerStatistics.m_holdCapacity)
                {
                    DonutPickup();
                }
            }
        }
    }

    public void DonutPickup()
    {
        int donutToGo = m_donutCounter.m_donuts.Count - 1;
        GameObject donut = m_donutCounter.m_donuts[donutToGo];

        m_playerStatistics.m_donutsHeld.Add(donut);

        Vector3 offset = new Vector3(0, 0.25f * (m_playerStatistics.m_donutsHeld.Count - 1), 0); // TODO: will need to change when a new model is used

        donut.transform.parent = m_playerHold.transform;
        donut.transform.position = m_playerHold.transform.position + offset;

        m_donutCounter.m_donuts.Remove(donut);
    }
}
