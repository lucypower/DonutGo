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
        }

        Save();
        SpawnCustomer();
    }

    public void SpawnCustomer()
    {
        m_spawnedCustomers.Add(Instantiate(m_customer, m_spawnLocation.transform.position, Quaternion.identity));
        m_counterTrigger.UpdateCustomerList(m_spawnedCustomers.Last());

        StartCoroutine(SpawnTimer(5));
    }

    public void Save()
    {
        if (!m_player.m_firstTimeSave)
        {
            m_player.m_firstTimeSave = true;
        }

        SaveSystem.SavePlayer(m_player);
        Debug.Log("Save");
        StartCoroutine(SaveTimer(5));
    }

    public void Load()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        m_player.m_money = data.m_money;
        m_player.m_movementSpeed = data.m_movementSpeed;
        m_player.m_holdCapacity = data.m_holdCapacity;
        m_player.m_firstTimeSave = data.m_firstTimeSave;

        m_player.m_walkLevel = data.m_walkLevel;
        m_player.m_holdLevel = data.m_holdLevel;
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
