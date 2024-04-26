using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatistics : MonoBehaviour
{
    public int m_movementSpeed;
    public int m_holdCapacity;
    public List<GameObject> m_donutsHeld;
    public int m_money;

    // upgrades

    public int m_walkLevel;
    public int m_holdLevel;

    public bool m_firstTimeSave;
}
