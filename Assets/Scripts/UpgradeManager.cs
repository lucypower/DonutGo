using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    UIManager m_uiManager;

    private void Start()
    {
        m_uiManager = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_uiManager.UpgradeMenu(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_uiManager.UpgradeMenu(false);
        }
    }
}
