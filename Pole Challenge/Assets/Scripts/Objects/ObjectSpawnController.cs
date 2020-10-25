using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnController : MonoBehaviour
{
    public GameObject PizzaPrefab;
    public GameObject GoldPrefab;
    public GameObject EnergyDrinkPrefab;
    public GameObject DonutPrefab;
    [Range(0,1)]
    public float PizzaChance;
    [Range(0,1)]
    public float GoldChance;
    [Range(0, 1)]
    public float EnergyDrinkChance;
    [Range(0, 1)]
    public float DonutChance;

    private float chanceMultiplier = 1f;

    void Start()
    {
        if(GameController.instance.Score > 4000)
        {
            chanceMultiplier = 0.25f;
        }else if(GameController.instance.Score > 2000)
        {
            chanceMultiplier = 0.5f;
        }

        SpawnPizza();
        SpawnGold();
        SpawnEnegyDrink();
        SpawnDonut();
    }

    private void OnEnable()
    {
        GameController.instance.callBackToDestroyObjects += DestroySelf;
    }

    private void OnDestroy()
    {
        GameController.instance.callBackToDestroyObjects -= DestroySelf;
    }

    private void SpawnPizza()
    {
        float ch = Random.Range(0f, 1f);
        if(ch <= PizzaChance*chanceMultiplier)
        {
            //Spawn
            GameObject g = Instantiate(PizzaPrefab, transform);
            float posZ = Random.Range(-4f, +4f);
            if(Mathf.Abs(posZ) < 1f)
            {
                posZ = Mathf.Sign(posZ) + 0.2f;
            }
            g.transform.localPosition = new Vector3(0, 0, posZ);
            g.transform.eulerAngles = new Vector3(0, 0, Random.Range(0,360));

            PizzaController p = g.GetComponent<PizzaController>();
            p.Strength += Mathf.FloorToInt(Mathf.Sqrt(GameController.instance.Score)/2);
        }
    }

    private void SpawnGold()
    {
        float ch = Random.Range(0f, 1f);

        if(ch <= GoldChance * chanceMultiplier)
        {
            //Spawn
            StartCoroutine(GoldSpawnerEnum());
        }
    }

    private void SpawnEnegyDrink()
    {
        float ch = Random.Range(0f, 1f);

        if (ch <= EnergyDrinkChance * chanceMultiplier)
        {
            //Spawn
            GameObject g = Instantiate(EnergyDrinkPrefab, transform);
            float posZ = Random.Range(-5f, +5f);
            if (Mathf.Abs(posZ) < 1f)
            {
                posZ = Mathf.Sign(posZ) + 0.5f;
            }
            g.transform.localPosition = new Vector3(0, 0, posZ);
            g.transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));

        }
    }

    private void SpawnDonut()
    {
        float ch = Random.Range(0f, 1f);

        if (ch <= DonutChance * chanceMultiplier)
        {
            //Spawn
            GameObject g = Instantiate(DonutPrefab, transform);
            float posZ = Random.Range(-5f, +5f);
            if (Mathf.Abs(posZ) < 1f)
            {
                posZ = Mathf.Sign(posZ) + 0.5f;
            }
            g.transform.localPosition = new Vector3(0, 0, posZ);
            g.transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));

        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    IEnumerator GoldSpawnerEnum()
    {
        int count = Random.Range(2, 8);
        float angle = Random.Range(0, 8) * 45;

        for (int i = 0; i < count; i++)
        {
            GameObject g = Instantiate(GoldPrefab, transform);
            g.transform.localPosition = new Vector3(0, 0, 2 + i);
            angle += Random.Range(-1, 2) * 22.5f;
            g.transform.eulerAngles = new Vector3(0, 0, angle);

            GoldController gc = g.GetComponent<GoldController>();
            gc.Gold = 1;
            yield return null;
        }
    }
}
