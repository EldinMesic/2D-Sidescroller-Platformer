using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private float duration = 100f;
    [SerializeField] private Gradient gradient;

    private SpriteRenderer spriteRenderer;
    private float startTime;

    private void Awake()
    {
        TryGetComponent(out spriteRenderer);
    
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float timeElapsed = Time.time - startTime;
        float percentage = Mathf.Cos(timeElapsed / duration * Mathf.PI * 2) * 0.5f + 0.5f;
        percentage = 1- percentage;

        percentage = Mathf.Clamp01(percentage);

        if(spriteRenderer != null )
            spriteRenderer.color = gradient.Evaluate(percentage);


    }
}
