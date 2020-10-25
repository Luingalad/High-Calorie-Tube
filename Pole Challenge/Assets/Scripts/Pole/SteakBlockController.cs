using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteakBlockController : MonoBehaviour
{   
    public BlockGenerator bg;
    
    public void onCrash()
    {        
        gameObject.SetActive(false);
        bg.CloseColliders();
        return;      
    }
}
