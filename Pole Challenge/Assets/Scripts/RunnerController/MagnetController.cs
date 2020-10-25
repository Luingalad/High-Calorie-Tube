using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetController : MonoBehaviour
{
    private bool isMagneticDonutUsed;
    private bool isMagneticDrinkUsed;

    public SpriteRenderer sprite;

    //public float minAlpha;
    public float maxAlpha;

    //public float minScale;
    public float maxScale;

    private float _scale;
    private float _alpha;
    private float t;

    public Color color;

    private void OnEnable()
    {
        isMagneticDonutUsed = false;
        isMagneticDrinkUsed = false;
        sprite.transform.localScale = new Vector3(maxScale, maxScale, 1);
        sprite.color = color;
        t = 0;
    }

    private void Update()
    {
        _scale = (maxScale) /2 + (maxScale) * Mathf.Cos(t) / 2f;
        _alpha = (maxAlpha) /2 + (maxAlpha) * Mathf.Cos(t) / 2f;

        sprite.color = new Color(color.r, color.g, color.b, _alpha);
        sprite.transform.localScale = new Vector3(_scale, _scale, 1);

        t += Time.deltaTime*3;

        if (_scale < 0.01f)
        {
            t = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {        
        if (other.CompareTag("Pizza") || other.CompareTag("Gold"))
        {
            other.gameObject.GetComponent<FlyToRunner>().SetFlyTarget(transform.parent);
        }
        else if(other.CompareTag("Donut") && !isMagneticDonutUsed && ProfileManager.instance.InGameSpecialty2 > 0)
        {
            other.gameObject.GetComponent<FlyToRunner>().SetFlyTarget(transform.parent);
            isMagneticDonutUsed = true;
        }
        else if (other.CompareTag("EnergyDrink") && !isMagneticDrinkUsed && ProfileManager.instance.InGameSpecialty3 > 0)
        {
            other.gameObject.GetComponent<FlyToRunner>().SetFlyTarget(transform.parent);
            isMagneticDrinkUsed = true;
        }
    }
}
