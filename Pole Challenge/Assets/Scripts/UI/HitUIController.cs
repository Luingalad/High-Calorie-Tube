using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitUIController : MonoBehaviour
{
    public Color shieldColor;
    public Color hitColor;
    public Color hitSteak;

    public Image HitImage;
    public CanvasGroup HitImageCanvas;
    
    public void Hit(bool protectionAvailable)
    {        
        if(protectionAvailable)
        {
            HitImage.color = shieldColor;
        }
        else
        {
            HitImage.color = hitColor;
        }

        HitImageCanvas.alpha = 1f;
        StartCoroutine(fadeout());
    }


    public void HitSteak()
    {
        HitImage.color = hitSteak;
        HitImageCanvas.alpha = 1f;
        StartCoroutine(fadeout());
    }
    
    private IEnumerator fadeout()
    {
        while(HitImageCanvas.alpha > 0)
        {
            HitImageCanvas.alpha -= Time.deltaTime / 1.5f;
            yield return null;
        }
    }
}
