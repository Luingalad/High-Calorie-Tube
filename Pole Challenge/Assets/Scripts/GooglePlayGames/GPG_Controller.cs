using System;
using System.Text;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine;

public class GPG_Controller : MonoBehaviour
{
    #region Singleton
    public static GPG_Controller instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    #endregion Singleton

    public static PlayGamesPlatform playGames;
    
    void Start()
    {
        /*
        string s = "637037213194872100";
        long a = long.Parse(s);
        Debug.Log(a.ToString() + " " + s);
        */
        callBackOnSave += GetStringFromServer;
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        // enables saving game progress.        
        .EnableSavedGames()                        
        .Build();        
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        if(!PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.localUser.Authenticate(SignInCallBack);

        }
        playGames = PlayGamesPlatform.Instance;
        
        if(PlayerPrefs.HasKey(SAVE_DATE))
        {
            LastUpdateDate = PlayerPrefs.GetString(SAVE_DATE);
        }
        else
        {
            LastUpdateDate = DateTime.MinValue.Ticks.ToString();
                PlayerPrefs.SetString(SAVE_DATE, LastUpdateDate);
        }
         
    }

    public delegate void onLogIn();
    public onLogIn callBackSuccess;
    public onLogIn callBackFail;
    public onLogIn callBackSignOut;
    public delegate void onSave(bool save);
    public onSave callBackOnSave;
    private bool isSaving;

    #region SignIn
    public void GPGLogIn()
    {        
        if(!PlayGamesPlatform.Instance.localUser.authenticated)
        {
            Social.localUser.Authenticate(SignInCallBack);
        }      
    }   

    private void SignInCallBack(bool success, string msg)
    {
        if (success)
        {
            Debug.Log("success" + msg );
            GetStringFromServer(false); //Load
            if (callBackSuccess != null)
                callBackSuccess.Invoke();
        }
        else
        {
            Debug.Log("fail " + msg);
            if (callBackFail != null)
                callBackFail.Invoke();
            if (LoadingSceen.instance.gameObject.activeInHierarchy)
                LoadingSceen.instance.CloseLoadingScene();
        }
    }

    #endregion

    #region ShowUI
    public void ShowAchievements()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
            Social.ShowAchievementsUI();
        else
            GPGLogIn();
    }
    
    public void ShowLeaderBoard()
    {        
        if (PlayGamesPlatform.Instance.localUser.authenticated)
            Social.ShowLeaderboardUI();
        else
            GPGLogIn();
    }
    #endregion ShowUI

    #region UnLockAchievements
    //Welcome
    public void Achievements_1000m()
    {
        Debug.Log("Achievement Unlock Call 1000m");
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.UnlockAchievement(
            GPGSIds.achievement_reach_to_1000_m, (bool success) =>
            {
                // handle success or failure
                callBackGooglePlay(success, "Run 1.000m");
            });
        }
    }
    public void Achievements_2000m()
    {
        Debug.Log("Achievement Unlock Call 2000m");
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.UnlockAchievement(
            GPGSIds.achievement_reach_to_2000_m, (bool success) =>
            {
                // handle success or failure
                callBackGooglePlay(success, "Run 2.000m");
            });
        }
    }

    //1000meter
    public void Achievements_5000m()
    {
        Debug.Log("Achievement Unlock Call 5000m");
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.UnlockAchievement(
            GPGSIds.achievement_reach_to_5000_m, (bool success) =>
            {
                // handle success or failure
                callBackGooglePlay(success, "Run 5.000m");
            });
        }
    }

    //2000meter
    public void Achievements_10000m()
    {
        Debug.Log("Achievement Unlock Call 10000m");
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.UnlockAchievement(
            GPGSIds.achievement_run_to_10000_m, (bool success) =>
            {
                // handle success or failure
                callBackGooglePlay(success, "Run 10.000m");
            });
        }
    }

    //3500meter
    public void Achievements_Welcome()
    {
        Debug.Log("Achievement Unlock Call Welcome");
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.UnlockAchievement(
            GPGSIds.achievement_welcome_to_high_calorie_tube, (bool success) =>
            {
                // handle success or failure
                callBackGooglePlay(success, "Welcome Achievement");
            });
        }
    }

    //5000meter
    public void Achievements_Pizza50()
    {
        Debug.Log("Achievement Unlock Call Pizza");
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.UnlockAchievement(
            GPGSIds.achievement_gather_50_pizza, (bool success) =>
            {
                // handle success or failure
                callBackGooglePlay(success, "Gather 50 Pizza");
            });
        }
    }
    public void Achievements_Pizza100()
    {
        Debug.Log("Achievement Unlock Call Pizza");
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.UnlockAchievement(
            GPGSIds.achievement_gather_100_pizza, (bool success) =>
            {
                // handle success or failure
                callBackGooglePlay(success, "Gather 100 Pizza");
            });
        }
    }
    public void Achievements_Pizza200()
    {
        Debug.Log("Achievement Unlock Call Pizza");
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.UnlockAchievement(
            GPGSIds.achievement_gather_200_pizza, (bool success) =>
            {
                // handle success or failure
                callBackGooglePlay(success, "Gather 200 Pizza");
            });
        }
    }
    public void Achievements_Burger250()
    {
        Debug.Log("Achievement Unlock Call Burger");
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.UnlockAchievement(
            GPGSIds.achievement_gather_250_burgers, (bool success) =>
            {
                // handle success or failure
                callBackGooglePlay(success, "Gather 250 Burger");
            });
        }
    }

    public void Achievements_Burger500()
    {
        Debug.Log("Achievement Unlock Call Burger");
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.UnlockAchievement(
            GPGSIds.achievement_gather_500_burgers, (bool success) =>
            {
                // handle success or failure
                callBackGooglePlay(success, "Gather 500 Burger");
            });
        }
    }
    public void Achievements_Burger1000()
    {
        Debug.Log("Achievement Unlock Call Burger");
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.UnlockAchievement(
            GPGSIds.achievement_gather_1000_burgers, (bool success) =>
            {
                // handle success or failure
                callBackGooglePlay(success, "Gather 1000 Burger");
            });
        }
    }
    #endregion UnLockAchievements

    #region LeaderBoard
    public void ScoreToLeaderBoard(int score, int burger)
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            Social.ReportScore(score, GPGSIds.leaderboard_high_score, (bool success) =>
            {
                // handle success or failure
                callBackGooglePlay(success, "High Score LB");
            });

            Social.ReportScore(burger, GPGSIds.leaderboard_gathered_burger, (bool success) =>
            {
                // handle success or failure
                callBackGooglePlay(success, "Gather Burger LB");
            });

        }
    }
    #endregion LeaderBoard

    #region Save Games
    const string dateJsonKey = "UpdateDate";
    const string JsonKey = "GameSaves";

    const string SAVE_NAME = "GameSaves";
    const string SAVE_DATE = "GameSaveDate";

    private string LastUpdateDate;    

    private void StringToGameData(string localData)
    {   //yeni değerler oyuna işlendi. Bu işlem cloud cihazdan güncel ise yapılacak.        
        long date;
        Debug.Log(localData);
        if (localData != string.Empty)
        {
            Debug.Log("String is processing...");
            int[] CloudArray = JsonUtil.JsonStringToArray(localData, JsonKey, dateJsonKey, str => int.Parse(str), str => long.Parse(str), out date);
            CloudValues.instance.GetArrayFromCloud(CloudArray);
            PlayerPrefs.SetString(SAVE_DATE, date.ToString());            
        }
        else
        {
            Debug.Log("string is Empty");
            if (LoadingSceen.instance.gameObject.activeInHierarchy)
                LoadingSceen.instance.CloseLoadingScene();
        }           
    }
    private string GameDataToString() //oyun verilerini clouda atmak için dönüştürür
    {
        long date = DateTime.UtcNow.Ticks;
        PlayerPrefs.SetString(SAVE_DATE, date.ToString());
        CloudValues.instance.BuildJSONIntegerArray();
        return JsonUtil.CollectionToJsonString(CloudValues.JSONIntegerArray, JsonKey, dateJsonKey, date);
    }
    public void GetStringFromServer(bool saving) //serverdan string alınır, ResolveConflict'te ve OnSavedGameOpened'da işlenir.
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            isSaving = saving;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithManualConflictResolution(SAVE_NAME,
                DataSource.ReadCacheOrNetwork, true, ResolveConflict, OnSavedGameOpened);
        }
    }
    private void ResolveConflict(IConflictResolver resolver, ISavedGameMetadata original, byte[] originalData,
        ISavedGameMetadata unmerged, byte[] unmergedData)
    {
        Debug.Log("Step-0");
        if (originalData == null)
        {
            resolver.ChooseMetadata(unmerged);
            Debug.Log("unmerged-0");
        }
        else if (unmergedData == null)
        {
            resolver.ChooseMetadata(original);
            Debug.Log("original-0");
        }
        else
        {
            //decoding byte data into string
            Debug.Log("Step-1");
            string originalStr = Encoding.ASCII.GetString(originalData);
            Debug.Log("Step-2");
            string unmergedStr = Encoding.ASCII.GetString(unmergedData);

            string originalDateString;
            string unmergedDateString;
            long originalDate;
            long unmergedDate;
            //parsing

            Debug.Log("Step-3");
            try
            {
                int[] originalArray = JsonUtil.JsonStringToArray(originalStr, JsonKey, dateJsonKey, str => int.Parse(str), str => long.Parse(str), out originalDate);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e.Message);
                resolver.ChooseMetadata(unmerged);
                return;
            }

            Debug.Log("Step-4");
            try
            {
                int[] unmergedArray = JsonUtil.JsonStringToArray(unmergedStr, JsonKey, dateJsonKey, str => int.Parse(str), str => long.Parse(str), out unmergedDate);
            }
            catch(Exception e)
            {
                Debug.LogWarning(e.Message);
                return;
            }

            originalDateString = originalDate.ToString();
            unmergedDateString = unmergedDate.ToString();

            Debug.Log("Step-5");
            Debug.Log("originalDate: " + originalDateString);
            Debug.Log("unmergedDate: " + unmergedDateString);            
            
            //if original score is greater than unmerged
            if (originalDate > unmergedDate)
            {
                Debug.Log("original-2");
                resolver.ChooseMetadata(original);
                return;
            }
            //else (unmerged score is greater than original)
            else if (unmergedDate > originalDate)
            {
                Debug.Log("unmerged-2");
                resolver.ChooseMetadata(unmerged);
                return;
            }

            //if return doesn't get called, original and unmerged are identical
            //we can keep either one
            Debug.Log("Step-9");
            resolver.ChooseMetadata(original);
        }
    }

    private void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        //if we are connected to the internet
        if (status == SavedGameRequestStatus.Success)
        {
            //if we're LOADING game data
            if (!isSaving)
            {
                LoadGame(game);
                Debug.Log("Loading-1");
            }
            //if we're SAVING game data
            else
            {
                SaveGame(game);
                Debug.Log("Saving-1");
            } 
        }
        //if we couldn't successfully connect to the cloud, runs while on device,
        //telefondaki sürümün daha güncel olduğu bilgisini yazar ve internet geldiğinde günceller.
        else
        {
            Debug.Log("OnSavedGameOpened-Status: " + status.ToString());
            if (isSaving)
            {
                Debug.Log("OnSavedGameOpened: Local Saving");
                string date = DateTime.UtcNow.Ticks.ToString();
                PlayerPrefs.SetString(SAVE_DATE, date);
                Debug.Log(status.ToString() + " " + game.ToString());
            }
        }
    }
    private void LoadGame(ISavedGameMetadata game) //Serverdan okur OnSavedGameDataRead'i callback olarak çağırır
    {
        ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(game, OnSavedGameDataRead);
        Debug.Log("Loading..");
    }

    private void SaveGame(ISavedGameMetadata game) //Servera yazar onSAvedGameDAtaWritten'i callback olarak çağırır
    {
        string stringToSave = GameDataToString();
        //saving also locally (can also call SaveLocal() instead)       

        //encoding to byte array
        byte[] dataToSave = Encoding.ASCII.GetBytes(stringToSave);
        //updating metadata with new description
        SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder().Build();
        //uploading data to the cloud
        ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(game, update, dataToSave,
            OnSavedGameDataWritten);
        Debug.Log("Saving..");
    }

    //callback for ReadBinaryData //Serverdan okuma sonuçları işlenir.
    private void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] savedData)
    {
        //if reading of the data was successful
        if (status == SavedGameRequestStatus.Success)
        {
            string cloudDataString;
            //if we've never played the game before, savedData will have length of 0
            if (savedData.Length == 0)
                //in such case, we want to assign default value to our string
                cloudDataString = string.Empty;
            //otherwise take the byte[] of data and encode it to string
            else
                cloudDataString = Encoding.ASCII.GetString(savedData);
            
            //getting local data (if we've never played before on this device, localData is already
            //string.Empty, so there's no need for checking as with cloudDataString)
            string localDateString = PlayerPrefs.GetString(SAVE_DATE);
            Debug.Log("LocalDate " + localDateString);
            //this method will compare cloud and local data

            string cloudDateString;
            long cloudDate;

            if (!string.IsNullOrEmpty(cloudDataString))
            {
                int[] cloudarray = JsonUtil.JsonStringToArray(cloudDataString, JsonKey, dateJsonKey, 
                    str => int.Parse(str), str => long.Parse(str), out cloudDate);                
            }
            else
            {
                cloudDate = DateTime.MinValue.Ticks;
            }

            cloudDateString = cloudDate.ToString();
            
            Debug.Log("CloudDate " + cloudDateString);

            long localDate;
            if(!long.TryParse(localDateString, out localDate))
            {
                StringToGameData(cloudDataString);
                Debug.Log("local data loaded-1");                
                return;
            }            

            if(localDate < cloudDate)
            {
                //oyunu kayıt et, çünkü kayıt daha yeni
                StringToGameData(cloudDataString);
                Debug.Log("Cloud data loaded-2");                
            }
            else 
            {
                //kayıttan yükleme yap
                Debug.Log("local data not loaded-2");
                if(LoadingSceen.instance.gameObject.activeInHierarchy)
                    LoadingSceen.instance.CloseLoadingScene();
            }            
        }
    }

    //callback for CommitUpdate
    private void OnSavedGameDataWritten(SavedGameRequestStatus status, ISavedGameMetadata game) 
    {
        Debug.Log(status.ToString() + "-> save status");
    }

    //private void 


    #endregion

    private void callBackGooglePlay(bool success, string msg)
    {
        Debug.Log(msg + ": " + success);
    }
}
