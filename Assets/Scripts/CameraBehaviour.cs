using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] GameObject m_player;
    [SerializeField] int m_height;
    [SerializeField] int m_distance;

    private void Update()
    {
        Vector3 position = new Vector3(m_player.transform.position.x, m_height, m_player.transform.position.z - 10);

        transform.position = position;
    }
}
