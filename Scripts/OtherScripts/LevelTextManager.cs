using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LevelTextManager : MonoBehaviour
{
    public float fixedStayTime = 1f;
    public float fadeDuration = 1f;
    
    [SerializeField] private GameObject go;
    [SerializeField] private TextMeshProUGUI textObject;
   

    public void ShowLevelMessage(string message)
    {
        if(!go.activeSelf)
        {          
            textObject.text = message;
            go.SetActive(true);
        }
    }


    public void HideTextMessage()
    {
        if (go.activeSelf)
        {
            StartCoroutine(FadeOutScreen());
        }
    }

    IEnumerator FadeOutScreen()
    {
        float alphaMod = 1f / fadeDuration;
        float duration = fadeDuration;
        CanvasGroup fadeScreen = GetComponent<CanvasGroup>();
        while(duration>= 0f)
        {
            duration -= Time.deltaTime;
            fadeScreen.alpha = duration * alphaMod;

            yield return new WaitForEndOfFrame();
        }

        go.SetActive(false);
        textObject.text = string.Empty;

        fadeScreen.alpha = 1f;

    }


}
