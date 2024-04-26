using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text m_moneyText;
    [SerializeField] TMP_Text[] m_playerUpgrades;
    [SerializeField] TMP_Text[] m_employeeUpgrades;

    // upgrades

    [SerializeField] GameObject m_upgradeMenu;
    [SerializeField] GameObject[] m_upgradeScreens;
    public bool m_playerScreenActive;

    PlayerStatistics m_playerStatistics;
    EmployeeStatistics m_employeeStatistics;

    private void Awake()
    {
        //FirstUpgradeUI();
    }

    private void Start()
    {
        m_playerScreenActive = true;
        m_playerStatistics = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatistics>();
        m_employeeStatistics = GameObject.FindGameObjectWithTag("Employee").GetComponent<EmployeeStatistics>();
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
        UpdateUpgradeUI("Walk", m_playerStatistics.m_walkLevel, true);
        UpdateUpgradeUI("Hold", m_playerStatistics.m_holdLevel, true);
        UpdateUpgradeUI("Walk", m_employeeStatistics.m_walkLevel, false);
        UpdateUpgradeUI("Walk", m_employeeStatistics.m_holdLevel, false);
    }

    public void UpdateUpgradeUI(string upgrade, int level, bool player)
    {
        switch (upgrade)
        {
            case "Walk":

                if (player)
                {
                    m_playerUpgrades[0].text = "Level " + level;
                }
                else
                {
                    m_employeeUpgrades[0].text = "Level " + level;
                }

            break;

            case "Hold":

                if (player)
                {
                    m_playerUpgrades[1].text = "Level " + level;
                }
                else
                {
                    m_employeeUpgrades[1].text = "Level " + level;
                }

                break;
        }
    }
}
