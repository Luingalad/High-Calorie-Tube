using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaController : MonoBehaviour
{
    public int Strength;
    public float FloatingSpeed;
    public float RotationSpeed;

    public Transform pizza;    
    void Update()
    {        
        float y = Mathf.Sin(Mathf.Deg2Rad * FloatingSpeed * Time.time)/6 + 1.5f;
        pizza.localPosition = new Vector3(0, y , 0);
        pizza.Rotate(new Vector3(0, 0, RotationSpeed * Time.deltaTime),Space.Self);
    }
}
