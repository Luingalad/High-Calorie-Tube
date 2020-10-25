using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scores : MonoBehaviour
{   
    public int BurgerCount;
    public int ScorePoint;
    public int PizzaCount;

    public static Scores instance;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;        
    }    
}
