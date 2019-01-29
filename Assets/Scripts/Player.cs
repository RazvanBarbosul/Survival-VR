using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {



    private int health;
    private int maxHealth=100;
    private int powerMax = 100;
    private int currentPower;
    private int powerCost = 1;
    private bool powerRegen = false;
    private int damage;

    public int level=1;
    public int[] LevelUp;

    private int xp;
    private int maxXP;

    public Text LevelUpText;
    public Text CurrentLevelText;

    public Image powerImage;
    public Text PowerPercentage;

    public Image healthImage;
    public Text healthPercentage;

    public Text gameOverText;

    public GameObject shootPoint;

    private GameObject fireSpell;
    private GameObject iceSpell;
    private GameObject lightingSpell;
    private GameObject waterSpell;

    [SerializeField]
    private Light Spotligt;

    public float range = 0.1f;
    Ray ray;

    // Use this for initialization
    void Start() {
      //  cam = Camera.main;
        currentPower = powerMax;
        health = maxHealth;
        CurrentLevelText.text = "Current Level: " + level.ToString();
        
        PowerCost();
        LoadAbilites();
    }

    // Update is called once per frame
    void Update () {
        PowerHandle();
        HandleHP();
        AbilitesUses();
        LevelUP();
    }

    public void LoadAbilites()
    {
        fireSpell = Resources.Load("Prefabs/FireSpell1") as GameObject;

        iceSpell = Resources.Load("Prefabs/IceSpell1") as GameObject;

        lightingSpell = Resources.Load("Prefabs/LightingSpell1") as GameObject;

        waterSpell = Resources.Load("Prefabs/WaterSpell1") as GameObject;

    }

    public void FireAbility()
    {
        // cost = powerCost;
       
        
          currentPower -= powerCost;

        Instantiate(fireSpell, shootPoint.transform.position, shootPoint.transform.rotation);
        Debug.Log("Fire Ability used");
    }
    public void IceAbility()
    {
        // cost = powerCost;
        
        currentPower -= powerCost;

        Instantiate(iceSpell, shootPoint.transform.position, shootPoint.transform.rotation);
        Debug.Log("Ice ability used");
    }
    public void LightingAbility()
    {
        // cost = powerCost;
       
        currentPower -= powerCost;

        Instantiate(lightingSpell, shootPoint.transform.position, shootPoint.transform.rotation);
        Debug.Log("Lighting ability used");
    }
    public void WaterAbility()
    {
        // cost = powerCost;
        
        currentPower -= powerCost;

        Instantiate(waterSpell, shootPoint.transform.position, shootPoint.transform.rotation);
        Debug.Log("Unknown ability used");
    }

    public void ShootDirection()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range))
        {
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(hit.normal * 30);
            }
        }
    }

    public void AbilitesUses()
    {

        if (currentPower >powerCost)
        {
            ShootDirection();

            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetButtonDown("Fire1"))
            {
                ShootDirection();
                FireAbility();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetButtonDown("Fire2"))
            {
                ShootDirection();
                IceAbility();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetButtonDown("Fire3"))
            {
                ShootDirection();
                LightingAbility();
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetButtonDown("Fire4"))
            {
                ShootDirection();
                WaterAbility();
            }
            //if (Input.GetButtonDown("Fire1"))
            //{
            //    ShootDirection();
            //    FireAbility();
            //}
            //if (Input.GetButtonDown("Fire2"))
            //{
            //    ShootDirection();
            //    IceAbility();
            //}
            //if (Input.GetButtonDown("Fire3"))
            //{
            //    ShootDirection();
            //    LightingAbility();
            //}
            //if (Input.GetButtonDown("Fire4"))
            //{
            //    ShootDirection();
            //    WaterAbility();
            //}
        }
       
    }

    public void PowerCost()
    {
        powerCost = level * 3;
        currentPower = powerMax;             
    }

    private void PowerHandle()
    {
        if (currentPower >= powerMax)
        {
            currentPower = powerMax;
        }
        if (powerRegen == false)
        {
            powerRegen = true;
            StartCoroutine(PowerRegen());
        }
        if (currentPower <= 0)
        {
            currentPower = 0;
        }
        powerImage.fillAmount = (float)currentPower / (float)powerMax;       
        PowerPercentage.text = currentPower.ToString();
    }

    public void HandleHP()
    {
        healthImage.fillAmount = (float)health / (float)maxHealth;
        healthPercentage.text = health.ToString();

        if (health <= 2)
        {
            Spotligt.color = Color.red;
        }
        else
        {
            Spotligt.color = Color.white;
        }

        if (health <= 0)
        {
            StartCoroutine(GameOver());
        }
    }

    public void LevelUP()
    {
        maxXP = LevelUp[level];
        if (xp >= maxXP)
        {
            if (level == 0)
            {
                level+=1;
                Debug.Log("Level UP");
            }
            else if (level >= 1)
            {
                xp = 0;
                maxXP += 50;
                level += 1;
                health += 1;
                maxHealth += 1;
                powerMax += 20;
                damage += 5;
                PowerCost();
                StartCoroutine(ShowLevelUp());
            }
           
            CurrentLevelText.text = "Current Level: " + level.ToString();
        }
    }

    public void GetDamage(int dmg)
    {
        health -= dmg;
    }

    public void DoDamage(int dmg)
    {
        damage = level * 3;
        damage -= dmg;
    }

    public void AddXP(int XPtoAdd)
    {
        xp += XPtoAdd;
        Debug.Log("ACTUAL XP: " + xp);
    }

 

    IEnumerator PowerRegen()
    {
        currentPower += 2;
        yield return new WaitForSeconds(1);
        powerRegen = false;
    }

    IEnumerator ShowLevelUp()
    {
        LevelUpText.text = "Level UP\n Current Level: " + level;
        
        yield return new WaitForSeconds(2);
        LevelUpText.text = " ";
    }

    IEnumerator GameOver()
    {
        gameOverText.text = "GAME OVER";
        Time.fixedDeltaTime = 0;
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Gameplay");
    }
}
