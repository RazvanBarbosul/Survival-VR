using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject enemyBoss;
    [SerializeField]
    private GameObject[] portals;
    private GameObject enemySpawned;

    private GameObject player;
    [SerializeField]
    private GameObject portal;

    private bool alreadySpawned = true;
    private bool waveCleared = true;

    public Text waveTimeCountText;
    public Text currentWaveText;
    private int seconds=5;

    private int currentWave;
    private float wavesCount;
    private float hazardCount = 2;
    private float hazardLeft;
    private float waveWait = 5;
    private float spawnWait = 2;
    private float startWait = 1;
    private int timeUntilNextWave=5;

    private float waveTimeCount;

    private int totalScore;
    public Text totalScoreText;

    public Text nextWaveInfo;

    private Coroutine testEN;


    public bool canSpawn=false;

    // Use this for initialization
    void Start() {
        //enemy = Resources.Load("Prefabs/Enemy") as GameObject;
        //enemyBoss = Resources.Load("Prefabs/EnemyBoss") as GameObject;
        player = GameObject.Find("Player");
        Debug.Log("Portals:" + portals.Length);
        int RandomPortal = Random.Range(0, portals.Length);
        // portal = GameObject.Find("Portal");
        portal = portals[RandomPortal];
        hazardLeft = hazardCount;
        StartCoroutine(NextWaveAnnouncer());
        //StartCoroutine(Spawn());      
    }

    // Update is called once per frame
    void Update() {      
        UpdateScore();
        if (!GameObject.Find("Enemy(Clone)") && !GameObject.Find("EnemyBoss(Clone)") && canSpawn == false)
        {
            StartCoroutine(NextWaveAnnouncer());
        }
        NextWaveCheck();
    }

    public void NextWaveCheck()
    {
        if (!GameObject.Find("Enemy(Clone)") && !GameObject.Find("EnemyBoss(Clone)"))
        {
            
            if (currentWave >= 1)
            {
                WaveCleared();
            }
            currentWave += 1;
            seconds = 5;
            Debug.Log("SECONDS:!!!! : " + seconds);
            currentWaveText.text = "Current Wave: " + currentWave;
            Debug.Log("Next Wave is incoming...");
            TimeUpdate();
            // InvokeRepeating("WaveTimeCount", 0, 1);
            
            if (wavesCount < 2)
            {
                if (alreadySpawned == true)
                {                                  
                    waveCleared = true;
                    seconds = 5;
                    hazardLeft = hazardCount;
                    StartCoroutine(SpawnWaves());
                    if (!IsInvoking("WaveTimeCount"))
                    {
                        InvokeRepeating("WaveTimeCount", 0, 1);
                    } 
                    alreadySpawned = false;
                }
            }
            else if (wavesCount==2)
            {
                if (!IsInvoking("WaveTimeCount"))
                {
                    InvokeRepeating("WaveTimeCount", 0, 1);
                }
                Debug.Log("Boss is coming");
                BossSpawn();
            }                   
        }
        else
        {            
            Debug.Log("Wave still existing");
        }
    }


    IEnumerator Spawn()
    {
        
        yield return new WaitForSeconds(1);
        NextWaveCheck();
        //StartCoroutine(Spawn());
    }

    public IEnumerator NextWaveAnnouncer()
    {
        canSpawn = true;
        if (seconds <= 0)
        {
            StartCoroutine(Spawn());
            nextWaveInfo.text = "";
        }
        else
        {
            if (IsInvoking("WaveTimeCount"))
            {
                CancelInvoke("WaveTimeCount");
            }
            seconds -= 1;
            nextWaveInfo.text = "Next wave in: " + seconds + " seconds";
            yield return new WaitForSeconds(1);
            StartCoroutine(NextWaveAnnouncer());
        }         
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (alreadySpawned==false)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                
                //Vector3 spawnPosition = new Vector3(portal.transform.position.x, portal.transform.position.y, portal.transform.position.z-3);
                Vector3 spawnPosition = new Vector3(37,3,15);
                Quaternion spawnRotation = Quaternion.identity;
                enemySpawned=Instantiate(enemy, portal.transform.position, spawnRotation);
                hazardLeft -= 1;
                //Debug.Log("LEFT: " + hazardLeft);
                yield return new WaitForSeconds(spawnWait);
                
            }
            if (hazardLeft == 0)
            {
               
                alreadySpawned = true;
                wavesCount += 1;
               
                // Debug.Log("Waves Count: " + wavesCount);
            }

            else
            {
                Debug.Log("THE FUK WAVE WAIT");
                yield return new WaitForSeconds(waveWait);
                
            }

        }
    }

    void  WaveTimeCount( )
    {
            waveTimeCount -= 1;
    }

    IEnumerator WaveTimeCountTest()
    {
        yield return new WaitForSeconds(1);
        waveTimeCount -= 1;
        StartCoroutine(WaveTimeCountTest());
        
    }

    public void BossSpawn()
    {
        Vector3 spawnPosition = new Vector3(37, 3, 15);
        Quaternion spawnRotation = Quaternion.identity;
        enemySpawned = Instantiate(enemyBoss, spawnPosition, spawnRotation);
        wavesCount = 0;
    }

    //public void EnemySpawn()
    //{
    //    float xCoordinate = Random.Range(30, 60);
    //    float zCoordinate = Random.Range(30, 60);
    //    enemySpawned = Instantiate(enemy, new Vector3(portal.transform.position.x, portal.transform.position.y, portal.transform.position.z), Quaternion.identity);
    //    Debug.Log("Spawn Complete!");
    //    Debug.Log(enemy.transform.position);
    //    alreadySpawned = false;
    //}

    private void TimeUpdate()
    {
        waveTimeCount = ((wavesCount+1) * 30) ;
    }

    private void WaveCount()
    {
       
           if (waveTimeCount <= 0)
            {
            waveTimeCount = 0;
            }
        
    }

    public void WaveCleared()
    {           
            AddScore((int)waveTimeCount);
            Debug.Log("Added TIME SCORE: " + waveTimeCount);
    }

    public void UpdateScore()
    {
        
        totalScoreText.text ="Current Score: "+ totalScore.ToString();
        waveTimeCountText.text = "Kill Enemies in : " + waveTimeCount.ToString();
        WaveCount();
    }

    public void AddScore(int scr)
    {
        totalScore += scr;
    }
}
