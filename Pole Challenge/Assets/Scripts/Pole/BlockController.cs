using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlockController : MonoBehaviour
{
    //max 100 min 1
    public int BlockStrenght;
    public TMP_Text point;

    public BlockGenerator bg;

    public int specify= 0;

    void Start()
    {        
        int i = BlockStrenght % 10;
        GetComponent<MeshRenderer>().material = GetMaterial(i);
        point.text = BlockStrenght.ToString("D2");  
        if(specify == 2)
        {
            point.color = new Color(0,0.75f,0.75f,1);
        }
    }   

    public Material GetMaterial(int index)
    {
        if(specify == 1)
        {
            return GameController.instance.materials[10];
        } else if(specify == 2)
        {
            return GameController.instance.materials[11];
        }        
        return GameController.instance.materials[index];        
    }

    public void onCrash()
    {
        if(BlockStrenght <= 0)
        {
            gameObject.SetActive(false);
            bg.CloseColliders();
            return;
        }
        point.text = BlockStrenght.ToString("D2");
    }

    
}
