using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleDestroyer : MonoBehaviour
{
    private Transform runner;
    void Start()
    {
        runner = GameObject.FindGameObjectWithTag("Player").transform;
    }    
    void Update()
    {
        if((runner.position.z - transform.position.z) > 10f)
        {
            Destroy(gameObject);
        }
    }
}
