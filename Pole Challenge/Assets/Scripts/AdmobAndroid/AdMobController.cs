using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;
using System;

public class AdMobController : MonoBehaviour
{
    private BannerView bannerAd;
    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;
    private RewardedAd rewardForBurger;   

    #region Singleton
    public static AdMobController instance;

    private bool isRewardEarned;    
    public bool isRewardedAdLoaded;
    public bool isRewardedAdCloseTrigger;

    private bool isBurgerRewardEarned;    
    public bool isBurgerRewardedAdLoaded;
    public bool isBurgerRewardedAdCloseTrigger;
    public bool isInterstitalAdLoaded;

    public bool isInterstialAdShown;
    public bool isInterstialAdClosed;
    public delegate void OnInterstitalAdClosed();
    public OnInterstitalAdClosed callBackOnInterstitalAdClosed;

    private Scene activeScene;
    private Scene MainMenuScene;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    #endregion
    
    void Start()
    {
#if UNITY_ANDROID
        string appId = "ca-app-pub-5564803278580548~8724492662";
#elif UNITY_IPHONE
            string appId = "ca-app-pub-3940256099942544~1458002511";
#else
            string appId = "unexpected_platform";
#endif
        MobileAds.Initialize(appId);
        if (!ProfileManager.instance.isAdsRemoved)
        {
            RequestBanner();
            RequestIntersialAd();
        }
        RequestFirstRewardedAd();
        RequestBurgerRewardedAd();

        MainMenuScene = SceneManager.GetSceneByBuildIndex(0);
    }
    private void Update()
    {
        if(isRewardedAdCloseTrigger)
        {
            DoTheDirtyJobForRewardedAd();
        }

        if (isBurgerRewardedAdCloseTrigger)
        {
            DoTheDirtyJobForBurgerRewardedAd();
        }

        if (!activeScene.Equals(SceneManager.GetActiveScene()))
        {
            activeScene = SceneManager.GetActiveScene();
        }

        if(isInterstialAdClosed )
        {
            callBackOnInterstitalAdClosed.Invoke();
            isInterstialAdClosed = false;
            isInterstialAdShown = false;
        }
    }
    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-5564803278580548/9244680494"; //real
        //string adUnitId = "ca-app-pub-3940256099942544/6300978111"; //test
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
        string adUnitId = "unexpected_platform";
#endif
        
        AdSize adSize = new AdSize(320, 50);
        bannerAd = new BannerView(adUnitId, adSize, AdPosition.Bottom);

        AdRequest request = new AdRequest.Builder().Build(); //real

        //AdRequest request = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build(); //test

        HandleAdSubBanner(true);

        bannerAd.LoadAd(request);
    }

    private void RequestIntersialAd()
    {
        isInterstitalAdLoaded = false;
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-5564803278580548/8971904674"; //real
        //string adUnitId = "ca-app-pub-3940256099942544/1033173712"; //test
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
        string adUnitId = "unexpected_platform";
#endif
        interstitialAd = new InterstitialAd(adUnitId);

        AdRequest request = new AdRequest.Builder().Build(); //real

        //AdRequest request = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build(); //test
        HandleAdSubInterstitial(true);

        interstitialAd.LoadAd(request);        
    }

    private void RequestFirstRewardedAd()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-5564803278580548/3104252904"; //real
        //string adUnitId = "ca-app-pub-3940256099942544/5224354917"; //test
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
        string adUnitId = "unexpected_platform";
#endif

        rewardedAd = new RewardedAd(adUnitId);
        HandleAdSubRewarded(true);

        AdRequest request = new AdRequest.Builder().Build(); //real

        //AdRequest request = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build(); //test


        rewardedAd.LoadAd(request);

    }

    private void RequestBurgerRewardedAd()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-5564803278580548/7252850080"; //real
        //string adUnitId = "ca-app-pub-3940256099942544/5224354917"; //test
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
        string adUnitId = "unexpected_platform";
#endif

        rewardForBurger = new RewardedAd(adUnitId);
        HandleAdSubBurgerRewarded(true);

        AdRequest request = new AdRequest.Builder().Build(); //real

        //AdRequest request = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build(); //test


        rewardForBurger.LoadAd(request);

    }

    #region HandleBanner
    public bool BannerOpened;
    public bool BannerLoaded;
    //Banner
    public void ShowBanner()
    {
        if(!ProfileManager.instance.isAdsRemoved)
            bannerAd.Show();
    }

    public void CloseBanner()
    {
        if(!ProfileManager.instance.isAdsRemoved)
        bannerAd.Hide();
    }

    public void DestroyBanner()
    {
        bannerAd.Destroy();               
    }

    public void HandleAdSubBanner(bool sub)
    {
        if(sub)
        {
            // Called when an ad request has successfully loaded.
            bannerAd.OnAdLoaded += HandleOnAdLoadedBanner;
            // Called when an ad request failed to load.
            bannerAd.OnAdFailedToLoad += HandleOnAdFailedToLoadBanner;
            // Called when an ad is clicked.
            bannerAd.OnAdOpening += HandleOnAdOpenedBanner;
            // Called when the user returned from the app after an ad click.
            bannerAd.OnAdClosed += HandleOnAdClosedBanner;
            // Called when the ad click caused the user to leave the application.
            bannerAd.OnAdLeavingApplication += HandleOnAdLeavingApplicationBanner;
        }
        else
        {
            // Called when an ad request has successfully loaded.
            bannerAd.OnAdLoaded -= HandleOnAdLoadedBanner;
            // Called when an ad request failed to load.
            bannerAd.OnAdFailedToLoad -= HandleOnAdFailedToLoadBanner;
            // Called when an ad is clicked.
            bannerAd.OnAdOpening -= HandleOnAdOpenedBanner;
            // Called when the user returned from the app after an ad click.
            bannerAd.OnAdClosed -= HandleOnAdClosedBanner;
            // Called when the ad click caused the user to leave the application.
            bannerAd.OnAdLeavingApplication -= HandleOnAdLeavingApplicationBanner;
        }
    }

    public void HandleOnAdLoadedBanner(object sender, EventArgs args)
    {        
        BannerLoaded = true;
        print("Banner: HandleOnShow");
    }

    public void HandleOnAdFailedToLoadBanner(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("Banner: HandleFailedToReceiveAd event received with message: "
                            + args.Message);
        BannerLoaded = false;
        DestroyBanner();
    }

    public void HandleOnAdOpenedBanner(object sender, EventArgs args)
    {
        Debug.Log("Banner: HandleAdOpened event received");
        BannerOpened = true;
    }

    public void HandleOnAdClosedBanner(object sender, EventArgs args)
    {
        Debug.Log("Banner: HandleAdClosed event received");
        BannerOpened = false;
    }

    public void HandleOnAdLeavingApplicationBanner(object sender, EventArgs args)
    {
        Debug.Log("Banner: HandleAdLeavingApplication event received");
    }
    #endregion

    #region HangleInterstitial
    public void ShowInterstital()
    {
        if (isInterstitalAdLoaded && !ProfileManager.instance.isAdsRemoved)
        {
            interstitialAd.Show();
        }
    }
    public void DestroyInterstital()
    {
        isInterstitalAdLoaded = false;
        interstitialAd.Destroy();
    }
    public void HandleAdSubInterstitial(bool sub)
    {
        if (sub)
        {
            // Called when an ad request has successfully loaded.
            interstitialAd.OnAdLoaded += HandleOnAdLoadedInterstitial;
            // Called when an ad request failed to load.
            interstitialAd.OnAdFailedToLoad += HandleOnAdFailedToLoadInterstitial;
            // Called when an ad is clicked.
            interstitialAd.OnAdOpening += HandleOnAdOpenedInterstitial;
            // Called when the user returned from the app after an ad click.
            interstitialAd.OnAdClosed += HandleOnAdClosedInterstitial;
            // Called when the ad click caused the user to leave the application.
            interstitialAd.OnAdLeavingApplication += HandleOnAdLeavingApplicationInterstitial;
        }
        else
        {
            // Called when an ad request has successfully loaded.
            interstitialAd.OnAdLoaded -= HandleOnAdLoadedInterstitial;
            // Called when an ad request failed to load.
            interstitialAd.OnAdFailedToLoad -= HandleOnAdFailedToLoadInterstitial;
            // Called when an ad is clicked.
            interstitialAd.OnAdOpening -= HandleOnAdOpenedInterstitial;
            // Called when the user returned from the app after an ad click.
            interstitialAd.OnAdClosed -= HandleOnAdClosedInterstitial;
            // Called when the ad click caused the user to leave the application.
            interstitialAd.OnAdLeavingApplication -= HandleOnAdLeavingApplicationInterstitial;
        }
    }

    public void HandleOnAdLoadedInterstitial(object sender, EventArgs args)
    {
        print("HandleAdLoaded event received");
        isInterstitalAdLoaded = true;
    }

    public void HandleOnAdFailedToLoadInterstitial(object sender, AdFailedToLoadEventArgs args)
    {
        RequestIntersialAd();
        print("Interstitial: Failed again " + args.Message);
    }

    public void HandleOnAdOpenedInterstitial(object sender, EventArgs args)
    {
        print("Interstitial: HandleAdOpened event received");
        isInterstialAdShown = true;
    }

    public void HandleOnAdClosedInterstitial(object sender, EventArgs args)
    {
        print("Interstitial: HandleAdClosed event received");
        isInterstialAdClosed = true;
        RequestIntersialAd();
    }

    public void HandleOnAdLeavingApplicationInterstitial(object sender, EventArgs args)
    {
        print("Interstitial: HandleAdLeavingApplication event received");
    }

    #endregion

    #region FirstRewardedAd
    //Rewarded Ad    

    
    public void ShowRewardedAd()
    {
        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }
    }
    public void HandleAdSubRewarded(bool sub)
    {
        if(sub)
        {           
            rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;            
            rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
            rewardedAd.OnAdOpening += HandleRewardedAdOpening;
            rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
            rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
            rewardedAd.OnAdClosed += HandleRewardedAdClosed;            
        }
        else
        {
            rewardedAd.OnAdLoaded -= HandleRewardedAdLoaded;
            rewardedAd.OnAdFailedToLoad -= HandleRewardedAdFailedToLoad;
            rewardedAd.OnAdOpening -= HandleRewardedAdOpening;
            rewardedAd.OnAdFailedToShow -= HandleRewardedAdFailedToShow;
            rewardedAd.OnUserEarnedReward -= HandleUserEarnedReward;
            rewardedAd.OnAdClosed -= HandleRewardedAdClosed;
        }
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        isRewardedAdLoaded = true;        

        print("Rewarded Ad: Loaded");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        RequestFirstRewardedAd();
        print("Rewarded Ad: failed");
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        print("Rewarded Ad: HandleRewardedAdOpening event received");
        //HandleAdSubRewarded(false);
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        print(
            "Rewarded Ad: HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
        RequestFirstRewardedAd(); 
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        print("Rewarded Ad: Ad Closed");        
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {        
        print("Rewarded Ad: Reward Taken");       
        
        isRewardEarned = true;
        isRewardedAdCloseTrigger = true;
        //bunları kullanacaz.
    }

    public void DoTheDirtyJobForRewardedAd()
    {
        if(isRewardEarned)
        {
            GameController.instance.RewardToContuinue();
        }        
        GameController.instance.RewardClosed();
        isRewardEarned = false;
        isRewardedAdLoaded = false;
        isRewardedAdCloseTrigger = false;
        RequestFirstRewardedAd();
        
    }
    #endregion 

    #region SecondRewardedAd
    //Rewarded Ad    
    public delegate void OnBurgerReward();
    public OnBurgerReward callBackOnBurgerReward;
    
    public void ShowBurgerRewardedAd()
    {
        if (rewardForBurger.IsLoaded())
        {
            rewardForBurger.Show();
        }
    }
    public void HandleAdSubBurgerRewarded(bool sub)
    {
        if (sub)
        {
            rewardForBurger.OnAdLoaded += HandleBurgerRewardedAdLoaded;
            rewardForBurger.OnAdFailedToLoad += HandleBurgerRewardedAdFailedToLoad;
            rewardForBurger.OnAdOpening += HandleBurgerRewardedAdOpening;
            rewardForBurger.OnAdFailedToShow += HandleBurgerRewardedAdFailedToShow;
            rewardForBurger.OnUserEarnedReward += HandleUserEarnedBurgerReward;
            rewardForBurger.OnAdClosed += HandleBurgerRewardedAdClosed;
        }
        else
        {
            rewardForBurger.OnAdLoaded -= HandleRewardedAdLoaded;
            rewardForBurger.OnAdFailedToLoad -= HandleBurgerRewardedAdFailedToLoad;
            rewardForBurger.OnAdOpening -= HandleBurgerRewardedAdOpening;
            rewardForBurger.OnAdFailedToShow -= HandleBurgerRewardedAdFailedToShow;
            rewardForBurger.OnUserEarnedReward -= HandleUserEarnedBurgerReward;
            rewardForBurger.OnAdClosed -= HandleBurgerRewardedAdClosed;
        }
    }

    public void HandleBurgerRewardedAdLoaded(object sender, EventArgs args)
    {
        isBurgerRewardedAdLoaded = true;
        print("BurgerRewarded Ad: Loaded");
    }

    public void HandleBurgerRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        RequestFirstRewardedAd();
        print("BurgerRewarded Ad: failed");
    }

    public void HandleBurgerRewardedAdOpening(object sender, EventArgs args)
    {
        print("BurgerRewarded Ad: HandleRewardedAdOpening event received");
        //HandleAdSubRewarded(false);
    }

    public void HandleBurgerRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        print(
            "BurgerRewarded Ad: HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
        RequestBurgerRewardedAd();       
    }

    public void HandleBurgerRewardedAdClosed(object sender, EventArgs args)
    {
        print("BurgerRewarded Ad: Ad Closed");
    }

    public void HandleUserEarnedBurgerReward(object sender, Reward args)
    {
        print("BurgerRewarded Ad: Reward Taken");
        isBurgerRewardEarned = true;
        isBurgerRewardedAdCloseTrigger = true;
        //bunları kullanacaz.
    }

    public void DoTheDirtyJobForBurgerRewardedAd()
    {        
        RequestBurgerRewardedAd();
        if (isBurgerRewardEarned)
        {//change it            
            callBackOnBurgerReward.Invoke();
        }
        isBurgerRewardEarned = false;
        isBurgerRewardedAdLoaded = false;
        isBurgerRewardedAdCloseTrigger = false;
        print("OK!");

    }
#if UNITY_EDITOR
    public void DebugReward()
    {
        print("BurgerRewarded Ad: Reward Taken");
        isBurgerRewardEarned = true;
        isBurgerRewardedAdCloseTrigger = true;
    }
#endif
#endregion

}

