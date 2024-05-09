using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTrigger : MonoBehaviour
{
    GameManager m_gameManager;
    PlayerStatistics m_playerStatistics;
    UpgradeManager m_upgradeManager;
    UIManager m_uiManager;

    private bool m_playerNear;
    [SerializeField] private string m_station;



    private void Start()
    {
        m_upgradeManager = GameObject.FindGameObjectWithTag("UpgradeManager").GetComponent<UpgradeManager>();
        m_uiManager = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>();
        m_gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
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
            if (m_playerNear)
            {
                switch (m_station)
                {
                    case "Donut":

                        if (m_upgradeManager.m_donutCounterLevel < 10)
                        {
                            if (m_playerStatistics.m_money >= m_upgradeManager.m_donutCounterLevel * 1000)
                            {
                                m_playerStatistics.m_money -= m_upgradeManager.m_donutCounterLevel * 1000;
                                m_upgradeManager.m_donutCounterLevel++;

                                if (m_upgradeManager.m_donutCounterLevel % 2 == 0)
                                {
                                    m_upgradeManager.m_donutCapacityLevel++;
                                }
                                else
                                {
                                    m_upgradeManager.m_donutSpawnTimeLevel++;
                                }

                                int money = m_upgradeManager.m_donutCounterLevel * 1000;

                                m_uiManager.UpdateUpgradeUI(m_station, m_upgradeManager.m_donutCounterLevel, money);

                                if (m_gameManager.m_hapticOn)
                                {
                                    Handheld.Vibrate();
                                }

                                m_playerNear = false;
                                StartCoroutine(Timer(2f));
                            }
                        }

                    break;

                    case "Customer":

                        if (m_upgradeManager.m_customerCounterLevel < 10)
                        {
                            if (m_playerStatistics.m_money >= m_upgradeManager.m_customerCounterLevel * 1000)
                            {
                                m_playerStatistics.m_money -= m_upgradeManager.m_customerCounterLevel * 1000;  
                                m_upgradeManager.m_customerCounterLevel++;

                                int money = m_upgradeManager.m_customerCounterLevel * 1000;

                                m_uiManager.UpdateUpgradeUI(m_station, m_upgradeManager.m_customerCounterLevel, money);

                                if (m_gameManager.m_hapticOn)
                                {
                                    Handheld.Vibrate();
                                }

                                m_playerNear = false;
                                StartCoroutine(Timer(2f));

                            }
                        }

                    break;

                    case "Cooking":

                        if (m_upgradeManager.m_cookingLevel < 10)
                        {
                            if (m_playerStatistics.m_money >= m_upgradeManager.m_cookingLevel * 1000)
                            {
                                m_playerStatistics.m_money -= m_upgradeManager.m_cookingLevel * 1000;
                                m_upgradeManager.m_cookingLevel++;

                                if (m_upgradeManager.m_cookingLevel % 2 == 0)
                                {
                                    m_upgradeManager.m_cookingCapacityLevel++;
                                }
                                else
                                {
                                    m_upgradeManager.m_cookingSpawnTimeLevel++;
                                }

                                int money = m_upgradeManager.m_cookingLevel * 1000;

                                m_uiManager.UpdateUpgradeUI(m_station, m_upgradeManager.m_cookingLevel, money);

                                if (m_gameManager.m_hapticOn)
                                {
                                    Handheld.Vibrate();
                                }

                                m_playerNear = false;
                                StartCoroutine(Timer(2f));
                            }
                        }

                        break;

                    case "Icing":

                        if (m_upgradeManager.m_icingLevel < 10)
                        {
                            if (m_playerStatistics.m_money >= m_upgradeManager.m_icingLevel * 1000)
                            {
                                m_playerStatistics.m_money -= m_upgradeManager.m_icingLevel * 1000;
                                m_upgradeManager.m_icingLevel++;

                                if (m_upgradeManager.m_icingLevel % 2 == 0)
                                {
                                    m_upgradeManager.m_icingCapacityLevel++;
                                }
                                else
                                {
                                    m_upgradeManager.m_icingSpawnTimeLevel++;
                                }

                                int money = m_upgradeManager.m_icingLevel * 1000;

                                m_uiManager.UpdateUpgradeUI(m_station, m_upgradeManager.m_icingLevel, money);

                                if (m_gameManager.m_hapticOn)
                                {
                                    Handheld.Vibrate();
                                }

                                m_playerNear = false;
                                StartCoroutine(Timer(2f));
                            }
                        }

                    break;

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
