using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Runner;

    public Vector3 offset;
    public Vector3 degree;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Runner.position + offset;
        transform.localEulerAngles = degree;
    }
}
