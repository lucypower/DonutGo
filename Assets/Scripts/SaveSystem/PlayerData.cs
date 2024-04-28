using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int m_money;
    public int m_movementSpeed;
    public int m_holdCapacity;
    public bool m_firstTimeSave;

    // upgrades

    public int m_walkLevel;
    public int m_holdLevel;

    public PlayerData(PlayerStatistics player)
    {
        m_money = player.m_money;
        m_movementSpeed = player.m_movementSpeed;
        m_holdCapacity = player.m_holdCapacity;
        m_firstTimeSave = player.m_firstTimeSave;

        m_walkLevel = player.m_walkLevel;
        m_holdLevel = player.m_holdLevel;
    }
}
