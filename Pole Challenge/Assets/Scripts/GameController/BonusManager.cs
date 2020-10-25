using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    #region singleton
    public static BonusManager instance;
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
    }
    #endregion    
    void Start()
    {
        //initialize
        //Speed Reduction
        SRLevel = PlayerPrefs.GetInt(SRLevelStr, 0);
        SRPrice = PlayerPrefs.GetInt(SRPriceStr, SRFirstPrice);
        SRRate = PlayerPrefs.GetFloat(SRRateStr, 0);
        //Protection Duration
        PDLevel = PlayerPrefs.GetInt(PDLevelStr, 0);
        PDPrice = PlayerPrefs.GetInt(PDPriceStr, PDFirstPrice);
        PDRate = PlayerPrefs.GetFloat(PDRateStr, 0);
        //Magnet Duration
        MDLevel = PlayerPrefs.GetInt(MDLevelStr, 0);
        MDPrice = PlayerPrefs.GetInt(MDPriceStr, MDFirstPrice);
        MDRate = PlayerPrefs.GetFloat(MDRateStr, 0);
        //Block Power
        BPLevel = PlayerPrefs.GetInt(BPLevelStr, 0);
        BPPrice = PlayerPrefs.GetInt(BPPriceStr, BPFirstPrice);
        BPRate = PlayerPrefs.GetFloat(BPRateStr, 0);
        //Start Magnet
        SMLevel = PlayerPrefs.GetInt(SMLevelStr, 0);
        SMPrice = PlayerPrefs.GetInt(SMPriceStr, SMFirstPrice);
        SMRate = PlayerPrefs.GetFloat(SMRateStr, 0);
        //Start Protection
        SPLevel = PlayerPrefs.GetInt(SPLevelStr, 0);
        SPPrice = PlayerPrefs.GetInt(SPPriceStr, SPFirstPrice);
        SPRate = PlayerPrefs.GetFloat(SPRateStr, 0);
        //Respawn Protection
        RPLevel = PlayerPrefs.GetInt(RPLevelStr, 0);
        RPPrice = PlayerPrefs.GetInt(RPPriceStr, RPFirstPrice);
        RPRate = PlayerPrefs.GetFloat(RPRateStr, defaultRespawnProtection);
        //Respawn Magnet
        RMLevel = PlayerPrefs.GetInt(RMLevelStr, 0);
        RMPrice = PlayerPrefs.GetInt(RMPriceStr, RMFirstPrice);
        RMRate = PlayerPrefs.GetFloat(RMRateStr, 0);
    }
    #region SpeedReduction
    //Speed Reduction = SR
    private readonly float SRPerLevel = 0.04f;
    private readonly int SRFirstPrice = 250;
    private readonly string SRLevelStr = "SRLevel";
    private readonly string SRRateStr = "SRRate";
    private readonly string SRPriceStr = "SRPrice";
    public int SRPrice { get; private set; }
    public float SRRate { get; private set; }
    public int SRLevel { get; private set; }
    public readonly int SRMaxLevel = 10;    

    public void SpeedReductionLevelUp()
    {
        SRLevel++;
        SRRate = SRPerLevel * SRLevel;
        SRPrice =(int) Mathf.Pow(2, SRLevel) * SRFirstPrice;

        PlayerPrefs.SetInt(SRLevelStr, SRLevel);
        PlayerPrefs.SetInt(SRPriceStr, SRPrice);
        PlayerPrefs.SetFloat(SRRateStr, SRRate);
        PlayerPrefs.Save();

        CloudValues.instance.BuildJSONIntegerArray();
    }

    public void SRCalculateWithLevel(int level)
    {
        SRLevel = level;
        SRRate = SRPerLevel * SRLevel;
        SRPrice = (int)Mathf.Pow(2, SRLevel) * SRFirstPrice;

        PlayerPrefs.SetInt(SRLevelStr, SRLevel);
        PlayerPrefs.SetInt(SRPriceStr, SRPrice);
        PlayerPrefs.SetFloat(SRRateStr, SRRate);
        PlayerPrefs.Save();
    }
    #endregion

    #region ProtectionDuration
    //Protection Duration = PD
    private readonly float PDPerLevel = 0.1f;
    private readonly int PDFirstPrice = 250;
    private readonly string PDLevelStr = "PDLevel";
    private readonly string PDRateStr = "PDRate";
    private readonly string PDPriceStr = "PDPrice";
    public int PDPrice { get; private set; }
    public float PDRate { get; private set; }
    public int PDLevel { get; private set; }
    public readonly int PDMaxLevel = 10;
    public void ProtectionDurationLevelUp()
    {
        PDLevel++;
        PDRate = PDPerLevel * PDLevel;
        PDPrice = (int)Mathf.Pow(2, PDLevel) * PDFirstPrice;

        PlayerPrefs.SetInt(PDLevelStr, PDLevel);
        PlayerPrefs.SetInt(PDPriceStr, PDPrice);
        PlayerPrefs.SetFloat(PDRateStr, PDRate);
        PlayerPrefs.Save();

        CloudValues.instance.BuildJSONIntegerArray();
    }

    public void PDCalculateWithLevel(int level)
    {
        PDLevel = level;
        PDRate = PDPerLevel * PDLevel;
        PDPrice = (int)Mathf.Pow(2, PDLevel) * PDFirstPrice;

        PlayerPrefs.SetInt(PDLevelStr, PDLevel);
        PlayerPrefs.SetInt(PDPriceStr, PDPrice);
        PlayerPrefs.SetFloat(PDRateStr, PDRate);
        PlayerPrefs.Save();
    }
    #endregion

    #region Magnetduration
    //Magnet Duration = MD
    private readonly float MDPerLevel = 0.1f;
    private readonly int MDFirstPrice = 250;
    private readonly string MDLevelStr = "MDLevel";
    private readonly string MDRateStr = "MDRate";
    private readonly string MDPriceStr = "MDPrice";
    public int MDPrice { get; private set; }
    public float MDRate { get; private set; }
    public int MDLevel { get; private set; }
    public readonly int MDMaxLevel = 10;
    public void MagnetDurationLevelUp()
    {
        MDLevel++;
        MDRate = MDPerLevel * MDLevel;
        MDPrice = (int)Mathf.Pow(2, MDLevel) * MDFirstPrice;

        PlayerPrefs.SetInt(MDLevelStr, MDLevel);
        PlayerPrefs.SetInt(MDPriceStr, MDPrice);
        PlayerPrefs.SetFloat(MDRateStr, MDRate);
        PlayerPrefs.Save();

        CloudValues.instance.BuildJSONIntegerArray();
    }
    //After SaveLoad
    public void MDCalculateWithLevel(int level)
    {
        MDLevel = level;
        MDRate = MDPerLevel * MDLevel;
        MDPrice = (int)Mathf.Pow(2, MDLevel) * MDFirstPrice;

        PlayerPrefs.SetInt(MDLevelStr, MDLevel);
        PlayerPrefs.SetInt(MDPriceStr, MDPrice);
        PlayerPrefs.SetFloat(MDRateStr, MDRate);
        PlayerPrefs.Save();
    }
    #endregion

    #region BlockPower
    //Block Power = BP
    private readonly float BPPerLevel = 0.04f;
    private readonly int BPFirstPrice = 250;
    private readonly string BPLevelStr = "BPLevel";
    private readonly string BPRateStr = "BPRate";
    private readonly string BPPriceStr = "BPPrice";
    public int BPPrice { get; private set; }
    public float BPRate { get; private set; }
    public int BPLevel { get; private set; }
    public readonly int BPMaxLevel = 10;
    public void BlockPowerReductionLevelUp()
    {
        BPLevel++;
        BPRate = BPPerLevel * BPLevel;
        BPPrice = (int)Mathf.Pow(2, BPLevel) * BPFirstPrice;

        PlayerPrefs.SetInt(BPLevelStr, BPLevel);
        PlayerPrefs.SetInt(BPPriceStr, BPPrice);
        PlayerPrefs.SetFloat(BPRateStr, BPRate);
        PlayerPrefs.Save();

        CloudValues.instance.BuildJSONIntegerArray();
    }

    public void BPCalculateWitLevel(int level)
    {
        BPLevel = level;
        BPRate = BPPerLevel * BPLevel;
        BPPrice = (int)Mathf.Pow(2, BPLevel) * BPFirstPrice;

        PlayerPrefs.SetInt(BPLevelStr, BPLevel);
        PlayerPrefs.SetInt(BPPriceStr, BPPrice);
        PlayerPrefs.SetFloat(BPRateStr, BPRate);
        PlayerPrefs.Save();
    }
    #endregion

    #region StartMagnet
    //StartMagnet = SM
    private readonly float SMPerLevel = 2f;
    private readonly int SMFirstPrice = 100;
    private readonly string SMLevelStr = "SMLevel";
    private readonly string SMRateStr = "SMRate";
    private readonly string SMPriceStr = "SMPrice";
    public int SMPrice { get; private set; }
    public float SMRate { get; private set; }
    public int SMLevel { get; private set; }
    public readonly int SMMaxLevel = 10;
    public void StartMagnetLevelUp()
    {
        SMLevel++;
        SMRate = SMPerLevel * SMLevel;
        SMPrice = (int)Mathf.Pow(2, SMLevel) * SMFirstPrice;

        PlayerPrefs.SetInt(SMLevelStr, SMLevel);
        PlayerPrefs.SetInt(SMPriceStr, SMPrice);
        PlayerPrefs.SetFloat(SMRateStr, SMRate);
        PlayerPrefs.Save();

        CloudValues.instance.BuildJSONIntegerArray();
    }

    public void SMCalculateWitLevel(int level)
    {
        SMLevel = level;
        SMRate = SMPerLevel * SMLevel;
        SMPrice = (int)Mathf.Pow(2, SMLevel) * SMFirstPrice;

        PlayerPrefs.SetInt(SMLevelStr, SMLevel);
        PlayerPrefs.SetInt(SMPriceStr, SMPrice);
        PlayerPrefs.SetFloat(SMRateStr, SMRate);
        PlayerPrefs.Save();
    }
    #endregion

    #region StartProtection
    //StartProtection = SP
    private readonly float SPPerLevel = 2f;
    private readonly int SPFirstPrice = 100;
    private readonly string SPLevelStr = "SPLevel";
    private readonly string SPRateStr = "SPRate";
    private readonly string SPPriceStr = "SPPrice";
    public int SPPrice { get; private set; }
    public float SPRate { get; private set; }
    public int SPLevel { get; private set; }
    public readonly int SPMaxLevel = 10;
    public void StartProtectionLevelUp()
    {
        SPLevel++;
        SPRate = SPPerLevel * SPLevel;
        SPPrice = (int)Mathf.Pow(2, SPLevel) * SPFirstPrice;

        PlayerPrefs.SetInt(SPLevelStr, SPLevel);
        PlayerPrefs.SetInt(SPPriceStr, SPPrice);
        PlayerPrefs.SetFloat(SPRateStr, SPRate);
        PlayerPrefs.Save();

        CloudValues.instance.BuildJSONIntegerArray();
    }

    public void SPCalculateWitLevel(int level)
    {
        SPLevel = level;
        SPRate = SPPerLevel * SPLevel;
        SPPrice = (int)Mathf.Pow(2, SPLevel) * SPFirstPrice;

        PlayerPrefs.SetInt(SPLevelStr, SPLevel);
        PlayerPrefs.SetInt(SPPriceStr, SPPrice);
        PlayerPrefs.SetFloat(SPRateStr, SPRate);
        PlayerPrefs.Save();
    }
    #endregion

    #region RespawnProtection
    //RespawnProtection = RP
    private readonly int defaultRespawnProtection = 5;
    private readonly float RPPerLevel = 2f;
    private int RPFirstPrice = 100;
    private readonly string RPLevelStr = "RPLevel";
    private readonly string RPRateStr = "RPRate";
    private readonly string RPPriceStr = "RPPrice";
    public int RPPrice { get; private set; }
    public float RPRate { get; private set; }
    public int RPLevel { get; private set; }
    public readonly int RPMaxLevel = 10;
    public void RespawnProtectionLevelUp()
    {
        RPLevel++;
        RPRate = RPPerLevel * RPLevel + defaultRespawnProtection;
        RPPrice = (int)Mathf.Pow(2, RPLevel) * RPFirstPrice;

        PlayerPrefs.SetInt(RPLevelStr, RPLevel);
        PlayerPrefs.SetInt(RPPriceStr, RPPrice);
        PlayerPrefs.SetFloat(RPRateStr, RPRate);
        PlayerPrefs.Save();

        CloudValues.instance.BuildJSONIntegerArray();
    }

    public void RPCalculateWitLevel(int level)
    {
        RPLevel = level;
        RPRate = RPPerLevel * RPLevel + defaultRespawnProtection;
        RPPrice = (int)Mathf.Pow(2, RPLevel) * RPFirstPrice;

        PlayerPrefs.SetInt(RPLevelStr, RPLevel);
        PlayerPrefs.SetInt(RPPriceStr, RPPrice);
        PlayerPrefs.SetFloat(RPRateStr, RPRate);
        PlayerPrefs.Save();
    }
    #endregion

    #region RespawnMagnet
    //RespawnProtection = RM
    private readonly float RMPerLevel = 2f;
    private readonly int RMFirstPrice = 100;
    private readonly string RMLevelStr = "RMLevel";
    private readonly string RMRateStr = "RMRate";
    private readonly string RMPriceStr = "RMPrice";
    public int RMPrice { get; private set; }
    public float RMRate { get; private set; }
    public int RMLevel { get; private set; }
    public readonly int RMMaxLevel = 10;
    public void RespawnMagnetLevelUp()
    {
        RMLevel++;
        RMRate = RMPerLevel * RMLevel;
        RMPrice = (int)Mathf.Pow(2, RMLevel) * RMFirstPrice;

        PlayerPrefs.SetInt(RMLevelStr, RMLevel);
        PlayerPrefs.SetInt(RMPriceStr, RMPrice);
        PlayerPrefs.SetFloat(RMRateStr, RMRate);
        PlayerPrefs.Save();

        CloudValues.instance.BuildJSONIntegerArray();
    }

    public void RMCalculateWitLevel(int level)
    {
        RMLevel = level;
        RMRate = RMPerLevel * RMLevel;
        RMPrice = (int)Mathf.Pow(2, RMLevel) * RMFirstPrice;

        PlayerPrefs.SetInt(RMLevelStr, RMLevel);
        PlayerPrefs.SetInt(RMPriceStr, RMPrice);
        PlayerPrefs.SetFloat(RMRateStr, RMRate);
        PlayerPrefs.Save();
    }
    #endregion

    public string GetValues(string code)
    {
        switch(code)
        {
            case "SR":
                {
                    return SRPrice + ":" + SRRate + ":" + SRLevel + ":"+ SRMaxLevel;                    
                }
            case "PD":
                {
                    return PDPrice + ":" + PDRate + ":" + PDLevel + ":" + PDMaxLevel;
                }
            case "MD":
                {
                    return MDPrice + ":" + MDRate + ":" + MDLevel + ":" + MDMaxLevel; ;
                }
            case "BP":
                {
                    return BPPrice + ":" + BPRate + ":" + BPLevel + ":" + BPMaxLevel; ;
                }
            case "SM":
                {
                    return SMPrice + ":" + SMRate + ":" + SMLevel + ":" + SMMaxLevel; ;
                }
            case "SP":
                {

                    return SPPrice + ":" + SPRate + ":" + SPLevel + ":" + SPMaxLevel;
                }
            case "RP":
                {

                    return RPPrice + ":" + RPRate + ":" + RPLevel + ":" + RPMaxLevel;
                }
            case "RM":
                {

                    return RMPrice + ":" + RMRate + ":" + RMLevel + ":" + RMMaxLevel; ;
                }
            default:
                return "0:0";
        }
    }
}