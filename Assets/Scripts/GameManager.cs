using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // customer spawn

    [SerializeField] GameObject m_spawnLocation;
    [SerializeField] GameObject m_customer;
    public List<GameObject> m_spawnedCustomers;

    private void Start()
    {
        SpawnCustomer();
    }

    public void SpawnCustomer()
    {
        m_spawnedCustomers.Add(Instantiate(m_customer, m_spawnLocation.transform.position, Quaternion.identity));

        StartCoroutine(SpawnTimer(5));
    }

    public IEnumerator SpawnTimer(float time)
    {
        yield return new WaitForSeconds(time);
        SpawnCustomer();
    }
}
