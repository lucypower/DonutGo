using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] GameObject m_player;
    [SerializeField] int m_height;
    [SerializeField] int m_distance;
    [SerializeField] int m_angle;

    private void Update()
    {
        Vector3 position = new(m_player.transform.position.x, m_height, m_player.transform.position.z - m_distance);

        transform.position = position;
    }
}
