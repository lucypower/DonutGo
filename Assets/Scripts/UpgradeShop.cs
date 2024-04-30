using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeShop : MonoBehaviour
{
    UIManager m_uiManager;
    GameManager m_gameManager;
    PlayerStatistics m_playerStatistics;

    private void Start()
    {
        m_uiManager = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>();
        m_gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        m_playerStatistics = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatistics>();
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

    public void Upgrade(string upgrade) // TODO: ui with red/green colour changes 
    {
        switch (upgrade) 
        {
            case "Walk":

                if (m_uiManager.m_playerScreenActive)
                {                    
                    if (m_playerStatistics.m_walkLevel < 5)
                    {
                        if (m_playerStatistics.m_money >= (Mathf.Pow(m_playerStatistics.m_walkLevel, 3) * 100))
                        {
                            RemoveMoney(Mathf.Pow(m_playerStatistics.m_walkLevel, 3) * 100);

                            m_playerStatistics.m_walkLevel++;

                            m_uiManager.UpdateUpgradeUI(upgrade, m_playerStatistics.m_walkLevel, true);                       
                        }                        
                    }
                }
                else
                {                    
                    if (m_gameManager.m_debugEmployee.m_walkLevel < 5)
                    {
                        if (m_playerStatistics.m_money >= (Mathf.Pow(m_gameManager.m_debugEmployee.m_walkLevel, 3) * 100))
                        {
                            RemoveMoney(Mathf.Pow(m_gameManager.m_debugEmployee.m_walkLevel, 3) * 100);

                            m_gameManager.m_debugEmployee.m_walkLevel++;

                            m_uiManager.UpdateUpgradeUI(upgrade, m_gameManager.m_debugEmployee.m_walkLevel, false);
                        }                        
                    }
                }

                break;

            case "Hold":

                if (m_uiManager.m_playerScreenActive)
                {
                    if (m_playerStatistics.m_holdLevel < 5)
                    {
                        if (m_playerStatistics.m_money >= (Mathf.Pow(m_playerStatistics.m_holdLevel, 3) * 100))
                        {
                            RemoveMoney(Mathf.Pow(m_playerStatistics.m_holdLevel, 3) * 100);
                            m_playerStatistics.m_holdLevel++;
                            m_uiManager.UpdateUpgradeUI(upgrade, m_playerStatistics.m_holdLevel, true);
                        }                        
                    }
                }
                else
                {                    
                    if (m_gameManager.m_debugEmployee.m_holdLevel < 5)
                    {
                        if (m_playerStatistics.m_money >= (Mathf.Pow(m_gameManager.m_debugEmployee.m_holdLevel, 3) * 100))
                        {
                            RemoveMoney(Mathf.Pow(m_gameManager.m_debugEmployee.m_holdLevel, 3) * 100);

                            m_gameManager.m_debugEmployee.m_holdLevel++;

                            m_uiManager.UpdateUpgradeUI(upgrade, m_gameManager.m_debugEmployee.m_holdLevel, false);
                        }                        
                    }
                }

            break;

            case "Employee":

                if (m_gameManager.m_debugEmployee.m_numOfEmployees < 2)
                {
                    if (m_playerStatistics.m_money >= 1000) // TODO: Adjust cost of employees
                    {
                        m_gameManager.m_debugEmployee.m_numOfEmployees++;
                        RemoveMoney(1000);
                        m_gameManager.SpawnEmployee();
                    }
                }

                break;

            default:
                break;
        }
    }

    public void RemoveMoney(float money)
    {
        m_playerStatistics.m_money -= (int)money;
    }
}
