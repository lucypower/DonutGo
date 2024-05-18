using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerStatistics : MonoBehaviour
{
    public List<GameObject> m_donutsHeld;
    public string m_donutTypeHeld;

    public int m_maxDonuts;


    public int m_money;
    public int m_gems;

    // upgrades

    public int m_walkLevel;
    public int m_holdLevel;
    public float m_profitLevel;

    public bool m_firstTimeSave;
    public bool m_broughtAdFree;

    public bool m_xProfitActive;

    private void Awake()
    {
        m_gems = 1000;
    }

    private void Update()
    {
        m_maxDonuts = m_holdLevel * 2;
    }

    public void BuyAdFree()
    {
        m_broughtAdFree = true;
    }

    public void DebugNoAddFree() // TODO: DEBUG
    {
        m_broughtAdFree = false;
    }
}
