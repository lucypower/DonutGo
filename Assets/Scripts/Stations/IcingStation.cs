using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcingStation : MonoBehaviour
{
    UpgradeManager m_upgradeManager;

    public List<GameObject> m_nonIcedDonuts;
    public List<GameObject> m_icedDonuts;

    [SerializeField] private Material[] m_icing;
    [SerializeField] private Transform m_icedDonutHold;

    [SerializeField] private GameObject m_donut;

    public bool m_iceNext;
    private int m_maxDonuts;

    private void Start()
    {
        m_upgradeManager = GameObject.FindGameObjectWithTag("UpgradeManager").GetComponent<UpgradeManager>();
    }

    private void Update()
    {
        if (m_upgradeManager.m_icingCapacityLevel == 0)
        {
            m_maxDonuts = m_upgradeManager.m_icingCapacityLevel + 2;
        }
        else
        {
            m_maxDonuts = m_upgradeManager.m_icingCapacityLevel * 2;
        }

        if (m_iceNext && m_nonIcedDonuts.Count > 0)
        {
            if (m_icedDonuts.Count < m_maxDonuts)
            {
                IceDonut();
                RestartCoroutine();
            }
        }
    }

    public void IceDonut()
    {
        int donutNo = m_nonIcedDonuts.Count - 1;
        GameObject donut = m_nonIcedDonuts[donutNo];

        Vector3 offset = new Vector3(0, 0.2f * (m_icedDonuts.Count - 1), 0);

        GameObject newDonut = Instantiate(m_donut, m_icedDonutHold.position + offset, Quaternion.identity);
        m_icedDonuts.Add(newDonut);
        newDonut.transform.parent = m_icedDonutHold;

        m_nonIcedDonuts.Remove(donut);
        Destroy(donut);


        //int donutNo = m_nonIcedDonuts.Count - 1;
        //GameObject donut = m_nonIcedDonuts[donutNo];

        //m_icedDonuts.Add(donut);

        //Vector3 offset = new Vector3(0, 0.25f * (m_icedDonuts.Count - 1), 0);

        //int random = Random.Range(0, 3);

        //donut.GetComponent<Renderer>().material = m_icing[random];
        //donut.transform.parent = m_icedDonutHold;
        //donut.transform.position = m_icedDonutHold.position + offset; 
        
        //m_nonIcedDonuts.Remove(donut);
    }

    IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        m_iceNext = true;
    }

    public void RestartCoroutine()
    {
        m_iceNext = false;
        StartCoroutine(Timer(6 - m_upgradeManager.m_icingSpawnTimeLevel));
    }
}
