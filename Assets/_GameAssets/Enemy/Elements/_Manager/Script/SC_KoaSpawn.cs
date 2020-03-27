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

    [SerializeField]
    GameObject containerPrefab;

    public GameObject[] koaTab;
    public int[] waveIndex;
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

        GameObject container = Instantiate(containerPrefab);

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

        waveIndex = new int[index];
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
                    waveIndex[index] = j;
                    DisplaceKoaOnSpawn(koaTab[index], curWave.initialSpawnPosition[k]);
                    koaTab[index].transform.SetParent(container.transform);
                    index++;
                }
                for(int l =0; l<curWave.backupSpawnFlock.Length; l++)
                {
                 
                    koaTab[index] = Instantiate(koaPrefab);
                    waveIndex[index] = j;
                    DisplaceKoaOnSpawn(koaTab[index], curWave.backupSpawnPosition[l]);
                    koaTab[index].transform.SetParent(container.transform);
                    index++;

                }
            }
        }
    }

    void DisplaceKoaOnSpawn(GameObject koa, int spawnPoint)
    {
        int rndx = Random.Range(-200, -100);
        int rndy = Random.Range(100, 500);
        int rndz = Random.Range(-200, 200);

        for(int i = 0; i<koa.transform.childCount;i++)
        {
            int rndscale = Random.Range(0, 250);
            koa.transform.GetChild(i).transform.localScale = new Vector3(1,1,rndscale);
        }

        koa.transform.position = splineSpawn[spawnPoint].GetPoint(1);
        koa.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        koa.transform.Translate(new Vector3(-500 + rndx , rndy, rndz), Space.Self);
        koa.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);

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
            curKoa.transform.Translate(new Vector3(0, -fallSpeed * Time.deltaTime, 0));
            yield return 0;
        }
    }


    public void PreparationKoa(int index)
    {
        for (int i = 0; i < waveIndex.Length; i++)
        {
            if (waveIndex[i] == index)
            {
                GameObject curKoa = koaTab[i];
              
                StartCoroutine(GoTargetPos(i,7.5f));
            }
        }
    }

    IEnumerator GoTargetPos(int index, float Duration)
    {
        GameObject curKoa = koaTab[index];
        float t = 0;
        float rate = 1 / Duration;

        while (t < 1)
        {

            t += Time.deltaTime * rate;

            curKoa.transform.Translate(Vector3.forward * Time.deltaTime*35);

            yield return 0;

        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
