using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Lean.Localization;
using UnityEngine.Audio;

public class UIController : MonoBehaviour
{
    //ScoreTable
    public LeanToken Score_Table;
    public LeanToken Gold_Table;
    public LeanLocalizedTextMeshProUGUI ScoreTableText;
    public LeanLocalizedTextMeshProUGUI GoldTableText;
    public GameObject ScoreTableUI;
    //EndScene
    public GameObject EndGameUI;
    public TMP_Text Score_EndScene;
    public TMP_Text Warning_EndScene;

    public Button WatchAdToContunie_EndScene;
    public Button SpendGoldToContunie_EndScene;
    public Button CloseButton_EndScene;
        
    //CurrentStr
    public TMP_Text CurrentStr;

    //ScorePoint
    public GameObject ScoreTable;
    public Button CloseButton_ScoreTable;

    //private float EndSceneWaitDuration = 1.5f;
    //private float _endSceneWaitDuration;
    private RunnerController runner;
    //New Record
    public GameObject NewRecordUI;
    public delegate void NewRecord();
    public NewRecord callBackNewRecord;

    [Header(header: "PauseMenu")]
    public GameObject PauseMenu;
    public Button PauseButton;

    public Button BackButtonPauseMenu;
    public Button FinishButtonPauseMenu;

    public Toggle soundToggle;
    public Toggle musicToggle;

    public AudioMixerGroup SoundMixer;
    public AudioMixerGroup MusicMixer;

    private float soundUnmute = 5f;
    private float soundMute = -80f;

    private float musicUnmute = -5f;
    private float musicMute = -80f;

    private string soundStr = "SoundFloat";
    private string musicStr = "MusicFloat";
    ///
    private bool backToGame;
    private float _duration;
    private float Duration = 3f;

    public TMP_Text CountDown;
    public GameObject CountDownGO;

    private int RespawnPrice = 10; //steak

    private readonly float SteakChance = 0.015f;
    [Header(header:"Steak Award")]
    public GameObject SteakRewardUI;   

    
    void Start()
    {
        WatchAdToContunie_EndScene.onClick.AddListener(OnClickWatchAdToContunie_EndScene);
        SpendGoldToContunie_EndScene.onClick.AddListener(OnClickSpendSteakToContunie_EndScene);
        CloseButton_EndScene.onClick.AddListener(OnClickCloseButton_EndScene);
        PauseButton.onClick.AddListener(onPauseMenuClick);
        BackButtonPauseMenu.onClick.AddListener(OnPauseMenuBackButton);
        FinishButtonPauseMenu.onClick.AddListener(OnPauseMenuFinishButton);

        musicToggle.onValueChanged.AddListener((g) => OnMusicToggle(g));
        soundToggle.onValueChanged.AddListener((g) => OnSoundToggle(g));

        callBackNewRecord += EarnNewRecord;
        GameController.instance.CallBackGameEnd += OnGameEnd;
        runner = FindObjectOfType<RunnerController>();

        float music = PlayerPrefs.GetFloat(musicStr, musicUnmute);
        float sound = PlayerPrefs.GetFloat(soundStr, soundUnmute);

        musicToggle.isOn = !(music == musicMute);
        soundToggle.isOn = !(sound == soundMute);
    }

    private void Update()
    {
        Score_Table.SetValue(GameController.instance.Score);
        Gold_Table.SetValue(Scores.instance.BurgerCount);
        ScoreTableText.UpdateLocalization();
        GoldTableText.UpdateLocalization();

        CurrentStr.text = runner.Strength.ToString();

        if (backToGame)
        {
            _duration += Time.deltaTime;
            
            if(_duration >= Duration)
            {
                _duration = 0;
                backToGame = false;

                PauseMenuState();
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape) && PauseButton.interactable)
        {
            if (!PauseMenu.activeSelf)
            {
                onPauseMenuClick();
            }
            else
            {
                OnPauseMenuBackButton();
            }
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && ScoreTable.activeSelf)
        {
            ScoreTable.GetComponent<ScoreTable>().OnMenuButtonClick();
        }
    }
    #region EndScene
    //EndScene
    public void OnClickWatchAdToContunie_EndScene()
    {
        //Open Ad first, Contunie After Ad
        //AdMobController.instance.callBackRewardedOnReward += GameController.instance.RewardToContuinue;
        //AdMobController.instance.callBackRewardedOnClose += GameController.instance.RewardClosed;
        AdMobController.instance.ShowRewardedAd();        
        WatchAdToContunie_EndScene.interactable = false;
    }
    public void OnClickSpendSteakToContunie_EndScene()
    {
        //Spend Diamond, Contunie to play
        ProfileManager.instance.callBackOnSteakChanged(-RespawnPrice);
        GameController.instance.RewardToContuinue();
        GameController.instance.RewardClosed();
    }
    public void OnClickCloseButton_EndScene()
    {
        //oyunu bitirir, score gözükür. Sonra Ana ekrana dönülür.
        float chance = Random.Range(0f, 1f) - ( (float)GameController.instance.Score / 5000f );
        
        if (chance < SteakChance)
        {
            SteakRewardUI.SetActive(true);
        }
        ProfileManager.instance.OnSave();
        EndGameUI.SetActive(false);
        PauseButton.gameObject.SetActive(false);
        ScoreTable.SetActive(true);
        SignAchievement();
    }

    public void OnRewardCallBack()
    {
        PauseButton.interactable = true;
        ScoreTableUI.SetActive(true);
        PauseButton.gameObject.SetActive(true);
        CurrentStr.gameObject.SetActive(true);
        EndGameUI.SetActive(false);
    }
    public void OnGameEnd()
    {
        PauseButton.interactable = false;
        if (GameController.instance.isRespawndUsed )
        {
            StartCoroutine(GameEnded());            
        }
        else
        {            
            ScoreTableUI.SetActive(false);
            PauseButton.gameObject.SetActive(false);
            CurrentStr.gameObject.SetActive(false);
            Score_EndScene.text = "Your score is " + GameController.instance.Score;
            EndGameUI.SetActive(true);
            WatchAdToContunie_EndScene.interactable = AdMobController.instance.isRewardedAdLoaded;

            SpendGoldToContunie_EndScene.interactable = !(ProfileManager.instance.Steak < RespawnPrice);
            
        }
    }

    private void SignAchievement()
    {
        int score = GameController.instance.Score;
        
        GPG_Controller.instance.Achievements_Welcome();

        if (score > 1000)
        {
            GPG_Controller.instance.Achievements_1000m();
        }
        if (score > 2000)
        {
            GPG_Controller.instance.Achievements_2000m();
        }
        if(score > 5000)
        {            
            GPG_Controller.instance.Achievements_5000m();
        }
        if (score > 10000)
        {            
            GPG_Controller.instance.Achievements_10000m();
        }
        if (Scores.instance.BurgerCount > 250)
        {
            GPG_Controller.instance.Achievements_Burger250();
        }
        if (Scores.instance.BurgerCount > 1000)
        {
            GPG_Controller.instance.Achievements_Burger500();
        }
        if (Scores.instance.BurgerCount > 1000)
        {
            GPG_Controller.instance.Achievements_Burger1000();
        }
        if(Scores.instance.PizzaCount > 50)
        {
            GPG_Controller.instance.Achievements_Pizza50();
        }
        if (Scores.instance.PizzaCount > 100)
        {
            GPG_Controller.instance.Achievements_Pizza100();
        }
        if (Scores.instance.PizzaCount > 200)
        {
            GPG_Controller.instance.Achievements_Pizza200();
        }

        GPG_Controller.instance.ScoreToLeaderBoard(Scores.instance.ScorePoint, Scores.instance.BurgerCount);


    }

    private IEnumerator GameEnded()
    {
        yield return new WaitForSeconds(3f);
        OnClickCloseButton_EndScene();
    }
    //
    #endregion

    #region Record
    private void EarnNewRecord()
    {
        NewRecordUI.SetActive(true);
    }
    #endregion

    #region PauseMenu
    private void PauseMenuState()
    {
        if(PauseMenu.activeSelf)
        {

            runner._DeadOrAlive = true;
            runner.runnerBody.velocity = Vector3.zero;
            runner.animator.SetFloat("Speed", 0f);            
            GameController.instance._isGameControlActive = false;
            AdMobController.instance.ShowBanner();
            
        }
        else
        {
            runner._DeadOrAlive = false;
            runner.animator.SetFloat("Speed", GameController.instance.SpeedMultiplier);

            GameController.instance._isGameControlActive = true;
            AdMobController.instance.CloseBanner();
        }
        
    }
    private void onPauseMenuClick()
    {
        if (!PauseMenu.activeSelf)
        {
            PauseMenu.SetActive(true);

            PauseMenuState();
        }
        else
        {
            PauseMenu.SetActive(false);
            backToGame = true;
            StartCoroutine(CountDownNu());
        }
    }
    private void OnPauseMenuBackButton()
    {
        PauseMenu.SetActive(false);
        backToGame = true;
        StartCoroutine(CountDownNu());
        AdMobController.instance.CloseBanner();
    }
    
    private void OnPauseMenuFinishButton()
    {
        AdMobController.instance.CloseBanner();
        FindObjectOfType<RunnerController>().Die();
        OnClickCloseButton_EndScene();
        PauseButton.gameObject.SetActive(false);
        PauseMenu.SetActive(false);
    }

    private void OnMusicToggle(bool state)
    {
        if (!state)
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

    private void OnSoundToggle(bool state)
    {
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
    #endregion

    #region CountDown
    public IEnumerator CountDownNu()
    {
        PauseButton.interactable = false;
        FindObjectOfType<RunnerSounds>().TimerToStart();
        CountDownGO.SetActive(true);
        for(int i = 3; i > 0; i--)
        {
            CountDown.text = i.ToString();
            yield return new WaitForSecondsRealtime(1f);
        }
        FindObjectOfType<RunnerSounds>().TimerStop();
        CountDown.text = "GO";
        yield return new WaitForSecondsRealtime(1f);
        CountDownGO.SetActive(false);
        PauseButton.interactable = true;
    }
    #endregion

}
