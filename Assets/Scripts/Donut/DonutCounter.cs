using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutCounter : MonoBehaviour
{
    UpgradeManager m_upgradeManager;

    [SerializeField] private GameObject m_donut;
    [SerializeField] private GameObject m_spawnLocation;

    public int m_maxDonuts;

    public List<GameObject> m_donuts;

    private void Start()
    {
        m_upgradeManager = GameObject.FindGameObjectWithTag("UpgradeManager").GetComponent<UpgradeManager>();

        SpawnDonut();
    }

    private void Update()
    {
        if (m_upgradeManager.m_donutCapacityLevel == 0)
        {
            m_maxDonuts = m_upgradeManager.m_donutCapacityLevel + 2;
        }
        else
        {
            m_maxDonuts = m_upgradeManager.m_donutCapacityLevel * 2;
        }
    }

    public void SpawnDonut()
    {
        Vector3 offset = new Vector3(0, 0.25f * (m_donuts.Count - 1), 0);

        if (m_donuts.Count < m_maxDonuts)
        {
            Instantiate(m_donut, m_spawnLocation.transform.position + offset, Quaternion.identity);

        }

        StartCoroutine(SpawnTimer(2));
    }

    public IEnumerator SpawnTimer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        SpawnDonut();
    }
}
