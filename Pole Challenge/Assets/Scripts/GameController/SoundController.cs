using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public AudioSource MenuClick;
    public AudioSource MusicTrack;

    public AudioMixerGroup sound;
    public AudioMixerGroup music;

    private float SoundUnmute = 5f;
    private float MusicUnmute = -5f;

    void Start()
    {
        var buttons = FindObjectOfType<ButtonHolder>();

        foreach (Button b in buttons.buttons)
        {
            b.onClick.AddListener(MenuButtonClick);
        }

        MusicTrack.Play();
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;

        sound.audioMixer.SetFloat("Volume2", PlayerPrefs.GetFloat("SoundFloat", SoundUnmute));
        music.audioMixer.SetFloat("Volume1", PlayerPrefs.GetFloat("MusicFloat", MusicUnmute));
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        if (arg1.buildIndex == 2)
            return;

        var buttons = FindObjectOfType<ButtonHolder>();

        foreach (Button b in buttons.buttons)
        {
            b.onClick.AddListener(MenuButtonClick);
        }
        
    }

    private void MenuButtonClick()
    {
        MenuClick.Play();
    }

    
}
