using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // customer 

    [SerializeField] GameObject m_spawnLocation;
    [SerializeField] GameObject m_counter;
    public GameObject m_customer;

    public List<GameObject> m_spawnedCustomers;

    // order counter

    CounterTrigger m_counterTrigger;

    // player

    PlayerStatistics m_player;
    [HideInInspector] public int m_firstTime;

    // employee

    public List<EmployeeStatistics> m_spawnedEmployees;
    [SerializeField] private GameObject m_employee;

    public EmployeeStatistics m_debugEmployee;
    [SerializeField] UpgradeManager m_upgradeManager;

    private void Start()
    {
        m_counterTrigger = m_counter.GetComponentInChildren<CounterTrigger>();
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerStatistics>();

        int hasPlayed = PlayerPrefs.GetInt("m_firstTime");

        if (hasPlayed == 0)
        {
            Debug.Log("first time");
            PlayerPrefs.SetInt("m_firstTime", 1);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("not first time");
            Load();

            for (int i = 0; i < m_debugEmployee.m_numOfEmployees; i++)
            {
                Instantiate(m_employee, GameObject.Find("UpgradeTrigger").transform.position, Quaternion.identity);
            }
        }

        Save();
        SpawnCustomer();
    }

    public void SpawnCustomer()
    {
        if (m_spawnedCustomers.Count < 10)
        {
            m_spawnedCustomers.Add(Instantiate(m_customer, m_spawnLocation.transform.position, Quaternion.identity));
            m_counterTrigger.UpdateCustomerList(m_spawnedCustomers.Last());
        }        

        StartCoroutine(SpawnTimer(11 - m_upgradeManager.m_customerCounterLevel));
    }

    public void SpawnEmployee()
    {
        Instantiate(m_employee, GameObject.Find("UpgradeTrigger").transform.position, Quaternion.identity);
    }

    public void Save()
    {
        if (!m_player.m_firstTimeSave)
        {
            m_player.m_firstTimeSave = true;
        }

        SaveSystem.SavePlayer(m_player, m_debugEmployee, m_upgradeManager);
        Debug.Log("Save");
        StartCoroutine(SaveTimer(5));
    }

    public void Load()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        m_player.m_money = data.m_money;
        m_player.m_walkLevel = data.m_playerWalkLevel;
        m_player.m_holdLevel = data.m_playerHoldLevel;
        m_player.m_firstTimeSave = data.m_firstTimeSave;

        m_debugEmployee.m_walkLevel = data.m_employeeWalkLevel;
        m_debugEmployee.m_holdLevel = data.m_employeeHoldLevel;
        m_debugEmployee.m_numOfEmployees = data.m_numOfEmployees;

        m_upgradeManager.m_donutCounterLevel = data.m_donutCounterLevel;
        m_upgradeManager.m_donutCapacityLevel = data.m_donutCapacityLevel;
        m_upgradeManager.m_donutSpawnTimeLevel = data.m_donutSpawnTimeLevel;

        m_upgradeManager.m_customerCounterLevel = data.m_customerCounterLevel;

        m_upgradeManager.m_cookingLevel = data.m_cookingLevel;
        m_upgradeManager.m_cookingCapacityLevel = data.m_cookingCapacityLevel;
        m_upgradeManager.m_cookingSpawnTimeLevel = data.m_cookingSpawnTimeLevel;

        m_upgradeManager.m_icingLevel = data.m_icingLevel;
        m_upgradeManager.m_icingCapacityLevel = data.m_icingCapacityLevel;
        m_upgradeManager.m_icingSpawnTimeLevel = data.m_icingSpawnTimeLevel;
    }

    public IEnumerator SpawnTimer(float time)
    {
        yield return new WaitForSeconds(time);
        SpawnCustomer();
    }

    public IEnumerator SaveTimer(float time)
    {
        yield return new WaitForSeconds(time);
        Save();
    }


    // TODO : Need way to reset game, probably button in settings menu
}
