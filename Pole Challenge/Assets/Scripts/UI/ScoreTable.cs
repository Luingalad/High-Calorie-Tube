using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Lean.Localization;

public class ScoreTable : MonoBehaviour
{
    [Header(header: "UI Elements")]
    public LeanToken ScorePoint;
    public LeanToken BurgerPoint;
    public LeanLocalizedTextMeshProUGUI BurgerPointText;

    public Button CloseButton;  

    public Button PlayAgainButton;   

    public Button DoubleBurger;    

    [Header(header: "Score Values")]
    public int Burger;
    public int Score;        
    
    [Header(header: "Scenes")]
    public int GameScene;
    public int MenuScene;    
    private void Start()
    {
        CloseButton.onClick.AddListener(OnMenuButtonClick);
        PlayAgainButton.onClick.AddListener(OnPlayAgainButtonClick);
        DoubleBurger.onClick.AddListener(OnDoubleBurgerButtonClick);
    }
    private void OnEnable()
    {
        Burger = Scores.instance.BurgerCount;
        Score = Scores.instance.ScorePoint;
        ScorePoint.SetValue(Score.ToString() + " m");
        BurgerPoint.SetValue(Burger.ToString());

        ProfileManager.instance.callBackOnGameFinished.Invoke();
#if !UNITY_EDITOR
        if(!AdMobController.instance.isBurgerRewardedAdLoaded)
        {
            DoubleBurger.interactable = false;
        }
#else

        DoubleBurger.interactable = true;
#endif
    }
    private void UpdateBurgerCount()
    {
        ProfileManager.instance.callBackOnBurgerChanged(Scores.instance.BurgerCount);
        AdMobController.instance.callBackOnBurgerReward -= UpdateBurgerCount;
        int newBurger = Burger * 2;
        Scores.instance.BurgerCount *= 2;
        //BurgerPoint.SetValue(newBurger.ToString());
        //BurgerPoint.Value = newBurger.ToString();
        //Debug.Log(BurgerPoint.Value);
        //BurgerPointText.enabled = true;
        //BurgerPointText.UpdateLocalization();
    }
    public void OnMenuButtonClick()
    {
        if (AdMobController.instance.isInterstitalAdLoaded && !ProfileManager.instance.isAdsRemoved)
        {
            IntersialAdController.instance.ShowIntersialAd(MenuScene);            
        }
        else
        {
            SceneManager.LoadScene(MenuScene);
        }
    }
    private void OnPlayAgainButtonClick()
    {
        if(AdMobController.instance.isInterstitalAdLoaded && !ProfileManager.instance.isAdsRemoved)
        {
            IntersialAdController.instance.ShowIntersialAd(GameScene);            
        }
        else
        {
            SceneManager.LoadScene(GameScene);
        }        
    }
    private void OnDoubleBurgerButtonClick()
    {
        //2. rewarded ad açılır
        DoubleBurger.interactable = false;
        AdMobController.instance.callBackOnBurgerReward += UpdateBurgerCount;
#if !UNITY_EDITOR
        AdMobController.instance.ShowBurgerRewardedAd();
#else
        AdMobController.instance.DebugReward();
        Debug.Log("DebugClick");
#endif
    }

}
