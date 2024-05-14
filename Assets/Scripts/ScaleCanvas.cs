using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleCanvas : MonoBehaviour
{
    private void Start()
    {
        RectTransform canvas = GetComponent<RectTransform>();

        canvas.sizeDelta = new Vector2(Screen.width / 2, Screen.height / 2);
    }
}
