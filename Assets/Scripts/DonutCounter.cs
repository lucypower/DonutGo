using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutCounter : MonoBehaviour
{
    [SerializeField] private GameObject m_donut;
    [SerializeField] private GameObject m_spawnLocation;

    public List<GameObject> m_donuts;

    private void Start()
    {
        SpawnDonut();
    }

    public void SpawnDonut()
    {
        Vector3 offset = new Vector3(0, 0.25f * (m_donuts.Count - 1), 0);

        Instantiate(m_donut, m_spawnLocation.transform.position + offset, Quaternion.identity);

        StartCoroutine(SpawnTimer(5));
    }

    public IEnumerator SpawnTimer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        SpawnDonut();
    }
}
