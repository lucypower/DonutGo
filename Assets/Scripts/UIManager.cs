using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text m_moneyText;

    PlayerStatistics m_playerStatistics;

    private void Start()
    {
        m_playerStatistics = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerStatistics>();
    }

    private void Update()
    {
        m_moneyText.text = "Money : " + m_playerStatistics.m_money;
    }
}
