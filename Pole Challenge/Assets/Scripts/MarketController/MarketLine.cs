using UnityEngine;
using UnityEngine.UI;
using Lean.Localization;

public class MarketLine : MonoBehaviour
{ 
    public LeanLocalizedTextMeshProUGUI Name;
    public LeanLocalizedTextMeshProUGUI Price;
    public LeanLocalizedTextMeshProUGUI Bonus;


    public LeanToken NameToken;        
    public LeanToken PriceToken;
    public LeanToken CurrentBonus;

    public Button BuyButton;
    
    private BonusManager bonusManager;
    public MarketItemScriptableObject item;

    public ProfileMarketUI marketUI;
    

    private int price;
    private float bonus;
    private int maxLevel;
    private int level;
    void Start()
    {
        marketUI.callBackSometingChange += UpdateUI;
        
        BuyButton.onClick.AddListener(onClickBuyButton);
        bonusManager = BonusManager.instance;
        marketUI.callBackSometingChange.Invoke();
        CheckPrice();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void onClickBuyButton()
    {
        if (ProfileManager.instance.Burger >= price && maxLevel > level)
        {
            marketUI.Buying(item.code);
            Debug.Log("Has been bought");
            marketUI.callBackSometingChange.Invoke();
        }
        else
        {
            Debug.Log("Cant Buy");
        }
    }

    private void UpdateUI()
    {
        string text = bonusManager.GetValues(item.code);
        string[] txt = text.Split(':');
        price = int.Parse(txt[0]);
        bonus = float.Parse(txt[1]);
        level = int.Parse(txt[2]);
        maxLevel = int.Parse(txt[3]);
        
        NameToken.SetValue(level + "/" + maxLevel) ;   
        PriceToken.SetValue(price);

        string format = null;
        if (item.isPercentage)
            format = "P";
        CurrentBonus.SetValue(bonus.ToString(format));

        if(maxLevel == level)
        {
            BuyButton.interactable = false;
        }

        Name.UpdateLocalization();
        Price.UpdateLocalization();
        Bonus.UpdateLocalization();
        CheckPrice();
    }

    private void CheckPrice()
    {
        if (ProfileManager.instance.Burger >= price && maxLevel > level)
        {
            BuyButton.interactable = true;
        }
        else
        {
            BuyButton.interactable = false;
        }
    }
}
