using System.Collections.Generic;
using UnityEngine;
public class GameController : MonoBehaviour
{
    public float SpeedMultiplier;
    public float DefaultSpeed;
    public int Score;

    public static GameController instance;
    public delegate void SpeedMultiplierChange();
    public SpeedMultiplierChange SpeedMultiplierChangeCallBack;

    public delegate void GameEnd();
    public SpeedMultiplierChange CallBackGameEnd;

    public bool _isGameControlActive;
    public bool _isGameEnd;
    public List<Material> materials;

    private GameObject Runner;

    public bool isRespawndUsed = false;
    public bool isSecondSecondChanceUsed = false;
    private bool isRewardTaken = false;

    public UIController uicontroller;

    public delegate void ToDestroyObjects();
    public ToDestroyObjects callBackToDestroyObjects;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;            
        }  
    }
    void Start()
    {
        Runner = GameObject.FindGameObjectWithTag("Player");        
    }
    void Update()
    {        
        Score = Mathf.FloorToInt(Runner.transform.position.z);
        Scores.instance.ScorePoint = Score;
        SetSpeedMultiplier();
    }
    void SetSpeedMultiplier()
    {        
        SpeedMultiplier = 1f + Score * (1f - BonusManager.instance.SRRate) / 400f;
        if (SpeedMultiplier >= 4f)
            SpeedMultiplier = 4f;
        SpeedMultiplierChangeCallBack.Invoke();
    }

    #region Reward
    public void RewardToContuinue()
    {
        uicontroller.OnRewardCallBack();
        callBackToDestroyObjects.Invoke();
        isRewardTaken = true;
    }
    public void RewardClosed()
    {
        if(isRespawndUsed)
        {
            uicontroller.OnClickCloseButton_EndScene();
        }
        else
        {
            if (isRewardTaken)
            {
                RunnerController runnerCont = Runner.GetComponent<RunnerController>();
                
                runnerCont.Respawn();
                isRewardTaken = false;

                if (!isSecondSecondChanceUsed && ProfileManager.instance.InGameSpecialty1 > 0)
                {
                    isSecondSecondChanceUsed = true;                    
                }
                else 
                {
                    isRespawndUsed = true;
                }
            }
            else
            {
                uicontroller.OnClickCloseButton_EndScene();
            }
        }
    }
    #endregion Reward
}
