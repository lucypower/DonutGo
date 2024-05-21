using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] Button m_adButton;
    private TextMeshProUGUI m_buttonText;

    private void Awake()
    {
        m_gems = 1000;
        m_buttonText = m_adButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        m_maxDonuts = m_holdLevel * 2;
    }

    public void BuyAdFree()
    {
        m_broughtAdFree = true;
        m_adButton.interactable = false;
        m_buttonText.text = "Purchased!";
    }

    public void DebugNoAddFree() // TODO: DEBUG
    {
        m_broughtAdFree = false;
    }
}
