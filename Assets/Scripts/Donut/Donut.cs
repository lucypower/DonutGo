using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donut : MonoBehaviour
{
    DonutCounter m_donutCounter;

    private void Start()
    {
        m_donutCounter = GameObject.FindGameObjectWithTag("DonutCounter").GetComponent<DonutCounter>();

        m_donutCounter.m_donuts.Add(gameObject);
    }
}
