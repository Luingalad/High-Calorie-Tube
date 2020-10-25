using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    public GameObject BlockPrefab;
    public GameObject BlockPrefabWithSteak;
    public List<GameObject> blocks;

    public float RotationSpeed;
    public int direction;
    
    void Start()
    {
        blocks = new List<GameObject>();
        SpawnObject();

        direction = (int) Mathf.Sign( Random.Range(-1f, 1f));
    }

    private void Update()
    {
        if( (GameController.instance.Score) > 1000f )
        {
            transform.Rotate(Vector3.forward * RotationSpeed * direction * Time.deltaTime, Space.Self);
        }
    }
    private void OnEnable()
    {
        GameController.instance.callBackToDestroyObjects += DestroySelf;
    }

    private void OnDestroy()
    {
        GameController.instance.callBackToDestroyObjects -= DestroySelf;
    }
    void SpawnObject()
    {
        StartCoroutine(BlockSpawnerEnum());
    }

    public void CloseColliders()
    {
        foreach(GameObject g in blocks)
        {
            g.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    private IEnumerator BlockSpawnerEnum()
    {
        int blockCount = Random.Range(4, 8);

        for (int i = 0; i < blockCount; i++)
        {
            if (GameController.instance.Score > 400)
            {
                int chance = Random.Range(0, 600);

                if (chance < 12)
                {
                    GameObject g = Instantiate(BlockPrefab, transform);
                    g.transform.localEulerAngles = new Vector3(0, 0, i * 45);
                    BlockController bl = g.GetComponent<BlockController>();
                    bl.specify = 1;
                    int score = GameController.instance.Score;

                    int max = Mathf.RoundToInt(Mathf.Sqrt(score) / 0.6f + 11) * 2;                    
                    bl.BlockStrenght = (int) (max* (1f - BonusManager.instance.BPRate));

                    bl.bg = this;
                    blocks.Add(g);
                    bl.onCrash();
                }
                else if (chance < 24)
                {
                    GameObject g = Instantiate(BlockPrefab, transform);
                    g.transform.localEulerAngles = new Vector3(0, 0, i * 45);
                    BlockController bl = g.GetComponent<BlockController>();
                    bl.specify = 2;
                    int score = GameController.instance.Score;

                    int min = Mathf.RoundToInt(Mathf.Sqrt(score) / 0.6f + 1) / 2;
                    bl.BlockStrenght = (int)(min * (1f - BonusManager.instance.BPRate));

                    bl.bg = this;
                    blocks.Add(g);
                    bl.onCrash();
                }
                else if (chance > 597)
                {
                    GameObject g = Instantiate(BlockPrefabWithSteak, transform);
                    g.transform.localEulerAngles = new Vector3(0, 0, i * 45);
                    SteakBlockController bl = g.GetComponent<SteakBlockController>();
                    
                    bl.bg = this;
                    blocks.Add(g);
                }
                else
                {
                    GameObject g = Instantiate(BlockPrefab, transform);
                    g.transform.localEulerAngles = new Vector3(0, 0, i * 45);
                    BlockController bl = g.GetComponent<BlockController>();
                    int score = GameController.instance.Score;
                    int min = Mathf.RoundToInt(Mathf.Sqrt(score) / 0.6f + 1);
                    int max = Mathf.RoundToInt(Mathf.Sqrt(score) / 0.6f + 11);
                    bl.BlockStrenght = (int)(Random.Range(min, max) * (1f - BonusManager.instance.BPRate));
                    bl.bg = this;
                    blocks.Add(g);
                    bl.onCrash();
                }
            }
            else
            {
                GameObject g = Instantiate(BlockPrefab, transform);
                g.transform.localEulerAngles = new Vector3(0, 0, i * 45);
                BlockController bl = g.GetComponent<BlockController>();
                int score = GameController.instance.Score;
                int min = Mathf.RoundToInt(Mathf.Sqrt(score) / 0.6f + 1);
                int max = Mathf.RoundToInt(Mathf.Sqrt(score) / 0.6f + 11);
                bl.BlockStrenght = (int)(Random.Range(min, max) * (1f - BonusManager.instance.BPRate));
                bl.bg = this;
                blocks.Add(g);
                bl.onCrash();
            }


            yield return null;
        }
        transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
    }
}
