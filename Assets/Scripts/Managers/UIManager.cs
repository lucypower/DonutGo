using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    PlayerStatistics m_playerStatistics;
    GameManager m_gameManager;
    UpgradeManager m_upgradeManager;

    [Header("General Shop")]

    [SerializeField] TMP_Text m_moneyText;
    [SerializeField] GameObject m_upgradeMenu;
    [SerializeField] GameObject[] m_upgradeScreens;
    public bool m_playerScreenActive;
    
    [Header("Shop Upgrades")]

    [SerializeField] TMP_Text[] m_playerUpgradeLevel;
    [SerializeField] TMP_Text[] m_employeeUpgradeLevel;
    [SerializeField] TMP_Text[] m_playerUpgradeCost;
    [SerializeField] TMP_Text[] m_employeeUpgradeCost;

    [Header("Station Upgrades")]

    [SerializeField] TMP_Text[] m_upgradeText;


    private void Start()
    {
        m_playerScreenActive = true;
        m_playerStatistics = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatistics>();
        m_gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        m_upgradeManager = GameObject.FindGameObjectWithTag("UpgradeManager").GetComponent<UpgradeManager>();

        FirstUpgradeUI();
    }

    private void Update()
    {
        m_moneyText.text = "Money : " + m_playerStatistics.m_money;
    }

    public void UpgradeMenu(bool showMenu)
    {
        switch (showMenu)
        {
            case true:

                m_upgradeMenu.SetActive(true);

            break;

            case false:

                m_upgradeMenu.SetActive(false);

            break;
        }
    }

    public void PlayerUpgrades()
    {
        m_upgradeScreens[0].SetActive(true);
        m_upgradeScreens[1].SetActive(false);
        m_playerScreenActive = true;
    }

    public void EmployeeUpgrades()
    {
        m_upgradeScreens[1].SetActive(true);
        m_upgradeScreens[0].SetActive(false);
        m_playerScreenActive = false;
    }

    public void FirstUpgradeUI()
    {
        UpdateShopUI("Walk", m_playerStatistics.m_walkLevel, true);
        UpdateShopUI("Hold", m_playerStatistics.m_holdLevel, true);
        UpdateShopUI("Walk", m_gameManager.m_debugEmployee.m_walkLevel, false);
        UpdateShopUI("Hold", m_gameManager.m_debugEmployee.m_holdLevel, false);

        UpdateUpgradeUI("Donut", m_upgradeManager.m_donutCounterLevel, m_upgradeManager.m_donutCounterLevel * 1000);
        UpdateUpgradeUI("Customer", m_upgradeManager.m_customerCounterLevel, m_upgradeManager.m_customerCounterLevel * 1000);
    }

    public void UpdateShopUI(string upgrade, int level, bool player)
    {
        switch (upgrade)
        {
            case "Walk":

                if (player)
                {
                    m_playerUpgradeLevel[0].text = "Level " + level;
                    m_playerUpgradeCost[0].text = "Costs " + (Mathf.Pow(m_playerStatistics.m_walkLevel, 3) * 100);

                    if (level == 5)
                    {
                        m_playerUpgradeCost[0].text = "Fully Upgraded!";
                    }
                }
                else
                {
                    m_employeeUpgradeLevel[0].text = "Level " + level;
                    m_employeeUpgradeCost[0].text = "Costs " + (Mathf.Pow(m_gameManager.m_debugEmployee.m_walkLevel, 3) * 100);

                    if (level == 5)
                    {
                        m_employeeUpgradeCost[0].text = "Fully Upgraded!";
                    }
                }

            break;

            case "Hold":

                if (player)
                {
                    m_playerUpgradeLevel[1].text = "Level " + level;
                    m_playerUpgradeCost[1].text = "Costs " + (Mathf.Pow(m_playerStatistics.m_holdLevel, 3) * 100);

                    if (level == 5)
                    {
                        m_playerUpgradeCost[1].text = "Fully Upgraded!";
                    }
                }
                else
                {
                    m_employeeUpgradeLevel[1].text = "Level " + level;
                    m_employeeUpgradeCost[1].text = "Costs " + (Mathf.Pow(m_gameManager.m_debugEmployee.m_holdLevel, 3) * 100);

                    if (level == 5)
                    {
                        m_employeeUpgradeCost[1].text = "Fully Upgraded!";
                    }
                }

            break;
        }
    }

    public void UpdateUpgradeUI(string station, int level, int money)
    {
        switch (station)
        {
            case "Donut":

                m_upgradeText[0].text = "Level " + level + "\nCosts " + money;

                break;

            case "Customer":

                m_upgradeText[1].text = "Level " + level + "\nCosts " + money;

                break;
        }
    }
}
