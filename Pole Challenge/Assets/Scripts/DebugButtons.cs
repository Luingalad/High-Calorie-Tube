using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugButtons : MonoBehaviour
{
    public Button AddSteak;
    public Button AddBurger;
    void Start()
    {
        AddSteak.onClick.AddListener(OnClikAddSteak);
        AddBurger.onClick.AddListener(OnClickAddBurger);
    }
    
    private void OnClikAddSteak()
    {
        ProfileManager.instance.callBackOnSteakChanged(1000);
    }

    private void OnClickAddBurger()
    {
        ProfileManager.instance.callBackOnBurgerChanged(1000);
    }
}
