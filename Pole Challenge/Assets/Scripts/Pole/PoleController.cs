using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleController : MonoBehaviour
{
    public float RotateSpeed;    
    
    void Update()
    {
        if(GameController.instance._isGameControlActive)
        {
            float z = 0;
#if UNITY_EDITOR

            z = RotateSpeed * Input.GetAxis("Horizontal") * GameController.instance.SpeedMultiplier;

#elif !UNITY_EDITOR
            if(Input.touchCount > 0)
                z = -RotateSpeed * Input.GetTouch(0).deltaPosition.x / 10;

#endif
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y,
               transform.eulerAngles.z + z);
        }
           
    }
   
}
