using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeShop : MonoBehaviour
{
    UIManager m_uiManager;
    GameManager m_gameManager;
    PlayerStatistics m_playerStatistics;

    [SerializeField] Button[] m_upgradeButtons;

    public float[] m_upgradeCosts;

    private void Start()
    {
        m_uiManager = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>();
        m_gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        m_playerStatistics = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatistics>();

        m_upgradeCosts = new float[6];

        CalculateCost();
    }

    private void Update()
    {
        CalculateCost();

        for (int i = 0; i < m_upgradeButtons.Length; i++)
        {
            if (m_playerStatistics.m_money >= m_upgradeCosts[i])
            {
                m_upgradeButtons[i].GetComponent<Image>().color = Color.green;
            }
            else
            {
                m_upgradeButtons[i].GetComponent<Image>().color = Color.red;
            }
        }
    }

    public void CalculateCost()
    {
        if (m_playerStatistics.m_walkLevel < 5)
        {
            m_upgradeCosts[0] = Mathf.Pow(m_playerStatistics.m_walkLevel + 1, 3) * 100;
        }
        else
        {
            m_upgradeCosts[0] = 0;
        }

        if (m_playerStatistics.m_holdLevel < 5)
        {
            m_upgradeCosts[1] = Mathf.Pow(m_playerStatistics.m_holdLevel + 1, 3) * 100;
        }
        else
        {
            m_upgradeCosts[1] = 0;
        }

        if (m_playerStatistics.m_profitLevel < 10)
        {
            m_upgradeCosts[2] = Mathf.Pow(m_playerStatistics.m_profitLevel + 1, 3) * 100;
        }
        else
        {
            m_upgradeCosts[2] = 0;
        }

        if (m_gameManager.m_debugEmployee.m_walkLevel < 5)
        {
            m_upgradeCosts[3] = Mathf.Pow(m_gameManager.m_debugEmployee.m_walkLevel + 1, 3) * 100;
        }
        else
        {
            m_upgradeCosts[3] = 0;
        }

        if (m_gameManager.m_debugEmployee.m_holdLevel < 5)
        {
            m_upgradeCosts[4] = Mathf.Pow(m_gameManager.m_debugEmployee.m_holdLevel + 1, 3) * 100;
        }
        else
        {
            m_upgradeCosts[4] = 0;
        }

        if (m_gameManager.m_debugEmployee.m_numOfEmployees < 1)
        {
            m_upgradeCosts[5] = Mathf.Pow(m_gameManager.m_debugEmployee.m_numOfEmployees + 4, 3) * 100;
        }
        else
        {
            m_upgradeCosts[5] = 0;
        }
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
                        if (m_playerStatistics.m_money >= m_upgradeCosts[0])
                        {
                            RemoveMoney(m_upgradeCosts[0]);

                            m_playerStatistics.m_walkLevel++;

                            m_uiManager.UpdateShopUI(upgrade, m_playerStatistics.m_walkLevel, true);                       
                        }                        
                    }
                }
                else
                {                    
                    if (m_gameManager.m_debugEmployee.m_walkLevel < 5)
                    {
                        if (m_playerStatistics.m_money >= m_upgradeCosts[3])
                        {
                            RemoveMoney(m_upgradeCosts[3]);

                            m_gameManager.m_debugEmployee.m_walkLevel++;

                            m_uiManager.UpdateShopUI(upgrade, m_gameManager.m_debugEmployee.m_walkLevel, false);
                        }                        
                    }
                }

                break;

            case "Hold":

                if (m_uiManager.m_playerScreenActive)
                {
                    if (m_playerStatistics.m_holdLevel < 5)
                    {
                        if (m_playerStatistics.m_money >= m_upgradeCosts[1])
                        {
                            RemoveMoney(m_upgradeCosts[1]);

                            m_playerStatistics.m_holdLevel++;

                            m_uiManager.UpdateShopUI(upgrade, m_playerStatistics.m_holdLevel, true);
                        }                        
                    }
                }
                else
                {                    
                    if (m_gameManager.m_debugEmployee.m_holdLevel < 5)
                    {
                        if (m_playerStatistics.m_money >= m_upgradeCosts[4])
                        {
                            RemoveMoney(m_upgradeCosts[4]);

                            m_gameManager.m_debugEmployee.m_holdLevel++;

                            m_uiManager.UpdateShopUI(upgrade, m_gameManager.m_debugEmployee.m_holdLevel, false);
                        }                        
                    }
                }

            break;

            case "Profit":

                if (m_playerStatistics.m_profitLevel < 10)
                {
                    if (m_playerStatistics.m_money >= m_upgradeCosts[2])
                    {
                        RemoveMoney(m_upgradeCosts[2]);

                        m_playerStatistics.m_profitLevel++;

                        m_uiManager.UpdateShopUI(upgrade, (int)m_playerStatistics.m_profitLevel, true);
                    }
                }

                break;

            case "Employee":

                if (m_gameManager.m_debugEmployee.m_numOfEmployees < 1)
                {
                    if (m_playerStatistics.m_money >= m_upgradeCosts[5]) 
                    {
                        RemoveMoney(m_upgradeCosts[5]);
                        m_gameManager.m_debugEmployee.m_numOfEmployees++;
                        m_gameManager.SpawnEmployee();

                        m_uiManager.UpdateShopUI(upgrade, 0, false);
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
