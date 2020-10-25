using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountDownControllers : MonoBehaviour
{
    public CanvasGroup magnetBackGround;
    public Image magnetBack;
    public Image magnetCountDown;
    public TMP_Text magnetDuration;
    public CanvasGroup protectionBackGround;
    public Image protectionCountDown;
    public Image protectionBack;
    public TMP_Text protectionDuration;

    public Gradient colorGradient;
    void Start()
    {
        magnetCountDown.fillAmount = 0;
        protectionCountDown.fillAmount = 0;
        magnetDuration.text = "";
        protectionDuration.text = "";
    }  

    public void SetMagnet(float currentTime, float startTime)
    {
        magnetDuration.text = (startTime - currentTime).ToString("0.0");
        magnetCountDown.fillAmount = (startTime - currentTime) / startTime;
        float f = (startTime - currentTime) / startTime;
        magnetBack.color = colorGradient.Evaluate(1-f);
        magnetCountDown.color = colorGradient.Evaluate(1-f);

        if ((startTime - currentTime) <= 0 )
        {
            magnetDuration.text = "0";
        }

        if (magnetBackGround.alpha != 0 && magnetBackGround.alpha != 1)
        {
            magnetBackGround.alpha = 1;
        }
    }

    public void SetActiveMagnetCounter(bool state)
    {        
        StartCoroutine(FadeMagnet(state));
    }

    public void SetActiveProtectionCounter(bool state)
    {        
        StartCoroutine(FadeProtection(state));        
    }

    public void SetProtection (float currentTime, float startTime)
    {
        protectionDuration.text = (startTime - currentTime).ToString("0.0");
        protectionCountDown.fillAmount = (startTime - currentTime) / startTime;
        float f = (startTime - currentTime) / startTime;
        protectionBack.color = colorGradient.Evaluate(1-f);
        protectionCountDown.color = colorGradient.Evaluate(1-f);

        if ((startTime - currentTime) <= 0)
        {
            protectionDuration.text = "0";
        }

        if(protectionBackGround.alpha != 0 && protectionBackGround.alpha != 1)
        {
            protectionBackGround.alpha = 1;
        }
    }

    IEnumerator FadeMagnet(bool state)
    {
        if (state)
        {
            while(magnetBackGround.alpha < 1f)
            {
                magnetBackGround.alpha += Time.deltaTime / 2f;
                yield return null;
            }
        }
        else
        {
            while (magnetBackGround.alpha > 0f)
            {
                magnetBackGround.alpha -= Time.deltaTime / 2f;
                yield return null;
            }
        }
    }

    IEnumerator FadeProtection(bool state)
    {        
        if (state)
        {
            while (protectionBackGround.alpha < 1f)
            {
                protectionBackGround.alpha += Time.deltaTime / 2f;
                yield return null;
            }
        }
        else
        {
            while (protectionBackGround.alpha > 0f)
            {
                protectionBackGround.alpha -= Time.deltaTime / 2f;
                yield return null;
            }
        }
    }


}
