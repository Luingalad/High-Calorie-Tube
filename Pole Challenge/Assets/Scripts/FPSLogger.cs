using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSLogger : MonoBehaviour
{
    private float time = 1f;
    private float _time;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        if(time <= _time)
        {
            float frameRate = 1f / Time.deltaTime;
            Debug.Log(frameRate.ToString("0.0"));
            _time = 0;
        }
    }
}
