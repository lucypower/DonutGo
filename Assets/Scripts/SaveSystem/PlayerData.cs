using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    //player

    public int m_money;
    public int m_playerWalkLevel;
    public int m_playerHoldLevel;
    public bool m_firstTimeSave;

    // employee

    public int m_employeeWalkLevel;
    public int m_employeeHoldLevel;
    public int m_numOfEmployees;

    // counter upgrades

    public int m_donutCounterLevel;
    public int m_donutCapacityLevel;
    public int m_donutSpawnTimeLevel;

    public PlayerData(PlayerStatistics player, EmployeeStatistics employee, UpgradeManager upgrade)
    {
        // player

        m_money = player.m_money;
        m_playerWalkLevel = player.m_walkLevel;
        m_playerHoldLevel = player.m_holdLevel;
        m_firstTimeSave = player.m_firstTimeSave;

        // employee

        m_employeeWalkLevel = employee.m_walkLevel;
        m_employeeHoldLevel = employee.m_holdLevel;
        m_numOfEmployees = employee.m_numOfEmployees;

        // counter upgrades

        m_donutCounterLevel = upgrade.m_donutCounterLevel;
        m_donutCapacityLevel = upgrade.m_donutCapacityLevel;
        m_donutSpawnTimeLevel = upgrade.m_donutSpawnTimeLevel;
    }
}
