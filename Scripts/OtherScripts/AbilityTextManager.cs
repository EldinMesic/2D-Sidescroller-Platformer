using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AbilityTextManager : MonoBehaviour
{
    [SerializeField] private float fixedStayTime = 1f;
    [SerializeField] private GameObject go;
    [SerializeField] private TextMeshProUGUI textObject;
    
    
    private float startTime;


    public void Update()
    {
        if(go.activeSelf && Time.unscaledTime - startTime >= fixedStayTime && Input.anyKeyDown) 
        {
            Time.timeScale = 1.0f;
            go.SetActive(false);
            textObject.text = string.Empty;
        }
    }



    public void ShowAbilityMessage(string message)
    {
        if(!go.activeSelf)
        {
            Time.timeScale = 0.0f;
            textObject.text = message;
            go.SetActive(true);
            startTime = Time.unscaledTime;
        }
    }



}
