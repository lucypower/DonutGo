using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeStatistics : MonoBehaviour
{
    public int m_walkLevel;
    public int m_holdLevel;

    public int m_numOfEmployees;

    public List<GameObject> m_donutsHeld;
    public string m_donutTypeHeld;

    public int m_maxDonuts;

    private void Update()
    {
        m_maxDonuts = m_holdLevel * 2;
    }
}
