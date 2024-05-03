using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CookingStation : MonoBehaviour
{
    UpgradeManager m_upgradeManager;

    public List<GameObject> m_uncookedDonuts;
    public List<GameObject> m_cookedDonuts;

    [SerializeField] private GameObject m_donut;

    [SerializeField] private Material m_cookedDonut;
    [SerializeField] private Transform m_cookedDonutHold;

    public bool m_cookNext;
    private int m_maxDonuts;

    // max donuts

    private void Start()
    {
        m_upgradeManager = GameObject.FindGameObjectWithTag("UpgradeManager").GetComponent<UpgradeManager>();
       
    }

    public void Update()
    {
        if (m_upgradeManager.m_cookingCapacityLevel == 0)
        {
            m_maxDonuts = m_upgradeManager.m_cookingCapacityLevel + 2;
        }
        else
        {
            m_maxDonuts = m_upgradeManager.m_cookingCapacityLevel * 2;
        }

        if (m_cookNext && m_uncookedDonuts.Count > 0)
        {
            if (m_cookedDonuts.Count < m_maxDonuts)
            {
                CookDonut();
                RestartCoroutine();
            }
        }
    }

    public void CookDonut()
    {
        int donutNo = m_uncookedDonuts.Count - 1;
        GameObject donut = m_uncookedDonuts[donutNo];

        Vector3 offset = new Vector3(0, 0.2f * (m_cookedDonuts.Count - 1), 0);

        GameObject newDonut = Instantiate(m_donut, m_cookedDonutHold.position + offset, Quaternion.identity);
        m_cookedDonuts.Add(newDonut);
        newDonut.transform.parent = m_cookedDonutHold;

        m_uncookedDonuts.Remove(donut);
        Destroy(donut);


        //int donutNo = m_uncookedDonuts.Count - 1;
        //GameObject donut = m_uncookedDonuts[donutNo];

        //m_cookedDonuts.Add(donut);

        //Vector3 offset = new Vector3(0, 0.25f * (m_cookedDonuts.Count - 1), 0);

        //donut.GetComponent<Renderer>().material = m_cookedDonut;
        //donut.transform.parent = m_cookedDonutHold;
        //donut.transform.position = m_cookedDonutHold.position + offset;
        
        //m_uncookedDonuts.Remove(donut);
    }

    IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        m_cookNext = true;
    }

    public void RestartCoroutine()
    {
        m_cookNext = false;
        StartCoroutine(Timer(6 - m_upgradeManager.m_cookingSpawnTimeLevel));
    }
}
