using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerPowers : MonoBehaviour
{
    public bool isProtectionAvailable = false;

    public float ProtectionDuration;
    public float _protectionDuration;
    private float protactionDurationDefault = 5f;

    public bool isMagnetAvailable = false;
    public float MagnetDuration;
    public float _magnetDuration;
    private float magnetDurationDefault = 5f;

    public GameObject ProtectionBall;
    public GameObject MagneticBall;
    public float ProtectionBallRotateSpeed;

    public RunnerController controller;
    public CountDownControllers countDown;

    public bool isDoubleBurgerAvailable = false;
    public float DoubleBurgerDuration;
    private float _doubleBurgerDuration;

    public bool isGaAvailable = false;
    public float GaDuration;
    private float _gaDuration;

    public GameObject DoubleBurgerText;
    public GameObject GuardianAngelText;
        
    private void Update()
    {
        if(isProtectionAvailable)
        {
            _protectionDuration += Time.deltaTime;
            ProtectionBall.transform.Rotate(0, ProtectionBallRotateSpeed, 0);
            countDown.SetProtection(_protectionDuration, ProtectionDuration);
            if ( _protectionDuration >= ProtectionDuration )
            {
                ProtectionOff();
                _protectionDuration = 0;
            }
        }

        if (isMagnetAvailable)
        {
            _magnetDuration += Time.deltaTime;
            countDown.SetMagnet(_magnetDuration, MagnetDuration);
            if (_magnetDuration >= MagnetDuration)
            {
                MagnetOff();
                _magnetDuration = 0;
            }
        }

        if(isGaAvailable)
        {
            _gaDuration += Time.deltaTime;
            if(_gaDuration >= GaDuration)
            {
                GaOff();                
            }
        }

        if(isDoubleBurgerAvailable)
        {
            _doubleBurgerDuration += Time.deltaTime;
            if(_doubleBurgerDuration >= DoubleBurgerDuration)
            {
                DoubleOff();
            }
        }

        if (controller._DeadOrAlive)
        {
            ProtectionOff();
            MagnetOff();
            _protectionDuration = 0;
            _magnetDuration = 0;
        }
    }

    private void OnEnable()
    {
        if(BonusManager.instance.SPLevel > 0)
        {
            StartCoroutine(startProtection());
        }

        if(BonusManager.instance.SMLevel > 0)
        {
            StartCoroutine(startMagnet());
        }
    }

    public void ProtectionOn()
    {
        if (isProtectionAvailable)
        {
            _protectionDuration -= protactionDurationDefault * (1 + BonusManager.instance.PDRate);
        }
        else
        {
            ProtectionDuration = protactionDurationDefault * (1 + BonusManager.instance.PDRate);
            _protectionDuration = 0;
            isProtectionAvailable = true;
            ProtectionBall.SetActive(true);
            countDown.SetActiveProtectionCounter(true);
        }
    }

    public void ProtectionOn(float t)
    {
        if (isProtectionAvailable)
        {
            _protectionDuration -= t;
        }
        else
        {
            ProtectionDuration = t;
            _protectionDuration = 0;
            isProtectionAvailable = true;
            ProtectionBall.SetActive(true);
            countDown.SetActiveProtectionCounter(true);
        }
    }

    public void ProtectionOff()
    {
        isProtectionAvailable = false;
        ProtectionBall.SetActive(false);
        countDown.SetActiveProtectionCounter(false);
    }

    public void MagnetnOn()
    {
        if(isMagnetAvailable)
        {
            _magnetDuration -= magnetDurationDefault * (1 + BonusManager.instance.MDRate);
        }
        else
        {        
        MagnetDuration = magnetDurationDefault * (1 + BonusManager.instance.MDRate);
        _magnetDuration = 0;
        MagneticBall.SetActive(true);
        isMagnetAvailable = true;
        countDown.SetActiveMagnetCounter(true);
        }
    }

    public void MagnetnOn(float t)
    {
        if (isMagnetAvailable)
        {
            _magnetDuration -= t;
        }
        else
        {
            MagnetDuration = t;
            _magnetDuration = 0;
            MagneticBall.SetActive(true);
            isMagnetAvailable = true;
            countDown.SetActiveMagnetCounter(true);
        }
    }

    public void MagnetOff()
    {
        isMagnetAvailable = false;
        MagneticBall.SetActive(false);
        countDown.SetActiveMagnetCounter(false);
    }

    public void GaOn()
    {
        GuardianAngelText.SetActive(true);
        isGaAvailable = true;
        _gaDuration = 0;
    }

    public void GaOff()
    {
        GuardianAngelText.SetActive(false);
        isGaAvailable = false;
    }

    public void DoubleOn()
    {
        DoubleBurgerText.SetActive(true);
        isDoubleBurgerAvailable = true;
        _doubleBurgerDuration = 0;
    }

    public void DoubleOff()
    {
        DoubleBurgerText.SetActive(false);
        isDoubleBurgerAvailable = false;
    }

    private IEnumerator startProtection()
    {
        yield return new WaitForSecondsRealtime(4.5f);
        ProtectionOn(BonusManager.instance.SPRate);
    }

    private IEnumerator startMagnet()
    {
        yield return new WaitForSecondsRealtime(4.5f);
        MagnetnOn(BonusManager.instance.SMRate);
    }
}
