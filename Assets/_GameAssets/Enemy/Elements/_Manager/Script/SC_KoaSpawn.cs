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

    GameObject Player;

    GameObject[,,,] koaTab2;

    public int nb_totalFlock;

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
        splineSpawn = SC_SpawnInfo.Instance.GetBezierSplines();
        Player = GameObject.FindGameObjectWithTag("Player");
        
    }

    public void KoaCountMaster()
    {
        for (int i = 0; i < koaTab2.GetLength(0); i++)
        {
            for (int j = 0; j < koaTab2.GetLength(1); j++)
            {
                for (int k = 0; k < koaTab2.GetLength(2); k++)
                {
                    for (int l = 0; l < koaTab2.GetLength(3); l++)
                    {
                        if (koaTab2 [i, j, k, l] != null)
                        {
                            nb_totalFlock += 1;
                        }
                        
                    }
                }
            }
        }
    }

    public void InitNewPhase(PhaseSettings newPhaseSettings)
    {
        int nbMaxFlock = 0;
        int nbSpawn = 8;
        int nbWaves = newPhaseSettings.waves.Length;

        GameObject container = Instantiate(containerPrefab);

        for (int i = 0; i < nbWaves; i++)
        {
            int nbFlock = newPhaseSettings.waves[i].initialSpawnFlock.Length;
            if (nbFlock < newPhaseSettings.waves[i].backupSpawnFlock.Length) nbFlock = newPhaseSettings.waves[i].backupSpawnFlock.Length;


            if (nbFlock > nbMaxFlock)
            {
                nbMaxFlock = nbFlock;
            }
        }

        koaTab2 = new GameObject[nbWaves, 2, nbMaxFlock, nbSpawn];

        for (int i = 0; i < nbWaves; i++)
        {

            WaveSettings curWave = newPhaseSettings.waves[i];

            for (int j = 0; j < curWave.initialSpawnFlock.Length; j++)
            {
                koaTab2[i,0,j, curWave.initialSpawnPosition[j]] = Instantiate(koaPrefab);
                DisplaceKoaOnSpawn(koaTab2[i, 0, j, curWave.initialSpawnPosition[j]], curWave.initialSpawnPosition[j]);
                koaTab2[i, 0, j, curWave.initialSpawnPosition[j]].transform.SetParent(container.transform);
          
            }
            for (int k = 0; k < curWave.backupSpawnFlock.Length; k++)
            {
                koaTab2[i, 1, k, curWave.backupSpawnPosition[k]] = Instantiate(koaPrefab);
                DisplaceKoaOnSpawn(koaTab2[i, 1, k, curWave.backupSpawnPosition[k]], curWave.backupSpawnPosition[k]);
                koaTab2[i, 1, k, curWave.backupSpawnPosition[k]].transform.SetParent(container.transform);
            }
        }
        KoaCountMaster();
    }

 


    void DisplaceKoaOnSpawn(GameObject koa, int spawnPoint)
    {
        int rndx = Random.Range(-200, -150);
        int rndy = Random.Range(100, 800);
        int rndz = Random.Range(-300,300);



        int rndscale = Random.Range(0, 250);
        for (int i = 0; i<koa.transform.childCount;i++)
        {
            koa.transform.GetChild(i).transform.localScale = new Vector3(1,1,rndscale);
        }

        koa.transform.position = splineSpawn[spawnPoint].GetPoint(1);
        koa.transform.LookAt(Player.transform);
        koa.transform.Translate(new Vector3(-1200 + rndx , rndy, rndz), Space.Self);
        koa.transform.LookAt(Player.transform);

        for (int i = 0; i < koaTab2.GetLength(0); i++) 
        {
            for (int j = 0; j < koaTab2.GetLength(1); j++)
            {
                for (int k = 0; k < koaTab2.GetLength(2); k++)
                {
                    for (int l = 0; l < koaTab2.GetLength(3); l++)
                    {
                        if(koaTab2[i,j,k,l] != null)
                        StartCoroutine(GoTargetPos(i, j, k, l,700,8f));
                    }
                }
            }
        }
        //POURQUOI C'EST DE LA MERDE CA ICI
    }

    public IEnumerator SpawnCoro(int wi, int backup, int flockrank, int spawnPos )
    {
        
        GameObject curKoa = koaTab2[wi,backup,flockrank,spawnPos];
        curKoa.GetComponent<TrailRenderer>().enabled = true;

        while (curKoa.transform.position.y > -150)
        {
            curKoa.transform.Translate(new Vector3(0, -fallSpeed * Time.deltaTime, 0));
            yield return 0;
        }
        yield return 0;
    }


    public IEnumerator GoTargetPos(int wi, int backup, int flockrank, int spawnPos, int minDist, float travelTime)
    {
        GameObject curKoa = koaTab2[wi, backup, flockrank, spawnPos];
        float curDist = Vector3.Distance(curKoa.transform.position, Player.transform.position);
        float distanceToTravel = curDist - minDist;
        float distancePerSec = distanceToTravel/ travelTime;

        float t = 0;
        float rate = 1 / travelTime;

        while (t < 1)
        {

            t += Time.deltaTime * rate;

            if (Vector3.Distance(curKoa.transform.position, Player.transform.position) > minDist)
                curKoa.transform.Translate(Vector3.forward * Time.deltaTime * distancePerSec);

            
            yield return 0;

        }

        /*
        while (Vector3.Distance(curKoa.transform.position,Player.transform.position) > minDist)
        {
            curKoa.transform.Translate(Vector3.forward * Time.deltaTime * speed);
            yield return 0;
        }*/
    }
}
