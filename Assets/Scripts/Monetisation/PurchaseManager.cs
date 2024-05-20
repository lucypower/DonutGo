using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PurchaseManager : MonoBehaviour
{
    [SerializeField] PlayerStatistics m_playerStats;
    [SerializeField] GameObject m_collectionScreen;
    [SerializeField] TextMeshProUGUI m_collectionText;

    #region Gems

    public void Gems10()
    {
        m_playerStats.m_gems += 10;
    }
    public void Gems50()
    {
        m_playerStats.m_gems += 50;

    }
    public void Gems100()
    {
        m_playerStats.m_gems += 100;

    }
    public void Gems500()
    {
        m_playerStats.m_gems += 500;
    }
    public void Gems1000()
    {
        m_playerStats.m_gems += 1000;

    }
    public void Gems5000()
    {
        m_playerStats.m_gems += 5000;
    }

    public void PurchaseFailed()
    {
        Debug.Log("purchase failed");
    }

    #endregion

    #region Money

    public void Money1000()
    {
        if (m_playerStats.m_gems >= 150)
        {
            m_playerStats.m_money += 1000;
            m_playerStats.m_gems -= 150;

            m_collectionScreen.SetActive(true);
            m_collectionText.text = "You've just brought " + 1000 + " money";
        }
        else
        {
            m_collectionScreen.SetActive(true);
            m_collectionText.text = "You do not have enough gems!";
        }
    }
    public void Money2000()
    {
        if (m_playerStats.m_gems >= 400)
        {
            m_playerStats.m_money += 2000;
            m_playerStats.m_gems -= 400;

            m_collectionScreen.SetActive(true);
            m_collectionText.text = "You've just brought " + 2000 + " money";
        }
        else
        {
            m_collectionScreen.SetActive(true);
            m_collectionText.text = "You do not have enough gems!";
        }
    }
    public void Money4000()
    {
        if (m_playerStats.m_gems >= 900)
        {
            m_playerStats.m_money += 4000;
            m_playerStats.m_gems -= 900;

            m_collectionScreen.SetActive(true);
            m_collectionText.text = "You've just brought " + 4000 + " money";
        }
        else
        {
            m_collectionScreen.SetActive(true);
            m_collectionText.text = "You do not have enough gems!";
        }
    }
    public void Money5000()
    {
        if (m_playerStats.m_gems >= 1150)
        {
            m_playerStats.m_money += 5000;
            m_playerStats.m_gems -= 1150;

            m_collectionScreen.SetActive(true);
            m_collectionText.text = "You've just brought " + 5000 + " money";
        }
        else
        {
            m_collectionScreen.SetActive(true);
            m_collectionText.text = "You do not have enough gems!";
        }
    }
    public void Money7500()
    {
        if (m_playerStats.m_gems >= 1775)
        {
            m_playerStats.m_money += 7500;
            m_playerStats.m_gems -= 1775;

            m_collectionScreen.SetActive(true);
            m_collectionText.text = "You've just brought " + 7500 + " money";
        }
        else
        {
            m_collectionScreen.SetActive(true);
            m_collectionText.text = "You do not have enough gems!";
        }
    }
    public void Money10000()
    {
        if (m_playerStats.m_gems >= 2400)
        {
            m_playerStats.m_money += 10000;
            m_playerStats.m_gems -= 2400;

            m_collectionScreen.SetActive(true);
            m_collectionText.text = "You've just brought " + 10000 + " money";
        }
        else
        {
            m_collectionScreen.SetActive(true);
            m_collectionText.text = "You do not have enough gems!";
        }
    }

    #endregion
}
