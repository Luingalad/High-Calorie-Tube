using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeController : MonoBehaviour
{
    public GameObject TubePrefab;

    private GameObject runner;

    private int TubeCount = 10;
    void Start()
    {
        runner = GameObject.FindGameObjectWithTag("Player");      
    }
    
    void Update()
    {
        if(runner.transform.position.z >= (10 * (TubeCount - 10)))
        {            
            SpawnTube();
        }
    }

    private void SpawnTube()
    {
        GameObject t = Instantiate(TubePrefab, transform);
        t.transform.localPosition = Vector3.forward * 10 * TubeCount;
        TubeCount++;
    }
}
