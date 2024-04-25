using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    UIManager m_uiManager;
    PlayerStatistics m_playerStatistics;

    private void Start()
    {
        m_uiManager = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>();
        m_playerStatistics = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatistics>();
    }

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

    public void UpgradeWalk()
    {

    }

    public void UpgradeHold()
    {

    }
}
