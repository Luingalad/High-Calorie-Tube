using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTheImage : MonoBehaviour
{
    private float turnSpeed = 15f;
    private float t = 0;
        
    void Update()
    {
        if (t >= 24f)
        {
            t = 0;
        }

        t += Time.deltaTime;
        transform.localEulerAngles = new Vector3(0, 0, t* turnSpeed);       

    }
}
