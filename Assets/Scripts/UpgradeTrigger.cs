using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTrigger : MonoBehaviour
{
    DonutCounter m_donutCounter;
    PlayerStatistics m_playerStatistics;
    UpgradeManager m_upgradeManager;

    private bool m_playerNear;
    [SerializeField] private string m_station;

    bool tempBool; // TODO: Temp bool

    private void Start()
    {
        m_upgradeManager = GameObject.FindGameObjectWithTag("UpgradeManager").GetComponent<UpgradeManager>();
        m_donutCounter = GetComponentInParent<DonutCounter>();
        m_playerStatistics = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatistics>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter");

        if (other.CompareTag("Player"))
        {
            StartCoroutine(Timer(2f));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (m_station)
            {
                case "Donut":

                    if (m_playerNear)
                    {
                        if (m_upgradeManager.m_donutCounterLevel < 10)
                        {
                            if (m_playerStatistics.m_money >= m_upgradeManager.m_donutCounterLevel * 1000)
                            {
                                m_playerStatistics.m_money -= m_upgradeManager.m_donutCounterLevel * 1000;
                                m_upgradeManager.m_donutCounterLevel++;

                                if (m_upgradeManager.m_donutCounterLevel % 2 == 0)
                                {
                                    Debug.Log("upgrade capacity");
                                    m_upgradeManager.m_donutCapacityLevel++;
                                }
                                else
                                {
                                    Debug.Log("upgrade spawn time");
                                    m_upgradeManager.m_donutSpawnTimeLevel++;
                                }

                                m_playerNear = false;
                                StartCoroutine(Timer(2f));
                            }
                        }
                    }

                break;
            }






            if (!tempBool)
            {
                if (m_playerNear)
                {
                    if (m_playerStatistics.m_money >= 1000)
                    {
                        tempBool = true;
                        m_playerStatistics.m_money -= 1000;
                        m_donutCounter.m_maxDonuts *= 2;
                    }
                }
            }
        }
    }

    IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        m_playerNear = true;
    }
}
