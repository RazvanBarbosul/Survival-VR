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
    [SerializeField]
    private GameObject arrowToPortal;

    private bool alreadySpawned = true;
    private bool waveCleared = true;

    public Text waveTimeCountText;
    public Text currentWaveText;
    private int seconds=10;

    private int currentWave;
    private float wavesCount=1;
    private float hazardCount = 10;
    private float hazardLeft;
    private float waveWait = 5;
    private float spawnWait = 5;
    private float startWait = 3;
    private int timeUntilNextWave=10;

    private float waveTimeCount;

    private int totalScore;
    public Text totalScoreText;

    public Text nextWaveInfo;

    public bool canSpawn=false;

    // Use this for initialization
    void Start() {

        //enemy = Resources.Load("Prefabs/Enemy") as GameObject;
        //enemyBoss = Resources.Load("Prefabs/EnemyBoss") as GameObject;
        player = GameObject.Find("Player");
       // Debug.Log("Portals:" + portals.Length);
        //int RandomPortal = Random.Range(0, portals.Length);
        //portal = portals[RandomPortal];
        
        hazardLeft = hazardCount;
        StartCoroutine(NextWaveAnnouncer());
        //StartCoroutine(Spawn());      
        //arrowToPortal.transform.position = portal.transform.position;
    }

    // Update is called once per frame
    void Update() {      
        UpdateScore();
        if (!GameObject.Find("Enemy(Clone)") && !GameObject.Find("EnemyBoss(Clone)") && canSpawn == false)
        {
            StartCoroutine(NextWaveAnnouncer());
        }
        //NextWaveCheck();
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
            currentWaveText.text = "Current Wave: " + currentWave;
            //Debug.Log("Next Wave is incoming...");
            TimeUpdate();
            int RandomPortal = Random.Range(0, portals.Length);
            portal = portals[RandomPortal];
            //arrowToPortal.transform.position = portal.transform.position;
            arrowToPortal.transform.position = new Vector3(portal.transform.position.x, 20f, portal.transform.position.z);
            //arrowToPortal.transform.position = player.transform.position;
            if (wavesCount % 5 == 0)
            {
                waveCleared = true;
                seconds = 5;
                BossSpawn();
                if (!IsInvoking("WaveTimeCount"))
                {
                    InvokeRepeating("WaveTimeCount", 0, 1);
                }
                //Debug.Log("Boss is coming");
                alreadySpawned = false;
            }
            else
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
            //if (wavesCount < 2)
            //{
            //    if (alreadySpawned == true)
            //    {
            //        waveCleared = true;
            //        seconds = 5;
            //        hazardLeft = hazardCount;
            //        StartCoroutine(SpawnWaves());
            //        if (!IsInvoking("WaveTimeCount"))
            //        {
            //            InvokeRepeating("WaveTimeCount", 0, 1);
            //        }
            //        alreadySpawned = false;
            //    }
            //}
            //else if (wavesCount == 2)
            //{
            //    waveCleared = true;
            //    seconds = 5;
            //    BossSpawn();
            //    if (!IsInvoking("WaveTimeCount"))
            //    {
            //        InvokeRepeating("WaveTimeCount", 0, 1);
            //    }
            //    Debug.Log("Boss is coming");
            //    alreadySpawned = false;
            //}
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

                Quaternion spawnRotation = Quaternion.identity;
                enemySpawned=Instantiate(enemy, portal.transform.position, spawnRotation);
                hazardLeft -= 1;
                yield return new WaitForSeconds(spawnWait);
                
            }
            if (hazardLeft == 0)
            {
               
                alreadySpawned = true;
                wavesCount += 1;
            }

            else
            {
                //Debug.Log("THE FUK WAVE WAIT");
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
       
            Quaternion spawnRotation = Quaternion.identity;
            enemySpawned = Instantiate(enemyBoss, portal.transform.position, spawnRotation);
            wavesCount = 1;
            alreadySpawned = true;
        
        
    }

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
            //Debug.Log("Added TIME SCORE: " + waveTimeCount);
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
