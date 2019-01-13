using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpellBullet : MonoBehaviour {

    public static EnemySpellBullet enemySpellBullet;

    private Player player;
    private Enemy enemy;

    private float distance;
    private float speed = 7;

    // Use this for initialization
    void Start () {
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        distance += 1 * Time.deltaTime;
        if (distance > 5)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            player = GameObject.Find("Player").GetComponent<Player>();

            enemy.EnemyDoDamage();
        }
        //if (collider.gameObject.tag != "Player")
        //{
        //    Destroy(this.gameObject);
        //}
    }
}
