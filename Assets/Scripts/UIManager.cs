using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text m_moneyText;

    // upgrades

    [SerializeField] GameObject m_upgradeMenu;
    [SerializeField] GameObject[] m_upgradeScreens;

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
        m_upgradeScreens[0].SetActive(true);
        m_upgradeScreens[1].SetActive(false);
    }

    public void EmployeeUpgrades()
    {
        m_upgradeScreens[1].SetActive(true);
        m_upgradeScreens[0].SetActive(false);
    }
}
