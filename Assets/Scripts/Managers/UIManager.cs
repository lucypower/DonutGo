using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    PlayerStatistics m_playerStatistics;
    GameManager m_gameManager;
    UpgradeManager m_upgradeManager;
    [SerializeField] UpgradeShop m_upgradeShop;

    [Header("General Shop")]

    [SerializeField] TMP_Text m_moneyText;
    [SerializeField] TMP_Text m_profitText;
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

    [Header("Shop Buttons")]

    [SerializeField] Button m_playerButton;
    [SerializeField] Button m_employeeButton;

    private void Start()
    {
        m_playerScreenActive = true;
        m_playerStatistics = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatistics>();
        m_gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        m_upgradeManager = GameObject.FindGameObjectWithTag("UpgradeManager").GetComponent<UpgradeManager>();

        m_playerButton.GetComponent<Image>().color = Color.gray;

        FirstUpgradeUI();
    }

    private void Update()
    {
        m_moneyText.text = "Money : " + m_playerStatistics.m_money;

        if (m_playerStatistics.m_xProfitActive)
        {
            m_profitText.text = "x" + ((1 + (m_playerStatistics.m_profitLevel / 10)) * 2);
        }
        else
        {
            m_profitText.text = "x" + (1 + (m_playerStatistics.m_profitLevel / 10));
        }        
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

        m_playerButton.GetComponent<Image>().color = Color.gray;
        m_employeeButton.GetComponent<Image>().color = Color.white;
    }

    public void EmployeeUpgrades()
    {
        m_upgradeScreens[1].SetActive(true);
        m_upgradeScreens[0].SetActive(false);
        m_playerScreenActive = false;

        m_playerButton.GetComponent<Image>().color = Color.white;
        m_employeeButton.GetComponent<Image>().color = Color.gray;
    }

    public void FirstUpgradeUI()
    {
        UpdateShopUI("Walk", m_playerStatistics.m_walkLevel, true);
        UpdateShopUI("Hold", m_playerStatistics.m_holdLevel, true);
        UpdateShopUI("Walk", m_gameManager.m_debugEmployee.m_walkLevel, false);
        UpdateShopUI("Hold", m_gameManager.m_debugEmployee.m_holdLevel, false);
        UpdateShopUI("Profit", (int)m_playerStatistics.m_profitLevel, true);
        UpdateShopUI("Employee", 0, false);

        UpdateUpgradeUI("Donut", m_upgradeManager.m_donutCounterLevel, m_upgradeManager.m_donutCounterLevel * 1000);
        UpdateUpgradeUI("Customer", m_upgradeManager.m_customerCounterLevel, m_upgradeManager.m_customerCounterLevel * 1000);
        UpdateUpgradeUI("Cooking", m_upgradeManager.m_cookingLevel, m_upgradeManager.m_cookingLevel * 1000);
        UpdateUpgradeUI("Icing", m_upgradeManager.m_icingLevel, m_upgradeManager.m_icingLevel * 1000);
    }

    public void UpdateShopUI(string upgrade, int level, bool player)
    {
        m_upgradeShop.CalculateCost();

        switch (upgrade)
        {
            case "Walk":

                if (player)
                {
                    m_playerUpgradeLevel[0].text = "Level " + level;
                    m_playerUpgradeCost[0].text = "Costs " + (m_upgradeShop.m_upgradeCosts[0]);

                    if (level == 5)
                    {
                        m_playerUpgradeCost[0].text = "Fully Upgraded!";
                    }
                }
                else
                {
                    m_employeeUpgradeLevel[0].text = "Level " + level;
                    m_employeeUpgradeCost[0].text = "Costs " + (m_upgradeShop.m_upgradeCosts[3]);

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
                    m_playerUpgradeCost[1].text = "Costs " + (m_upgradeShop.m_upgradeCosts[1]);

                    if (level == 5)
                    {
                        m_playerUpgradeCost[1].text = "Fully Upgraded!";
                    }
                }
                else
                {
                    m_employeeUpgradeLevel[1].text = "Level " + level;
                    m_employeeUpgradeCost[1].text = "Costs " + (m_upgradeShop.m_upgradeCosts[4]);

                    if (level == 5)
                    {
                        m_employeeUpgradeCost[1].text = "Fully Upgraded!";
                    }
                }

            break;

            case "Profit":

                m_playerUpgradeLevel[2].text = "Level " + level;
                m_playerUpgradeCost[2].text = "Costs " + m_upgradeShop.m_upgradeCosts[2];

                if (level == 10)
                {
                    m_playerUpgradeCost[2].text = "Fully Upgraded!";
                }

                break;

            case "Employee":

                m_employeeUpgradeCost[2].text = "Costs " + m_upgradeShop.m_upgradeCosts[5];

                break;
        }
    }

    public void UpdateUpgradeUI(string station, int level, int money)
    {
        switch (station)
        {
            case "Donut":

                m_upgradeText[0].text = "Level " + level + "\nCosts " + money;

                if (level == 10)
                {
                    m_upgradeText[0].text = "Max Upgrades";
                }

                break;

            case "Customer":

                m_upgradeText[1].text = "Level " + level + "\nCosts " + money;

                if (level == 10)
                {
                    m_upgradeText[1].text = "Max Upgrades";
                }

                break;

            case "Cooking":

                m_upgradeText[2].text = "Level " + level + "\nCosts " + money;

                if (level == 10)
                {
                    m_upgradeText[2].text = "Max Upgrades";
                }

                break;

            case "Icing":

                m_upgradeText[3].text = "Level " + level + "\nCosts " + money;

                if (level == 10)
                {
                    m_upgradeText[3].text = "Max Upgrades";
                }

                break;
        }
    }
}
