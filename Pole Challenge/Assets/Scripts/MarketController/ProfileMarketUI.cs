using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProfileMarketUI : MonoBehaviour
{
    [Header(header: "Gold")]
    public TMP_Text GoldCount;

    public Button BackButton;

    private BonusManager bonusM;
    private ProfileManager profile;

    public delegate void SomethingChange();
    public SomethingChange callBackSometingChange;
    void Start()
    {
        bonusM = BonusManager.instance;
        profile = ProfileManager.instance;
                
        BackButton.onClick.AddListener(CloseMarket);
        UpdateMarket();
        callBackSometingChange += UpdateMarket;
    }

    private void OnEnable()
    {
        if (callBackSometingChange != null)
        {
            callBackSometingChange.Invoke();
        }
    }

    public void Buying(string item)
    {
        switch(item)
        {
            case "SR":
                {
                    profile.callBackOnBurgerChanged.Invoke(-bonusM.SRPrice);
                    bonusM.SpeedReductionLevelUp();
                    callBackSometingChange.Invoke();
                    break;
                }
            case "PD":
                {
                    profile.callBackOnBurgerChanged.Invoke(-bonusM.PDPrice);
                    bonusM.ProtectionDurationLevelUp();
                    callBackSometingChange.Invoke();
                    break;
                }
            case "MD":
                {
                    profile.callBackOnBurgerChanged.Invoke(-bonusM.MDPrice);
                    bonusM.MagnetDurationLevelUp();
                    callBackSometingChange.Invoke();
                    break;
                }
            case "BP":
                {
                    profile.callBackOnBurgerChanged.Invoke(-bonusM.BPPrice);
                    bonusM.BlockPowerReductionLevelUp();
                    callBackSometingChange.Invoke();
                    break;
                }
            case "SM":
                {
                    profile.callBackOnBurgerChanged.Invoke(-bonusM.SMPrice);
                    bonusM.StartMagnetLevelUp();
                    callBackSometingChange.Invoke();
                    break;                    
                }
            case "SP":
                {
                    profile.callBackOnBurgerChanged.Invoke(-bonusM.SPPrice);
                    bonusM.StartProtectionLevelUp();
                    callBackSometingChange.Invoke();
                    break;
                }
            case "RP":
                {
                    profile.callBackOnBurgerChanged.Invoke(-bonusM.RPPrice);
                    bonusM.RespawnProtectionLevelUp();
                    callBackSometingChange.Invoke();
                    break;
                }
            case "RM":
                {
                    profile.callBackOnBurgerChanged.Invoke(-bonusM.RPPrice);
                    bonusM.RespawnMagnetLevelUp();
                    callBackSometingChange.Invoke();
                    break;
                }
        }
    }

    private void UpdateMarket()
    {
        GoldCount.text = profile.Burger.ToString();
    }

    private void CloseMarket()
    {
        gameObject.SetActive(false);
        AdMobController.instance.ShowBanner();
    }

    private void OnDisable()
    {
        ProfileManager.instance.OnSave();
    }

}
