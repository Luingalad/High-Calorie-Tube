using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerSounds : MonoBehaviour
{
    public AudioSource TakingBurger;
    public AudioSource TakingProtection;
    public AudioSource TakingMagnet;
    public AudioSource TakingPizza;
    public AudioSource HitTheBlock;
    public AudioSource Timer;
    public AudioSource General;

    public AudioClip RecordClip;

    public AudioClip DeadClip;
    public AudioClip RespawnClip;
    
    public AudioClip TimerClip { get; private set; }

    public void TakeBurger()
    {
        TakingBurger.Play();
    }

    public void TakeMagnet()
    {
        TakingMagnet.Play();
    }

    public void TakeProtection()
    {
        TakingProtection.Play();
    }

    public void TakePizza()
    {
        TakingPizza.Play();
    }

    public void HitBlock()
    {
        HitTheBlock.Play();
    }

    public void Record()
    {
        General.clip = RecordClip;
        General.Play();
    }

    public void Dead()
    {
        General.clip = DeadClip;
        General.Play();
    }

    public void Respawn()
    {
        General.clip = RespawnClip;
        General.Play();
    }

    public void TimerToStart()
    {
        Timer.Play();
    }

    public void TimerStop()
    {
        Timer.Stop();        
    }
}
