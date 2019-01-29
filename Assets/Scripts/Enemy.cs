using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour {

    public enum ResistanceTypes
    {
        Fire,
        Ice,
        Lighting,
        Water
    }

    public int level;
    public int health = 100;
    private int damage;

    private bool alreadyShot=false;
    private bool notFollowing = false;
    private float moveSpeed = 3;
    private float minDistance;

    [SerializeField]
    private Light Spotligt;
    private Animator anim;


    public ResistanceTypes resistType;

    private Player player;
    public Transform playerTransform;

    private GameManager gameManager;

    private TextMesh healthText;

    //private Rigidbody rb;

    private CapsuleCollider bc;

    private Spells spells;

    // Use this for initialization
    void Start() {

        anim = gameObject.GetComponent<Animator>();
  
        player = GameObject.Find("Player").GetComponent<Player>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        resistType = (ResistanceTypes)Random.Range(0, System.Enum.GetValues(typeof(ResistanceTypes)).Length);
        healthText = GameObject.Find("HealthText").GetComponent<TextMesh>();

        GetResistanceType();
        Stats();
        bc = this.gameObject.GetComponent<CapsuleCollider>();
        //rb = GetComponent<Rigidbody>();
        //StartCoroutine(EnemyShooting(5));
    }

    // Update is called once per frame
    void Update() {
         EnemyMoving();
        IntensityColor();


    }

    private void IntensityColor()
    {
        float phi = Time.time / 1.0f * 2 * Mathf.PI;
        float amplitude = Mathf.Cos(phi) * 10f + 10f;
        Spotligt.intensity = amplitude;
    }

    public void GetResistanceType()
    {

        if (resistType == ResistanceTypes.Fire)
        {
            Spotligt.color = Color.red;
            Debug.Log("Resistance: " + resistType);       
        }
        if (resistType == ResistanceTypes.Ice)
        {
            Spotligt.color = Color.white;
            Debug.Log("Resistance: " + resistType);
        }
        if (resistType == ResistanceTypes.Lighting)
        {
            Spotligt.color = Color.magenta;
            Debug.Log("Resistance: " + resistType);
        }
        if (resistType == ResistanceTypes.Water)
        {
            Spotligt.color = Color.blue;
            Debug.Log("Resistance: " + resistType);
        }
    }

    public void Stats()
    {
        level = player.level;
        health = level * 10;       
    }

    public void EnemyGetDamage(int dmg)
    {
       // rb.AddForce(Vector3.back, ForceMode.Impulse);
        health -= dmg;
        if (health <= 0)
        {
            Death();
        }
            
    }

    public void ShowStats()
    {
        healthText.text = health.ToString();
    }

    public void CancelShowStats()
    {
        healthText.text = "";
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.name == "FireSpell1(Clone)" && resistType == ResistanceTypes.Fire)
        {

            int random = Random.Range(5, 10);
            EnemyGetDamage(random);
        }
        if (collider.name == "IceSpell1(Clone)" && resistType == ResistanceTypes.Ice)
        {
      
            int random = Random.Range(5, 10);
            EnemyGetDamage(random);
        }
        if (collider.name == "LigthingSpell1(Clone)" && resistType == ResistanceTypes.Lighting)
        {

            int random = Random.Range(5, 10);
            EnemyGetDamage(random);
        }
        if (collider.name == "WaterSpell1(Clone)" && resistType == ResistanceTypes.Water)
        {

            int random = Random.Range(5, 10);
            EnemyGetDamage(random);
        }

        //{
        //    //if (resistType == ResistanceTypes.Fire)
        //    //{
        //    //    player.DoDamage(25);
        //    //    Debug.Log("Collision Fire");
        //    //}
        //    //if (resistType == ResistanceTypes.Ice)
        //    //{
        //    //    player.DoDamage(25);
        //    //    Debug.Log("Collision Ice");
        //    //}
        //    //if (resistType == ResistanceTypes.Lighting)
        //    //{
        //    //    player.DoDamage(25);
        //    //    Debug.Log("Collision Lighting");
        //    //}
        //    //if (resistType == ResistanceTypes.Unknown)
        //    //{
        //    //    player.DoDamage(25);
        //    //    Debug.Log("Collision Unknown");
        //    //}

        

    }

    public void EnemyDoDamage()
    {
        int dmg = Random.Range(5, 8) * level;
        player.GetDamage(dmg);
       
    }

    public void EnemyMoving()
    {
        transform.LookAt(playerTransform);

        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        minDistance = Vector3.Distance(transform.position, playerTransform.position);
        if (minDistance > 4)
        {
            anim.SetBool("isAttacking", false);
            anim.SetBool("isRunning", true);
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);           
        }

        else
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isAttacking", true);
            if(alreadyShot==false)
               StartCoroutine(EnemyShooting());
        }
            
    }

    public void Death()
    {        
       
       int xp = Random.Range(5, 10);
       player.AddXP(xp);
       int score = Random.Range(1, 3) * level;
       gameManager.AddScore(score);
        gameManager.canSpawn = false;
       // StartCoroutine(gameManager.NextWaveAnnouncer(5));
        Destroy(bc.gameObject);
    }

    IEnumerator EnemyShooting()
    {
        EnemyDoDamage();
        // enemyShootPoint = GameObject.Find("EnemyShootPoint");
        //Instantiate(enemySpellBullet, enemyShootPoint.transform.position, enemyShootPoint.transform.rotation);
        alreadyShot = true;
       Debug.Log("Enemy had shot");                  
       yield return new WaitForSeconds(1);
        alreadyShot = false;
    }

}


