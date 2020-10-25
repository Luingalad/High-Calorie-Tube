using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudValues : MonoBehaviour
{
    #region Singleton
    public static CloudValues instance;
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
    }
    #endregion Singleton

    public List<int> JSONIntegerList;
    public static int[] JSONIntegerArray;
    
    private readonly string hasLocalSaveString = "LocalSaveStatus";
    public int hasLocalSave;   

    void Start()
    {
        hasLocalSave = PlayerPrefs.GetInt(hasLocalSaveString, 0);
    }

    void Update()
    {
        
    }

    public void LocalSaveState()
    {
        if (hasLocalSave == 0)
        {
            hasLocalSave = 1;
            PlayerPrefs.SetInt(hasLocalSaveString, hasLocalSave);
        }
     
    }

    public void BuildJSONIntegerArray()
    {
        JSONIntegerList = new List<int>();        
        JSONIntegerList.Add(ProfileManager.instance.Steak);
        JSONIntegerList.Add(ProfileManager.instance.Burger);
        JSONIntegerList.Add(ProfileManager.instance.Record);
        JSONIntegerList.Add(ProfileManager.instance.isAdsRemoved ? 1 : 0);
        JSONIntegerList.Add(ProfileManager.instance.InGameSpecialty1);
        JSONIntegerList.Add(ProfileManager.instance.InGameSpecialty2);
        JSONIntegerList.Add(ProfileManager.instance.InGameSpecialty3);
        JSONIntegerList.Add(ProfileManager.instance.InGameSpecialty4);
        JSONIntegerList.Add(ProfileManager.instance.InGameSpecialty5);
        JSONIntegerList.Add(BonusManager.instance.SRLevel);
        JSONIntegerList.Add(BonusManager.instance.PDLevel);
        JSONIntegerList.Add(BonusManager.instance.MDLevel);
        JSONIntegerList.Add(BonusManager.instance.BPLevel);
        JSONIntegerList.Add(BonusManager.instance.SMLevel);
        JSONIntegerList.Add(BonusManager.instance.SPLevel);
        JSONIntegerList.Add(BonusManager.instance.RPLevel);
        JSONIntegerList.Add(BonusManager.instance.RMLevel);

        JSONIntegerArray = JSONIntegerList.ToArray();

    }

    public void GetArrayFromCloud(int[] array)
    {
        JSONIntegerArray = array;
        ProfileManager.instance.OnLoad(array);
        LoadingSceen.instance.CloseLoadingScene();
    }
}
