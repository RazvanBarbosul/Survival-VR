using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour {

    public int level;
    public int health = 100;
    private int damage;

    private bool alreadyShot = false;
    private bool notFollowing = false;
    private float moveSpeed = 3;
    private float minDistance;

    private Animator anim;

    private Player player;
    public Transform playerTransform;
    private GameManager gameManager;
    private TextMesh healthText;

    // Use this for initialization
    void Start () {
        anim.GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<Player>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        healthText = GameObject.Find("HealthText").GetComponent<TextMesh>();

    }
	
	// Update is called once per frame
	void Update () {
        EnemyMoving();
    }

    public void Stats()
    {
        level = player.level * 3;
        health = level * 15;
    }

    public void EnemyGetDamage(int dmg)
    {

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



    public void EnemyDoDamage()
    {

    }

    public void EnemyMoving()
    {
        //transform.LookAt(playerTransform);

        //transform.position += transform.position * moveSpeed * Time.deltaTime;
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
            if (alreadyShot == false)
                StartCoroutine(EnemyShooting());
        }

    }

    public void Death()
    {
        int xp = Random.Range(40, 50);
        player.AddXP(xp);
        int score = Random.Range(1, 3) * level;
        gameManager.AddScore(score);
        gameManager.canSpawn = false;
        Destroy(this.gameObject);
    }

    IEnumerator EnemyShooting()
    {
        
       // enemyShootPoint = GameObject.Find("EnemyShootPoint");
        //Instantiate(enemySpellBullet, enemyShootPoint.transform.position, enemyShootPoint.transform.rotation);
        //alreadyShot = true;
        Debug.Log("Enemy had shot");
        yield return new WaitForSeconds(3);
        alreadyShot = false;
    }
}