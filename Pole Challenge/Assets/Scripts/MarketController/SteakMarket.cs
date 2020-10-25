using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SteakMarket : MonoBehaviour
{
    [Header(header: "Gold")]
    public Button GoldButton1; // 1 steak = 1k burger
    public Button GoldButton2; // 5 steak = 7.5k burger
    public Button GoldButton3; // 20 steak = 40k burger
    public Button GoldButton4; // 100 steak = 250k burger
    public Button GoldButton5; // 500 steak = 1.5m burger

    [Header(header: "In-game")]
    public Button InGameButton1; //Double Second Chance
    public Button InGameButton2; //Magnet can take magnet only once
    public Button InGameButton3; //Magnet can take protection only once
    public Button InGameButton4; //Double Burger Buff in random block
    public Button InGameButton5; //Guardian angel => Prevent your dead with 50% possibilty. Only once in a run

    public TMP_Text InGameCount1;
    public TMP_Text InGameCount2;
    public TMP_Text InGameCount3;
    public TMP_Text InGameCount4;
    public TMP_Text InGameCount5;

    [Header(header: "BackButton")]
    public Button BackButton;
    
    [Header(header: "YesNoPanel")]
    public GameObject YesNoPanel;
    public Button YesButton;
    public Button NoButton;
    public TMP_Text Explanations;
    public LeanLocalizedTextMeshProUGUI ExplanationsLean;

    [Header(header: "Steak/Burger")]
    public TMP_Text steakCount;
    public TMP_Text burgerCount;

    /*private string[] GoldStr = {"You will buy\n"," x Burger\nFor\n"," x Steak"};*/
    public LeanPhrase BurgerString;
    public LeanToken BurgerToken;
    public LeanToken SteakToken;
    private int[] SteakToBurgerSteak = { 1, 5, 20, 100, 500 };
    private int[] SteakToBurgerBurger = { 500, 3750, 20000, 125000, 750000 };
    public LeanPhrase[] IngameStrings;
    public LeanToken IngameSteakToken;

    /*
    private string[] IngameStr = {"Specialty: Double Second Chance\nYou can use the second chance twice.\nPrice: "
            , "Specialty: Magnetic Donut\nMagnet can pull Donut only once.\nPrice: "
            , "Specialty: Magnetic Drink\nMagnet can pull Drink only once\nPrice: "
            , "Specialty: Double Burger Buff\nYou can earn Double Burger Buff in a random block.\nPrice: " 
            , "Specialty: Guardian Angel\nGuardian Angel become active with a 50% probability\nwhen Runner receives a deadly hit.\n" +
            "Only actives once in a game.\nPrice: "};*/
    private int[] IngameSteak = { 10, 12, 12, 12, 10 };

    

    private int Selection;

    void Start()
    {        
        ProfileManager.instance.callBackOnRefresh += UpdateSteakCount;
        UpdateSteakCount();

        GoldButton1.onClick.AddListener(OnClickGoldButton1);
        GoldButton2.onClick.AddListener(OnClickGoldButton2);
        GoldButton3.onClick.AddListener(OnClickGoldButton3);
        GoldButton4.onClick.AddListener(OnClickGoldButton4);
        GoldButton5.onClick.AddListener(OnClickGoldButton5);

        InGameButton1.onClick.AddListener(OnClickIngameButton1);
        InGameButton2.onClick.AddListener(OnClickIngameButton2);
        InGameButton3.onClick.AddListener(OnClickIngameButton3);
        InGameButton4.onClick.AddListener(OnClickIngameButton4);
        InGameButton5.onClick.AddListener(OnClickIngameButton5);

        Refresh();

        BackButton.onClick.AddListener(OnClickBackButton);

        YesButton.onClick.AddListener(OnClickYesButton);
        NoButton.onClick.AddListener(OnClickNoButton);
    }

    #region Burger
    private void OnClickGoldButton1()
    {
        Selection = 1;
        //ProfileManager.instance.callBackOnSteakChanged.Invoke();

        if (ProfileManager.instance.Steak < SteakToBurgerSteak[Selection - 1])
        {
            YesButton.interactable = false;
        }
        else
        {
            YesButton.interactable = true;
        }

        
        BurgerToken.Value = SteakToBurgerBurger[Selection - 1].ToString();
        SteakToken.Value = SteakToBurgerSteak[Selection - 1].ToString();
        ExplanationsLean.TranslationName = BurgerString.name;

        YesNoPanel.SetActive(true);
    }
    private void OnClickGoldButton2()
    {
        Selection = 2;

        if (ProfileManager.instance.Steak < SteakToBurgerSteak[Selection - 1])
        {
            YesButton.interactable = false;
        }
        else
        {
            YesButton.interactable = true;
        }

        BurgerToken.Value = SteakToBurgerBurger[Selection - 1].ToString();
        SteakToken.Value = SteakToBurgerSteak[Selection - 1].ToString();
        ExplanationsLean.TranslationName = BurgerString.name;

        YesNoPanel.SetActive(true);
    }
    private void OnClickGoldButton3()
    {
        Selection = 3;

        if (ProfileManager.instance.Steak < SteakToBurgerSteak[Selection - 1])
        {
            YesButton.interactable = false;
        }
        else
        {
            YesButton.interactable = true;
        }

        BurgerToken.Value = SteakToBurgerBurger[Selection - 1].ToString();
        SteakToken.Value = SteakToBurgerSteak[Selection - 1].ToString();
        ExplanationsLean.TranslationName = BurgerString.name;

        YesNoPanel.SetActive(true);
    }
    private void OnClickGoldButton4()
    {
        Selection = 4;

        if (ProfileManager.instance.Steak < SteakToBurgerSteak[Selection - 1])
        {
            YesButton.interactable = false;
        }
        else
        {
            YesButton.interactable = true;
        }

        BurgerToken.Value = SteakToBurgerBurger[Selection - 1].ToString();
        SteakToken.Value = SteakToBurgerSteak[Selection - 1].ToString();
        ExplanationsLean.TranslationName = BurgerString.name;

        YesNoPanel.SetActive(true);
    }
    private void OnClickGoldButton5()
    {
        Selection = 5;

        if (ProfileManager.instance.Steak < SteakToBurgerSteak[Selection - 1])
        {
            YesButton.interactable = false;
        }
        else
        {
            YesButton.interactable = true;
        }

        BurgerToken.Value = SteakToBurgerBurger[Selection - 1].ToString();
        SteakToken.Value = SteakToBurgerSteak[Selection - 1].ToString();
        ExplanationsLean.TranslationName = BurgerString.name;

        YesNoPanel.SetActive(true);
    }
    #endregion

    #region InGame
    private void OnClickIngameButton1()
    {
        Selection = 6;

        if (ProfileManager.instance.Steak < IngameSteak[Selection - 6])
        {
            YesButton.interactable = false;
        }
        else
        {
            YesButton.interactable = true;
        }

        IngameSteakToken.Value = IngameSteak[Selection - 6].ToString();
        ExplanationsLean.TranslationName = IngameStrings[Selection - 6].name;

        YesNoPanel.SetActive(true);
    }
    private void OnClickIngameButton2()
    {
        Selection = 7;

        if (ProfileManager.instance.Steak < IngameSteak[Selection - 6])
        {
            YesButton.interactable = false;
        }
        else
        {
            YesButton.interactable = true;
        }

        IngameSteakToken.Value = IngameSteak[Selection - 6].ToString();
        ExplanationsLean.TranslationName = IngameStrings[Selection - 6].name;

        YesNoPanel.SetActive(true);
    }
    private void OnClickIngameButton3()
    {
        Selection = 8;

        if (ProfileManager.instance.Steak < IngameSteak[Selection - 6])
        {
            YesButton.interactable = false;
        }
        else
        {
            YesButton.interactable = true;
        }

        IngameSteakToken.Value = IngameSteak[Selection - 6].ToString();
        ExplanationsLean.TranslationName = IngameStrings[Selection - 6].name;

        YesNoPanel.SetActive(true);
    }
    private void OnClickIngameButton4()
    {
        Selection = 9;

        if (ProfileManager.instance.Steak < IngameSteak[Selection - 6])
        {
            YesButton.interactable = false;
        }
        else
        {
            YesButton.interactable = true;
        }

        IngameSteakToken.Value = IngameSteak[Selection - 6].ToString();
        ExplanationsLean.TranslationName = IngameStrings[Selection - 6].name;

        YesNoPanel.SetActive(true);
    }
    private void OnClickIngameButton5()
    {
        Selection = 10;

        if (ProfileManager.instance.Steak < IngameSteak[Selection - 6])
        {
            YesButton.interactable = false;
        }
        else
        {
            YesButton.interactable = true;
        }

        IngameSteakToken.Value = IngameSteak[Selection - 6].ToString();
        ExplanationsLean.TranslationName = IngameStrings[Selection - 6].name;

        YesNoPanel.SetActive(true);
    }
    #endregion

    private void OnClickBackButton()
    {
        AdMobController.instance.ShowBanner();
        ProfileManager.instance.OnSave();
        gameObject.SetActive(false);
    }

    private void OnClickYesButton()
    {
        if (Selection <= 5)
        {
            ProfileManager.instance.callBackOnBurgerChanged.Invoke(SteakToBurgerBurger[Selection - 1]);
            ProfileManager.instance.callBackOnSteakChanged.Invoke(-SteakToBurgerSteak[Selection - 1]);
        }
        else
        {
            if (Selection == 6)
            {
                ProfileManager.instance.callBackOnIngamebought1.Invoke(true);
            }
            else if (Selection == 7)
            {
                ProfileManager.instance.callBackOnIngamebought2.Invoke(true);
            }
            else if (Selection == 8)
            {
                ProfileManager.instance.callBackOnIngamebought3.Invoke(true);
            }
            else if (Selection == 9)
            {
                ProfileManager.instance.callBackOnIngamebought4.Invoke(true);
            }
            else if (Selection == 10)
            {
                ProfileManager.instance.callBackOnIngamebought5.Invoke(true);
            }
            ProfileManager.instance.callBackOnSteakChanged.Invoke(-IngameSteak[Selection - 6]);
            Refresh();
        }

        YesNoPanel.SetActive(false);
    }

    private void OnClickNoButton()
    {
        YesNoPanel.SetActive(false);
    }

    private void UpdateSteakCount()
    {
        steakCount.text = ProfileManager.instance.Steak.ToString();
        burgerCount.text = ProfileManager.instance.Burger.ToString();
    }
    private void Refresh()
    {
        InGameCount1.text = ProfileManager.instance.InGameSpecialty1.ToString();
        InGameCount2.text = ProfileManager.instance.InGameSpecialty2.ToString();
        InGameCount3.text = ProfileManager.instance.InGameSpecialty3.ToString();
        InGameCount4.text = ProfileManager.instance.InGameSpecialty4.ToString();
        InGameCount5.text = ProfileManager.instance.InGameSpecialty5.ToString();
    }
}
