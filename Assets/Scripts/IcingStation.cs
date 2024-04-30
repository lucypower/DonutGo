using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcingStation : MonoBehaviour
{
    UpgradeManager m_upgradeManager;

    public List<GameObject> m_nonIcedDonuts;
    public List<GameObject> m_icedDonuts;

    [SerializeField] private Material m_icing;
    [SerializeField] private Transform m_icedDonutHold;

    private bool m_iceNext;

    private void Update()
    {
        if (m_iceNext)
        {

        }
    }

    public void IceDonut()
    {
        int donutNo = m_nonIcedDonuts.Count - 1;
        GameObject donut = m_nonIcedDonuts[donutNo];

        m_nonIcedDonuts.Remove(donut);
        m_icedDonuts.Add(donut);

        Vector3 offset = new Vector3(0, 0.25f * (m_icedDonuts.Count - 1), 0);

        donut.GetComponent<Renderer>().material = m_icing;
        donut.transform.parent = m_icedDonutHold;
        donut.transform.position = m_icedDonutHold.position + offset; 
    }

    IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        m_iceNext = true;
    }

    public void RestartCoroutine()
    {
        m_iceNext = false;
        StartCoroutine(Timer(3));
    }
}
