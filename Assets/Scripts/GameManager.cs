using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // customer 

    [SerializeField] GameObject m_spawnLocation;
    public GameObject m_customer;
    [SerializeField] GameObject m_counter;
    public List<GameObject> m_spawnedCustomers;

    // order counter

    CounterTrigger m_counterTrigger;

    // donuts

    


    private void Start()
    {
        m_counterTrigger = m_counter.GetComponentInChildren<CounterTrigger>();

        SpawnCustomer();
    }

    public void SpawnCustomer()
    {
        m_spawnedCustomers.Add(Instantiate(m_customer, m_spawnLocation.transform.position, Quaternion.identity));
        m_counterTrigger.UpdateCustomerList(m_spawnedCustomers.Last());

        StartCoroutine(SpawnTimer(5));
    }

    public IEnumerator SpawnTimer(float time)
    {
        yield return new WaitForSeconds(time);
        SpawnCustomer();
    }
}
