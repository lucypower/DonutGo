using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseManager : MonoBehaviour
{
    [SerializeField] PlayerStatistics m_playerStats;

    public void BuyTempPurchase()
    {
        m_playerStats.m_money += 1000;
    }

    public void BuyTempPurchaseFailed()
    {
        Debug.Log("temp purchase failed");
    }

    public void BuyTempPurchase2()
    {
        m_playerStats.m_money += 5000;
    }

    public void BuyTempPurchaseFailed2()
    {
        Debug.Log("temp purchase failed");
    }
}
