//08.08.2019 is my birthday
//Created by Ertuğ "Mithrildir" Oğuz

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileManager : MonoBehaviour
{
    public delegate void OnGameFinished();
    public OnGameFinished callBackOnGameFinished;

    public delegate void OnSteakChanged(int steak);
    public OnSteakChanged callBackOnSteakChanged;

    public delegate void OnGoldChanged(int gold);
    public OnGoldChanged callBackOnBurgerChanged;

    public delegate void OnRefresh();
    public OnRefresh callBackOnRefresh;
        
    public int Burger { get; private set; }
    public int Steak { get; private set; }
    public int Record { get; private set; }
    public bool isAdsRemoved { get; private set; }

    private string burgerString = "Gold";
    private string steakString = "Steak";
    private string recordString = "Record";
    private string adsRemovedString = "AdsRemoved";

    #region Specialties
    public delegate void OnInGameBought(bool b);
    public OnInGameBought callBackOnIngamebought1;
    public OnInGameBought callBackOnIngamebought2;
    public OnInGameBought callBackOnIngamebought3;
    public OnInGameBought callBackOnIngamebought4;
    public OnInGameBought callBackOnIngamebought5;

    public int InGameSpecialty1 { get; private set; }
    public int InGameSpecialty2 { get; private set; }
    public int InGameSpecialty3 { get; private set; }
    public int InGameSpecialty4 { get; private set; }
    public int InGameSpecialty5 { get; private set; }

    private readonly string InGameSpecialtyString1 = "InGameSpecialty1";
    private readonly string InGameSpecialtyString2 = "InGameSpecialty2";
    private readonly string InGameSpecialtyString3 = "InGameSpecialty3";
    private readonly string InGameSpecialtyString4 = "InGameSpecialty4";
    private readonly string InGameSpecialtyString5 = "InGameSpecialty5";
    #endregion

    #region Singleton
    public static ProfileManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {            
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    
    #endregion

    private void Start()
    {
        callBackOnGameFinished += GameFinished;
        callBackOnSteakChanged += SteakChanged;        
        callBackOnBurgerChanged += GoldChanged;

        Steak = PlayerPrefs.GetInt(steakString);
        Burger = PlayerPrefs.GetInt(burgerString);
        Record = PlayerPrefs.GetInt(recordString);

        callBackOnIngamebought1 += changeInGameSpecialty1;
        callBackOnIngamebought2 += changeInGameSpecialty2;
        callBackOnIngamebought3 += changeInGameSpecialty3;
        callBackOnIngamebought4 += changeInGameSpecialty4;
        callBackOnIngamebought5 += changeInGameSpecialty5;

        InGameSpecialty1 = PlayerPrefs.GetInt(InGameSpecialtyString1, 0);
        InGameSpecialty2 = PlayerPrefs.GetInt(InGameSpecialtyString2, 0);
        InGameSpecialty3 = PlayerPrefs.GetInt(InGameSpecialtyString3, 0);
        InGameSpecialty4 = PlayerPrefs.GetInt(InGameSpecialtyString4, 0);
        InGameSpecialty5 = PlayerPrefs.GetInt(InGameSpecialtyString5, 0);

        isAdsRemoved = PlayerPrefs.GetInt(adsRemovedString, 0) == 1;

        IAPManager.Instance.onCallBackRemoveAds += OnAdsRemoved;        
    }

    private void GameFinished()
    {
        int _record = Scores.instance.ScorePoint;
        if (_record > Record)
        {
            Record = _record;
        }

        Burger += Scores.instance.BurgerCount;

        if(InGameSpecialty1 > 0)
            callBackOnIngamebought1.Invoke(false);
        if (InGameSpecialty2 > 0)
            callBackOnIngamebought2.Invoke(false);
        if (InGameSpecialty3 > 0)
            callBackOnIngamebought3.Invoke(false);
        if (InGameSpecialty4 > 0)
            callBackOnIngamebought4.Invoke(false);
        if (InGameSpecialty5 > 0)
            callBackOnIngamebought5.Invoke(false);

        PlayerPrefs.SetInt(burgerString, Burger);
        PlayerPrefs.SetInt(recordString, Record);
        PlayerPrefs.Save();
        callBackOnRefresh.Invoke();
    }

    private void SteakChanged(int _steak)
    {
        Steak += _steak;
        PlayerPrefs.SetInt(steakString, Steak);
        PlayerPrefs.Save();
        callBackOnRefresh.Invoke();        
    }

    private void GoldChanged(int _gold)
    {
        Burger += _gold;
        PlayerPrefs.SetInt(burgerString, Burger);
        PlayerPrefs.Save();
        callBackOnRefresh.Invoke();        
    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        Steak = PlayerPrefs.GetInt(steakString);
        Burger = PlayerPrefs.GetInt(burgerString);
        Record = PlayerPrefs.GetInt(recordString);
        var objects = FindObjectsOfType<GameObject>();
        foreach(GameObject g in objects)
        {
            Destroy(g);
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);        
    }

    private void changeInGameSpecialty1(bool b)
    {
        int i = b ? 1 : -1;
        InGameSpecialty1 += i;
        PlayerPrefs.SetInt(InGameSpecialtyString1, InGameSpecialty1);
        PlayerPrefs.Save();
    }
    private void changeInGameSpecialty2(bool b)
    {        
        int i = b ? 1 : -1;
        InGameSpecialty2 += i;
        PlayerPrefs.SetInt(InGameSpecialtyString2, InGameSpecialty2);
        PlayerPrefs.Save();
    }
    private void changeInGameSpecialty3(bool b)
    {
        int i = b ? 1 : -1;
        InGameSpecialty3 += i;
        PlayerPrefs.SetInt(InGameSpecialtyString3, InGameSpecialty3);
        PlayerPrefs.Save();
    }
    private void changeInGameSpecialty4(bool b)
    {
        int i = b ? 1 : -1;
        InGameSpecialty4 += i;
        PlayerPrefs.SetInt(InGameSpecialtyString4, InGameSpecialty4);
        PlayerPrefs.Save();
    }
    private void changeInGameSpecialty5(bool b)
    {
        int i = b ? 1 : -1;
        InGameSpecialty5 += i;
        PlayerPrefs.SetInt(InGameSpecialtyString5, InGameSpecialty5);
        PlayerPrefs.Save();
    }

    private void OnAdsRemoved()
    {
        AdMobController.instance.DestroyBanner();
        AdMobController.instance.DestroyInterstital();
        isAdsRemoved = true;
        PlayerPrefs.SetInt(adsRemovedString, 1);
    }

    public void OnSave()
    {
        CloudValues.instance.BuildJSONIntegerArray();
        GPG_Controller.instance.GetStringFromServer(true);
    }

    public void OnLoad(int[] array)
    {
        Steak = array[0];
        PlayerPrefs.SetInt(steakString, Steak);
        Burger = array[1];
        PlayerPrefs.SetInt(burgerString, Burger);
        Record = array[2];
        PlayerPrefs.SetInt(recordString, Record);
        isAdsRemoved = array[3] == 1;
        PlayerPrefs.SetInt(adsRemovedString, array[3]);
        InGameSpecialty1 = array[4];
        PlayerPrefs.SetInt(InGameSpecialtyString1, InGameSpecialty1);
        InGameSpecialty2 = array[5];
        PlayerPrefs.SetInt(InGameSpecialtyString2, InGameSpecialty2);
        InGameSpecialty3 = array[6];
        PlayerPrefs.SetInt(InGameSpecialtyString3, InGameSpecialty3);
        InGameSpecialty4 = array[7];
        PlayerPrefs.SetInt(InGameSpecialtyString4, InGameSpecialty4);
        InGameSpecialty5 = array[8];
        PlayerPrefs.SetInt(InGameSpecialtyString5, InGameSpecialty5);
        PlayerPrefs.Save();

        BonusManager.instance.SRCalculateWithLevel(array[9]);
        BonusManager.instance.PDCalculateWithLevel(array[10]);
        BonusManager.instance.MDCalculateWithLevel(array[11]);
        BonusManager.instance.BPCalculateWitLevel(array[12]);
        BonusManager.instance.SMCalculateWitLevel(array[13]);
        BonusManager.instance.SPCalculateWitLevel(array[14]);
        BonusManager.instance.RPCalculateWitLevel(array[15]);
        BonusManager.instance.RMCalculateWitLevel(array[16]);
        Debug.Log("All things loaded");
        callBackOnRefresh.Invoke();
        /*
        JSONIntegerList.Add(ProfileManager.instance.Steak);     0
        JSONIntegerList.Add(ProfileManager.instance.Burger);    1
        JSONIntegerList.Add(ProfileManager.instance.Record);    2
        JSONIntegerList.Add(ProfileManager.instance.isAdsRemoved ? 1 : 0);  3
        JSONIntegerList.Add(ProfileManager.instance.InGameSpecialty1);      4
        JSONIntegerList.Add(ProfileManager.instance.InGameSpecialty2);      5
        JSONIntegerList.Add(ProfileManager.instance.InGameSpecialty3);      6
        JSONIntegerList.Add(ProfileManager.instance.InGameSpecialty4);      7
        JSONIntegerList.Add(ProfileManager.instance.InGameSpecialty5);      8
        JSONIntegerList.Add(BonusManager.instance.SRLevel);     9
        JSONIntegerList.Add(BonusManager.instance.PDLevel);     10
        JSONIntegerList.Add(BonusManager.instance.MDLevel);     11
        JSONIntegerList.Add(BonusManager.instance.BPLevel);     12
        JSONIntegerList.Add(BonusManager.instance.SMLevel);     13
        JSONIntegerList.Add(BonusManager.instance.SPLevel);     14
        JSONIntegerList.Add(BonusManager.instance.RPLevel);     15
        JSONIntegerList.Add(BonusManager.instance.RMLevel);     16*/
    }
}
