using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDrinkController : MonoBehaviour
{        
    public float FloatingSpeed;
    public float RotationSpeed;

    public Transform EnergyDrink;
    void Update()
    {
        float y = Mathf.Sin(Mathf.Deg2Rad * FloatingSpeed * Time.time) / 6 + 1.5f;
        EnergyDrink.localPosition = new Vector3(0, y, 0);
        EnergyDrink.Rotate(new Vector3(0, RotationSpeed * Time.deltaTime, 0), Space.World);
    }
}
