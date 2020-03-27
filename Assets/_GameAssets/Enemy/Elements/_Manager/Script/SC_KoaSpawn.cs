using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_KoaSpawn : MonoBehaviour
{
    #region Singleton

    private static SC_KoaSpawn _instance;
    public static SC_KoaSpawn Instance { get { return _instance; } }

    #endregion

    [SerializeField]
    GameObject koaPrefab;

    public GameObject[] koaTab;
    Coroutine[] corKoa;
    BezierSolution.BezierSpline[] splineSpawn;

    int indexSpawn = 0;
    float fallSpeed = 1200;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitAllKoa();
    }

    void InitAllKoa()
    {
        SC_EnemyManager enemyMng = SC_EnemyManager.Instance;
        splineSpawn = SC_SpawnInfo.Instance.GetBezierSplines();
        int index = 0;


        for (int i =0; i<enemyMng.phases.Length; i++)
        {
            PhaseSettings curPhase = enemyMng.phases[i];

            for (int j = 0; j< curPhase.waves.Length; j++)
            {
                WaveSettings curWave = curPhase.waves[j];

                for (int k = 0; k < curWave.initialSpawnFlock.Length; k++)
                {
                    index++;
                }
                for(int l =0; l<curWave.backupSpawnFlock.Length; l++)
                {
                    index++;
                }
            }
        }


        koaTab = new GameObject[index];
        
        index = 0;

        for (int i =0; i<enemyMng.phases.Length; i++)
        {
            PhaseSettings curPhase = enemyMng.phases[i];

            for (int j = 0; j< curPhase.waves.Length; j++)
            {
                WaveSettings curWave = curPhase.waves[j];

                for (int k = 0; k < curWave.initialSpawnFlock.Length; k++)
                {
                
                    koaTab[index] = Instantiate(koaPrefab);
                    DisplaceKoaOnSpawn(koaTab[index], curWave.initialSpawnPosition[k]);
                    index++;
                }
                for(int l =0; l<curWave.backupSpawnFlock.Length; l++)
                {
                 
                    koaTab[index] = Instantiate(koaPrefab);
                    DisplaceKoaOnSpawn(koaTab[index], curWave.backupSpawnPosition[l]);
                    index++;

                }
            }
        }
    }

    void DisplaceKoaOnSpawn(GameObject koa, int spawnPoint)
    {
        int rndx = Random.Range(-200, -100);
        int rndy = Random.Range(100, 500);
        int rndz = Random.Range(-500, 500);

        koa.transform.position = splineSpawn[spawnPoint].GetPoint(1);
        koa.transform.LookAt(new Vector3(0,0,0));
        koa.transform.Translate(new Vector3(-500 + rndx , rndy, rndz), Space.Self);
 
    }


    public void SpawnKoa()
    {
        StartCoroutine(SpawnCoro(indexSpawn));
        indexSpawn++;
    }
    IEnumerator SpawnCoro(int Index)
    {
        GameObject curKoa = koaTab[Index];

        curKoa.GetComponent<TrailRenderer>().enabled = true;
        while (curKoa.transform.position.y > -150)
        {
            curKoa.transform.position -= new Vector3(0, fallSpeed * Time.deltaTime, 0);
            yield return 0;

        }
    }

        // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            SpawnKoa();
        }
    }
}
