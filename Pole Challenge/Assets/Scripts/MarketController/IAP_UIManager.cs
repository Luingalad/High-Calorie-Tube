using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IAP_UIManager : MonoBehaviour
{
    public Button Steak_100Button;
    public Button Steak_200Button;
    public Button Steak_500Button;
    public Button Steak_1500Button;
    public Button RemoveAdsButton;

    public Button CloseButton;

    void Start()
    {
        Steak_100Button.onClick.AddListener(BuySteak_100);
        Steak_200Button.onClick.AddListener(BuySteak_200);
        Steak_500Button.onClick.AddListener(BuySteak_500);
        Steak_1500Button.onClick.AddListener(BuySteak_1500);
        RemoveAdsButton.onClick.AddListener(BuyRemoveAds);
        CloseButton.onClick.AddListener(CloseMarket);

        if (ProfileManager.instance.isAdsRemoved)
        {
            RemoveAdsButton.interactable = false;
        }
        else
        {
            IAPManager.Instance.onCallBackRemoveAds += OnRemoveAds;
        }
        
    }

    private void OnDisable()
    {
        ProfileManager.instance.OnSave();
    }

    private void OnRemoveAds()
    {
        RemoveAdsButton.interactable = false;
    }

    private void BuySteak_100()
    {
        IAPManager.Instance.Buy100Steak();
    }
    private void BuySteak_200()
    {
        IAPManager.Instance.Buy200Steak();
    }

    private void BuySteak_500()
    {
        IAPManager.Instance.Buy500Steak();
    }
    private void BuySteak_1500()
    {
        IAPManager.Instance.Buy1500Steak();
    }
    private void BuyRemoveAds()
    {
        IAPManager.Instance.BuyRemoveAd();
    }

    private void CloseMarket()
    {
        gameObject.SetActive(false);
    }
}
