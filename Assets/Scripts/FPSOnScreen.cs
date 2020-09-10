using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSOnScreen : MonoBehaviour
{
    Rect fpsRect;
    float fps;
    public TMP_Text FPSTextNumber;

    void Start()
    {
        StartCoroutine(RecalculateFPS());
    }

    private IEnumerator RecalculateFPS()
    {
        while (true)
        {
            fps = 1 / Time.deltaTime;
            yield return new WaitForSeconds(.2f);
        }
    }

    void OnGUI()
    {
        if (fps < 37f)
        {
            FPSTextNumber.fontSize = 50;
        }
        else
        {
            FPSTextNumber.fontSize = 36;
        }
        int truncatedFPS = (int)fps;
        FPSTextNumber.text = truncatedFPS.ToString();
    }
}