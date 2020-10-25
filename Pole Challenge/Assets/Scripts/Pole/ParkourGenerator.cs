using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourGenerator : MonoBehaviour
{
    public GameObject PolePrefab;
    public GameObject PoleParent;
    public GameObject NewRecord;
    
    public Transform Runner;
    public int PoleCount;    
    void Start()
    {
        Runner = GameObject.FindGameObjectWithTag("Player").transform;        

        if (ProfileManager.instance.Record > 5)
        {
            GameObject recordFlag = Instantiate(NewRecord);
            recordFlag.transform.localPosition = new Vector3(0, 0, ProfileManager.instance.Record + 1f);
        }
    }

    void Update()
    {
        if(Runner.position.z >= (10*(PoleCount-2)-5))
        {
            SpawnPole();
        }
    }
    private void SpawnPole()
    {
        GameObject g = Instantiate(PolePrefab, PoleParent.transform);
        g.transform.position = new Vector3(0, 0, 10 * (PoleCount + 1));        
        PoleCount++;
    }
}
