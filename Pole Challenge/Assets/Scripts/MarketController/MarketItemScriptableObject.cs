using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Market Item", menuName = "Market/New Market Item")]
public class MarketItemScriptableObject : ScriptableObject
{    
    public string ItemName;
    public string ItemDescription;

    public string code;

    public bool isPercentage;
}
