using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleCanvas : MonoBehaviour
{
    [SerializeField] Canvas m_mainCanvas;

    private void Update()
    {
        float h = m_mainCanvas.GetComponent<RectTransform>().rect.height;
        float w = m_mainCanvas.GetComponent<RectTransform>().rect.width;

        var canvas = GetComponent<RectTransform>();

        canvas.sizeDelta = new Vector2(w, h);
    }
}
