using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerController : MonoBehaviour
{
    public Rigidbody runnerBody;
    private float defaultSpeed;
    private float speedMultiplier;
    public bool _DeadOrAlive = false;

    private GameController controller;
    public Animator animator;

    public int Strength;
    public RunnerPowers powers;

    private float _previousBlockPosZ;

    public HitUIController hitUI;

    public RunnerSounds sounds;
    

    [Header(header: "Protection")]
    public Material ProtectionMaterial;
    public Color BaseColorNormal;
    [ColorUsage(true, true)]
    public Color EmissionColorNormal;
    public Color BaseColorGA;
    [ColorUsage(true, true)]
    public Color EmissionColorGA;
    private bool isGaUsed = false;

    public GameObject Trail;

    // Start is called before the first frame update
    void Start()
    {  
        controller = GameController.instance;
        animator = GetComponent<Animator>();
        runnerBody = GetComponent<Rigidbody>();
        
        controller.SpeedMultiplierChangeCallBack += SpeedChanged;
        controller.SpeedMultiplierChangeCallBack.Invoke();
        //SpeedChanged();
    }

    // Update is called once per frame
    void Update()
    {
        if (_DeadOrAlive)
            return;
        runnerBody.velocity = new Vector3(0, 0, defaultSpeed * speedMultiplier);        
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            if (_previousBlockPosZ < other.transform.position.z)
            {
                BlockController block = other.gameObject.GetComponent<BlockController>();
                int _strength;
                //Protection..
                if (powers.isProtectionAvailable)
                {
                    _strength = Strength;
                }
                else
                {
                    _strength = Strength - block.BlockStrenght;
                }                

                if (_strength < 0)
                {
                    float rate = Random.Range(0f, 1f);

                    if(rate < 0.5f && ProfileManager.instance.InGameSpecialty5 > 0 && !isGaUsed)
                    {
                        block.BlockStrenght = 0;
                        StartCoroutine(ChangeMaterial());
                        isGaUsed = true;
                        powers.GaOn();
                    }
                    else
                    {
                        block.BlockStrenght = Mathf.Abs(_strength);
                        Strength = 0;
                        Die();
                    }
                }
                else
                {
                    float rate = Random.Range(0f,1f);

                    if(rate < 0.1f && ProfileManager.instance.InGameSpecialty4 > 0)
                    {
                        //activate the double burger
                        powers.DoubleOn();
                    }

                    Strength = _strength;
                    block.BlockStrenght = 0;
                }
                block.onCrash();
                _previousBlockPosZ = other.transform.position.z;
                hitUI.Hit(powers.isProtectionAvailable);
                sounds.HitBlock();               
            }
        }
        else if(other.CompareTag("BlockWithSteak"))
        {
            SteakBlockController c = other.GetComponent<SteakBlockController>();
            ProfileManager.instance.callBackOnSteakChanged(1);
            hitUI.HitSteak();
            c.onCrash();
        }
        else if(other.CompareTag("Pizza"))
        {            
            PizzaController p = other.gameObject.GetComponent<PizzaController>();
            Strength += p.Strength;
            Destroy(p.gameObject);
            sounds.TakePizza();
            Scores.instance.PizzaCount++;            
        }
        else if(other.CompareTag("Gold"))
        {
            GoldController gold = other.gameObject.GetComponent<GoldController>(); 

            int multiplier = 1;

            if (powers.isDoubleBurgerAvailable)
                multiplier = 2;

            Scores.instance.BurgerCount += gold.Gold * multiplier;
            Strength += (gold.strenght * multiplier);

            sounds.TakeBurger();
            Destroy(other.gameObject);
        } else if(other.CompareTag("EnergyDrink"))
        {            
            powers.ProtectionOn();
            Destroy(other.gameObject);
            sounds.TakeProtection();
        } else if(other.CompareTag("Donut"))
        {
            powers.MagnetnOn();
            Destroy(other.gameObject);
            sounds.TakeMagnet();
        } else if(other.CompareTag("Record"))
        {
            other.enabled = false;
            FindObjectOfType<UIController>().callBackNewRecord.Invoke();
            sounds.Record();
        }
    }
    public void SpeedChanged()
    {
        defaultSpeed = controller.DefaultSpeed;
        speedMultiplier = controller.SpeedMultiplier;
        animator.SetFloat("Speed", speedMultiplier);
        if(speedMultiplier > 2f && !Trail.activeSelf)
        {
            Trail.SetActive(true);
        }
        else if(speedMultiplier < 2f && Trail.activeSelf)
        {
            Trail.SetActive(false);
        }
    }
    public void Die()
    {
        print("Runner dead");
        runnerBody.velocity = Vector3.zero;
        animator.SetBool("isDead", true);
        _DeadOrAlive = true;
        controller._isGameControlActive = false;
        controller._isGameEnd = true;
        sounds.Dead();
        controller.CallBackGameEnd.Invoke();

    }
    public void Respawn()
    {
        sounds.Respawn();
        print("Runner respawn");        
        animator.SetBool("isDead", false);        
        Strength = Mathf.RoundToInt(Mathf.Sqrt(GameController.instance.Score) / 0.2f + 11);
        StartCoroutine(RespawnTime());
        StartCoroutine(FindObjectOfType<UIController>().CountDownNu());
    }

    private IEnumerator RespawnTime()
    {
        //sounds.TimerToStart();
        yield return new WaitForSecondsRealtime(3f);
        //sounds.TimerStop();
        _DeadOrAlive = false;
        controller._isGameControlActive = true;
        controller._isGameEnd = false;
        yield return new WaitForSecondsRealtime(9f / controller.SpeedMultiplier);
        powers.ProtectionOn(BonusManager.instance.RPRate);
        if (BonusManager.instance.RMLevel > 0)
            powers.MagnetnOn(BonusManager.instance.RMRate);
        
    }

    private IEnumerator ChangeMaterial()
    {
        ProtectionMaterial.color = EmissionColorNormal;
        ProtectionMaterial.SetColor("_EmissionColor", EmissionColorGA);        

        powers.ProtectionOn(5f);

        float Count = 100;

        float nColorR = (BaseColorNormal.r - BaseColorGA.r) / Count;
        float nColorG = (BaseColorNormal.g - BaseColorGA.g) / Count;
        float nColorB = (BaseColorNormal.b - BaseColorGA.b) / Count;

        float hColorR = (EmissionColorNormal.r - EmissionColorGA.r) / Count;
        float hColorG = (EmissionColorNormal.g - EmissionColorGA.g) / Count;
        float hColorB = (EmissionColorNormal.b - EmissionColorGA.b) / Count;

        for (int i = 0; i <= Count; i++)
        {
            Color normalColor = new Color(BaseColorGA.r + nColorR*i, BaseColorGA.g + nColorG * i, 
                BaseColorGA.b + nColorB * i, BaseColorNormal.a);

            Color hdrColor = new Color(EmissionColorGA.r + hColorR * i, EmissionColorGA.g + hColorG * i, 
                EmissionColorGA.b + hColorB * i);

            ProtectionMaterial.color = normalColor;
            ProtectionMaterial.SetColor("_EmissionColor", hdrColor);
            
            yield return null;
        }
        //ProtectionMaterial.color = BaseColorNormal;
        //ProtectionMaterial.SetColor("_EMISSION", EmissionColorNormal);
    }
}
