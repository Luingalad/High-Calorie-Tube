using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MainMenuController : MonoBehaviour
{
    [Header(header: "Scores and Assets")]
    public TMP_Text Steak;
    public TMP_Text Gold;
    public TMP_Text HighScore;    
    [Header(header: "Main Menu Buttons")]    
    public Button PlayButton;
    public Button BurgerMarketButton;
    public Button SteakMarketButton;
    public Button IAPButton;
    public Button StatsButton;
    public Button OptionsButton;
    public Button CloseButton;
    public Button AddSteakButtonInMarket;
    [Header(header: "Scenes")]
    public int GameScene;
    public int MainScene;
    [Header(header: "Menus")]
    public GameObject OptionMenu;
    public GameObject BurgerMarketMenu;
    public GameObject SteakMarketMenu;
    public GameObject IAPMenu;
    public GameObject LoadingUI;
    [Header(header: "GooglePlay")]
    public Button AchievementButton;
    public Button LeaderBoardButton;

    private bool BannerShow;
    void Start()
    {
        ProfileManager.instance.callBackOnRefresh += RefreshUI;
        RefreshUI();

        AdMobController.instance.ShowBanner();

        CloseButton.onClick.AddListener(OnCloseButtonClick);
        PlayButton.onClick.AddListener(OnPlayMenuClick);
        BurgerMarketButton.onClick.AddListener(OnBurgerMarketButtonClick);
        SteakMarketButton.onClick.AddListener(OnSteakMarketButtonClick);
        IAPButton.onClick.AddListener(OnIAPMarketButtonClick);
        AddSteakButtonInMarket.onClick.AddListener(OnIAPMarketButtonClick);
        StatsButton.onClick.AddListener(OnStatsButtonClick);
        OptionsButton.onClick.AddListener(OnOptionsButtonClick);
        AchievementButton.onClick.AddListener(onAchievementShowButtonClick);
        LeaderBoardButton.onClick.AddListener(onShowLeaderBoardShowButtonClick);
        
        BannerShow = true;
    }
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            //geri tuşu aksiyonları
            if(IAPMenu.activeSelf)
            {
                IAPMenu.SetActive(false);
            }
            else if(OptionMenu.activeSelf)
            {
                OptionMenu.SetActive(false);
            }
            else if(BurgerMarketMenu.activeSelf)
            {
                BurgerMarketMenu.SetActive(false);
            }
            else if(SteakMarketMenu.activeSelf)
            {
                SteakMarketMenu.SetActive(false);
            }
            else
            {
                //appten çıkılacak bir ara..
            }
        }

        if(!AdMobController.instance.BannerOpened && AdMobController.instance.BannerLoaded && BannerShow && !ProfileManager.instance.isAdsRemoved && !LoadingUI.activeSelf)
        {
            AdMobController.instance.ShowBanner();
        }
        else if(AdMobController.instance.BannerOpened)
        {
            BannerShow = true;
        }
    }
    
    public void SteakChanged()
    {
        Steak.text = "Steak: " + ProfileManager.instance.Steak;        
    }

    public void OnCloseButtonClick()
    {
        Application.Quit();
    }

    public void OnPlayMenuClick()
    {
        AdMobController.instance.CloseBanner();
        SceneManager.LoadScene(GameScene);
        BannerShow = false;
    }

    public void OnBurgerMarketButtonClick()
    {
        AdMobController.instance.CloseBanner();
        BurgerMarketMenu.SetActive(true);
        BannerShow = false;
    }

    private void OnStatsButtonClick()
    {
        //Stats Menusu açılır
        //inş cnm ya
    }

    private void OnSteakMarketButtonClick()
    {
        //Steak Market Açılır --Steak Karşılığı gold satışı
        SteakMarketMenu.SetActive(true);
        AdMobController.instance.CloseBanner();
        BannerShow = false;
    }

    private void OnIAPMarketButtonClick()
    {
        if (GPG_Controller.playGames.localUser.authenticated)
            IAPMenu.SetActive(true);
        else
            GPG_Controller.instance.GPGLogIn();
    }

    public void OnOptionsButtonClick()
    {
        OptionMenu.SetActive(true);
        AdMobController.instance.CloseBanner();        
        BannerShow = false;
    }

    public void RefreshUI()
    {
        Steak.text = ProfileManager.instance.Steak.ToString();
        Gold.text = ProfileManager.instance.Burger.ToString();
        HighScore.text = ProfileManager.instance.Record + " m";        
    }

    private void onShowLeaderBoardShowButtonClick()
    {
        GPG_Controller.instance.ShowLeaderBoard();
    }

    private void onAchievementShowButtonClick()
    {
        GPG_Controller.instance.ShowAchievements();
    }
}
