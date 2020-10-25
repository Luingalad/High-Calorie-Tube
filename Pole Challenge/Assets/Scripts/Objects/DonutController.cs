using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutController : MonoBehaviour
{
    public float FloatingSpeed;
    public float RotationSpeed;

    public Transform Donut;
    void Update()
    {
        float y = Mathf.Sin(Mathf.Deg2Rad * FloatingSpeed * Time.time) / 6 + 1.5f;
        Donut.localPosition = new Vector3(0, y, 0);
        Donut.Rotate(new Vector3(0, RotationSpeed * Time.deltaTime, 0), Space.World );
        
    }
}
