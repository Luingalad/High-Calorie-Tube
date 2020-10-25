using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using Lean.Localization;

public class OptionsMenu : MonoBehaviour
{
    public GameObject YesNoMenu;

    public Button backButton;
    public Button resetProfileButton;

    public Button YesButton;
    public Button NoButton;

    public Toggle musicToggle;
    public Toggle soundToggle;

    public AudioMixerGroup SoundMixer;
    public AudioMixerGroup MusicMixer;

    private float soundUnmute = 5f;
    private float soundMute = -80f;

    private float musicUnmute = -5f;
    private float musicMute = -80f;

    private string soundStr = "SoundFloat";
    private string musicStr = "MusicFloat";

    

    [Header(header: "Language")]
    public TMP_Dropdown LanguageSelector;
    [Header(header: "SinginButton")]
    public Button LogInButton;
    public string signIn;
    public string signOut;
    public LeanLocalizedTextMeshProUGUI leanTextMesp;


    void Start()
    {
        backButton.onClick.AddListener(onClickBackButton);
        resetProfileButton.onClick.AddListener(OnClickResetProfile);
        YesButton.onClick.AddListener(OnClickYes);
        NoButton.onClick.AddListener(OnClickNo);
        LanguageSelector.onValueChanged.AddListener(OnValueChanged);
        LogInButton.onClick.AddListener(onClickLogInButton);

        musicToggle.onValueChanged.AddListener((g) => OnMusicToggle(g));
        soundToggle.onValueChanged.AddListener((g) => OnSoundToggle(g));

        float music = PlayerPrefs.GetFloat(musicStr, musicUnmute);
        float sound = PlayerPrefs.GetFloat(soundStr, soundUnmute);

        MusicMixer.audioMixer.SetFloat("Volume1", music);
        SoundMixer.audioMixer.SetFloat("Volume2", sound);

        if(music == musicMute)
        {
            musicToggle.isOn = false;
        }

        if(sound == soundMute)
        {
            soundToggle.isOn = false;
        }

        string lang = LeanLocalization.currentLanguage;
        var langs = LeanLocalization.CurrentLanguages;
        int i = 0;
        foreach(string s in langs.Keys)
        {
            if(s.Equals(lang))
            {
                break;
            }
            else
            {
                i++;
            }            
        }
        LanguageSelector.value = i;

        //
        GPG_Controller.instance.callBackSuccess += callBackSuccessLogIn;
        GPG_Controller.instance.callBackFail += callBackFailLogIn;       
        
    }

    private void OnEnable()
    {
#if UNITY_EDITOR
        if (GPG_Controller.playGames.localUser.authenticated)
        {
            callBackSuccessLogIn();
        }
#endif
    }

    private void onClickLogInButton()
    {
        
        GPG_Controller.instance.GPGLogIn();        
    }

    private void callBackSuccessLogIn()
    {
        leanTextMesp.TranslationName = signOut;
    }
    private void callBackFailLogIn()
    {
        
    }
    private void callBackSignOut()
    {
        leanTextMesp.TranslationName = signIn;
    }

    private void OnValueChanged(int arg0)
    {
        LeanLocalization.CurrentLanguage = LanguageSelector.options[arg0].text;
    }

    public void onClickBackButton()
    {
        gameObject.SetActive(false);
        AdMobController.instance.ShowBanner();
    }

    public void OnClickResetProfile()
    {
        YesNoMenu.SetActive(true);
    }

    public void OnClickYes()
    {
        ProfileManager.instance.ResetPlayerPrefs();
        YesNoMenu.SetActive(false);
    }

    public void OnClickNo()
    {
        YesNoMenu.SetActive(false);
    }

    public void OnMusicToggle(bool state)
    {
        //GameObject.FindGameObjectWithTag("Sounds").GetComponent<SoundController>().MusicTrack.mute = !state;
        if(!state)
        {
            MusicMixer.audioMixer.SetFloat("Volume1", musicMute);
            PlayerPrefs.SetFloat(musicStr, musicMute);
        }
        else
        {
            MusicMixer.audioMixer.SetFloat("Volume1", musicUnmute);
            PlayerPrefs.SetFloat(musicStr, musicUnmute);
        }        
    }

    public void OnSoundToggle(bool state)
    {
        //GameObject.FindGameObjectWithTag("Sounds").GetComponent<SoundController>().MenuClick.mute = !state;
        if (!state)
        {
            SoundMixer.audioMixer.SetFloat("Volume2", soundMute);
            PlayerPrefs.SetFloat(soundStr, soundMute);
        }
        else
        {
            SoundMixer.audioMixer.SetFloat("Volume2", soundUnmute);
            PlayerPrefs.SetFloat(soundStr, soundUnmute);
        }
    }
}
