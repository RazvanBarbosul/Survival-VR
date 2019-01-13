using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour {

    public static Spells spells;

    private Player player;
    private Enemy enemy;
    private EnemyBoss enemyBoss;

    private float distance;
    private float speed = 7;

    SpellTypes spellTypes;

    // Use this for initialization
    void Start() {
        player = GameObject.Find("Player").GetComponent<Player>();
        spellTypes = (SpellTypes)Random.Range(0, System.Enum.GetValues(typeof(SpellTypes)).Length);
        GetName();
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        distance += 1 * Time.deltaTime;
        if (distance > 5)
        {
            Destroy(this.gameObject);
        }

    }

    public void GetName()
    {
        if (this.gameObject.name == "FireSpell")
        {
            spellTypes = SpellTypes.Fire;
        }
        if (this.gameObject.name == "IceSpell")
        {
            spellTypes = SpellTypes.Ice;
        }
        if (this.gameObject.name == "LightingSpell")
        {
            spellTypes = SpellTypes.Lighting;
        }
        if (this.gameObject.name == "WaterSpell")
        {
            spellTypes = SpellTypes.Water;
        }
    }

    public void GetSpellType(int numberType)
    {
        switch (numberType)
        {
            case 1:
                spellTypes = SpellTypes.Fire;
                break;
            case 2:
                spellTypes = SpellTypes.Ice;
                break;
            case 3:
                spellTypes = SpellTypes.Lighting;
                break;
            case 4:
                spellTypes = SpellTypes.Water;
                break;
        }
    }

    public void MatchResistance()
    {
        if (enemy.resistType == Enemy.ResistanceTypes.Fire)
        {
            if (spellTypes == SpellTypes.Fire)
            {
                enemy.EnemyGetDamage(10);
                Destroy(this.gameObject);
            }
            else
            {
                Debug.Log("Immune");
            }
        }
        if (enemy.resistType == Enemy.ResistanceTypes.Ice)
        {
            if (spellTypes == SpellTypes.Ice)
            {
                enemy.EnemyGetDamage(10);
                Destroy(this.gameObject);
            }
            else
            {
                Debug.Log("Immune");
            }
        }
        if (enemy.resistType == Enemy.ResistanceTypes.Lighting)
        {
            if (spellTypes == SpellTypes.Lighting)
            {
                enemy.EnemyGetDamage(10);
                Destroy(this.gameObject);
            }
            else
            {
                Debug.Log("Immune");
            }
        }
        if (enemy.resistType == Enemy.ResistanceTypes.Water)
        {
            if (spellTypes == SpellTypes.Water)
            {
                enemy.EnemyGetDamage(10);
                Destroy(this.gameObject);
            }
            else
            {
                Debug.Log("Immune");
            }
        }
    }

    

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            enemy = GameObject.Find("Enemy(Clone)").GetComponent<Enemy>();
            Debug.Log("DETECTED");
            MatchResistance();
        }
        if (collider.gameObject.tag != "Enemy")
        {
            Destroy(this.gameObject);
        }
        if (collider.gameObject.tag == "EnemyBoss")
        {
            enemyBoss = GameObject.Find("EnemyBoss(Clone)").GetComponent<EnemyBoss>();
            Debug.Log("DETECTED");
            enemyBoss.EnemyGetDamage(8);
            Destroy(this.gameObject);
        }
    }
}

public enum SpellTypes
{
    Fire,
    Ice,
    Lighting,
    Water
}