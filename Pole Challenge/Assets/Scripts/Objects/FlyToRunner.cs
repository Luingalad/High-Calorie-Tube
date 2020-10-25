using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyToRunner : MonoBehaviour
{
    private Transform Runner;
    private bool isTracking = false;
    private float time;

    private float pos0;
    private float teta0;
    private float speed = 0.5f;
    private float rotationalSpeed = 20f;
    void Start()
    {
        
    }

    
    void Update()
    {
        if(isTracking)
        {
            time += Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, Runner.position.z), speed * time * GameController.instance.SpeedMultiplier);
            if(transform.eulerAngles.z < 180f)
            {
                transform.eulerAngles = Vector3.RotateTowards(transform.eulerAngles, Vector3.zero, rotationalSpeed * time * GameController.instance.SpeedMultiplier, rotationalSpeed * time * GameController.instance.SpeedMultiplier);
            } else
            {
                transform.eulerAngles = Vector3.RotateTowards(transform.eulerAngles, new Vector3(0,0,360f), rotationalSpeed * time * GameController.instance.SpeedMultiplier, rotationalSpeed * time * GameController.instance.SpeedMultiplier);
            }
            
        }
    }

    public void SetFlyTarget(Transform _runner)
    {
        Runner = _runner;
        isTracking = true;
        pos0 = transform.position.z;
        teta0 = transform.eulerAngles.z;
    }
}
