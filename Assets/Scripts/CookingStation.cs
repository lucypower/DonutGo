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

    [SerializeField] private Material m_cookedDonut;
    [SerializeField] private Transform m_cookedDonutHold;

    public bool m_cookNext;

    // max donuts

    private void Start()
    {
        m_upgradeManager = GameObject.FindGameObjectWithTag("UpgradeManager").GetComponent<UpgradeManager>();
       
    }

    public void Update()
    {
        if (m_cookNext && m_uncookedDonuts.Count > 0)
        {
            CookDonut();
            RestartCoroutine();
        }
    }

    public void CookDonut()
    {
        int donutNo = m_uncookedDonuts.Count - 1;
        GameObject donut = m_uncookedDonuts[donutNo];

        m_uncookedDonuts.Remove(donut);
        m_cookedDonuts.Add(donut);

        Vector3 offset = new Vector3(0, 0.25f * (m_cookedDonuts.Count - 1), 0);

        donut.GetComponent<Renderer>().material = m_cookedDonut;
        donut.transform.parent = m_cookedDonutHold;
        donut.transform.position = m_cookedDonutHold.position + offset;
    }

    IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        m_cookNext = true;
    }

    public void RestartCoroutine()
    {
        m_cookNext = false;
        StartCoroutine(Timer(3));
    }
}
