using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pixelizable : MonoBehaviour
{
    public enum PixelStatus
    {
        Retro,
        Low,
        Medium,
        High,
        Full
    }

    public int retroScale = 1;
    public PixelStatus pixelStatus = PixelStatus.Full;

    private PixelStatus prevPixelStatus = PixelStatus.Full;
    private SpriteRenderer spRenderer;

    private TilemapRenderer tpRenderer;


    // Start is called before the first frame update
    void Start()
    {
        if(TryGetComponent(out spRenderer))
        {
            spRenderer.material = new Material(Resources.Load<Material>("Materials/PixelateMaterial"));
            spRenderer.material.SetFloat("_RetroScale", retroScale);
        }
        if(TryGetComponent(out tpRenderer))
        {
            tpRenderer.material = new Material(Resources.Load<Material>("Materials/PixelateMaterial"));
            tpRenderer.material.SetFloat("_RetroScale", retroScale);
        }
        
        


        SetPixelSample();

    }

    private void Update()
    {
        SetPixelSample();
    }


    private void ToggleRetro(bool isRetro)
    {
        if(spRenderer != null)
            spRenderer.material.SetInt("_IsRetro", isRetro ? 1 : 0);
        if(tpRenderer != null)
            tpRenderer.material.SetInt("_IsRetro", isRetro ? 1 : 0);
    }


    private void SetPixelSample()
    {
        if (pixelStatus == prevPixelStatus) return;
        prevPixelStatus = pixelStatus;

        int pixelSample;
        switch (pixelStatus)
        {
            case PixelStatus.Retro:
                pixelSample = 256;
                ToggleRetro(true);
                break;
            case PixelStatus.Low:
                pixelSample = 256;
                ToggleRetro(false);
                break;
            case PixelStatus.Medium:
                pixelSample = 512;
                ToggleRetro(false);
                break;
            case PixelStatus.High:
                pixelSample = 1024;
                ToggleRetro(false);
                break;
            default:
                pixelSample = 2048;
                ToggleRetro(false);
                break;
        }

        if (spRenderer != null)
            spRenderer.material.SetFloat("_PixelSample", pixelSample * transform.localScale.x);
        if (tpRenderer != null)
            tpRenderer.material.SetFloat("_PixelSample", pixelSample * transform.localScale.x);

    }


}
