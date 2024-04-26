using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    UIManager m_uiManager;
    PlayerStatistics m_playerStatistics;
    EmployeeStatistics m_employeeStatistics; // TODO: Need to rewrite for multiple ai employees

    private void Start()
    {
        m_uiManager = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>();
        m_playerStatistics = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatistics>();
        m_employeeStatistics = GameObject.FindGameObjectWithTag("Employee").GetComponent<EmployeeStatistics>();
    }

    #region Trigger Enter/Exit

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_uiManager.UpgradeMenu(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_uiManager.UpgradeMenu(false);
        }
    }

    #endregion

    public void Upgrade(string upgrade)
    {
        switch (upgrade)
        {
            case "Walk":

                if (m_uiManager.m_playerScreenActive)
                {
                    if (m_playerStatistics.m_walkLevel < 5)
                    {
                        m_playerStatistics.m_walkLevel++;
                        m_uiManager.UpdateUpgradeUI(upgrade, m_playerStatistics.m_walkLevel, true);
                    }
                }
                else
                {
                    if (m_employeeStatistics.m_walkLevel < 5)
                    {
                        m_employeeStatistics.m_walkLevel++;
                        m_uiManager.UpdateUpgradeUI(upgrade, m_employeeStatistics.m_walkLevel, false);
                    }
                }

                break;

            case "Hold":

                if (m_uiManager.m_playerScreenActive)
                {
                    if (m_playerStatistics.m_holdLevel < 5)
                    {
                        m_playerStatistics.m_holdLevel++;
                        m_uiManager.UpdateUpgradeUI(upgrade, m_playerStatistics.m_holdLevel, true);
                    }
                }
                else
                {
                    if (m_employeeStatistics.m_holdLevel < 5)
                    {
                        m_employeeStatistics.m_holdLevel++;
                        m_uiManager.UpdateUpgradeUI(upgrade, m_employeeStatistics.m_holdLevel, false);
                    }
                }

                break;

            default:
                break;
        }
    }
}
