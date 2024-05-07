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

    public int m_customerCounterLevel;

    // cooking counter

    public int m_cookingLevel;
    public int m_cookingCapacityLevel;
    public int m_cookingSpawnTimeLevel;

    // icing counter

    public int m_icingLevel;
    public int m_icingCapacityLevel;
    public int m_icingSpawnTimeLevel;

    // rewards

    public string m_lastDClaimTime;
    public string m_lastHClaimTime;
    public bool m_todayClaimed;
    public bool m_hourClaimed;

    // adverts

    public bool m_broughtAdFree;

    public PlayerData(PlayerStatistics player, EmployeeStatistics employee, UpgradeManager upgrade, DailyRewards daily, HourlyRewards hourly)
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

        m_customerCounterLevel = upgrade.m_customerCounterLevel;

        m_cookingLevel = upgrade.m_cookingLevel;
        m_cookingCapacityLevel = upgrade.m_cookingCapacityLevel;
        m_cookingSpawnTimeLevel = upgrade.m_cookingSpawnTimeLevel;

        m_icingLevel = upgrade.m_icingLevel;
        m_icingCapacityLevel = upgrade.m_icingCapacityLevel;
        m_icingSpawnTimeLevel = upgrade.m_icingSpawnTimeLevel;

        // rewards system

        m_lastDClaimTime = daily.m_lastClaimTime;
        m_todayClaimed = daily.m_todayClaimed;

        m_lastHClaimTime = hourly.m_lastClaimTime;
        m_hourClaimed = hourly.m_hourClaimed;

        // adverts

        m_broughtAdFree = player.m_broughtAdFree;
    }
}
