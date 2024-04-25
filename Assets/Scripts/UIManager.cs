using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text m_moneyText;

    // upgrades

    [SerializeField] GameObject m_upgradeMenu;
    [SerializeField] GameObject m_playerUpgrades;
    [SerializeField] GameObject m_employeeUpgrades;

    PlayerStatistics m_playerStatistics;

    private void Start()
    {
        m_playerStatistics = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerStatistics>();
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
        m_playerUpgrades.SetActive(true);
        m_employeeUpgrades.SetActive(false);
    }

    public void EmployeeUpgrades()
    {
        m_employeeUpgrades.SetActive(true);
        m_playerUpgrades.SetActive(false);
    }
}
