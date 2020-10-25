using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldController : MonoBehaviour
{
    public int strenght = 1;
    public int Gold;
    public float FloatingSpeed;
    public float RotationSpeed;

    public Transform gold;
    void Update()
    {
        float y = Mathf.Sin(Mathf.Deg2Rad * FloatingSpeed * Time.time) / 6 + 1.5f;
        gold.localPosition = new Vector3(0, y, 0);
        gold.Rotate(new Vector3(0, RotationSpeed * Time.deltaTime, 0));
    }
}
