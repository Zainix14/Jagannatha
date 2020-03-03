using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Script gerant la wave d'enemy actuel selon le Setting donné
///  | Sur le prefab EnemyManager(à instantié une fois)
///  | Auteur : Zainix
/// </summary>
public class SC_WaveManager : MonoBehaviour
{

    #region Singleton

    private static SC_WaveManager _instance;
    public static SC_WaveManager Instance { get { return _instance; } }

    #endregion
    //---------------------------------------------------------------------//
    //---------------------------- VARIABLES ------------------------------//
    //---------------------------------------------------------------------//
    #region Variables
    WaveSettings _curWaveSettings;
    public bool waveStarted;
    public bool waveEnded;
    //Récupère les prefabs
    [SerializeField]
    GameObject _FlockPrefab; //Prefab du flock gérant la totalité de la nuée (guide compris)
    [SerializeField]
    GameObject _MultiFlockManagerPrefab; //Préfab du mutli flock manager, instantié lors d'un rassemblment de plusieurs flock

    List<GameObject> _FlockList; //Contient la totalité des flocks présents dans le jeu


    float curBackupTimer = 0;
    bool backupSend;


    Vector3Int sensitivityA;
    Vector3Int sensitivityB;
    Vector3Int sensitivityC;

    #endregion
    //---------------------------------------------------------------------//

    #region Start/Update
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


        _FlockList = new List<GameObject>(); //Instantiation de la list de flock
        
        resetVariables();

    }

    void Update()
    {
        BackupUpdate();

        //Debug
        if(Input.GetKeyDown(KeyCode.G))
        {
            _FlockList[0].GetComponent<SC_FlockManager>()._SCKoaManager.GetHit(new Vector3(100,0,0));
        }
    }
    #endregion

    //---------------------------------------------------------------------//
    //------------------------ INITIALIZE NEW WAVE ------------------------//
    //---------------------------------------------------------------------//
    #region Initialize New Wave
    public void InitializeWave(WaveSettings newWaveSettings)
    {

        resetVariables();
        _curWaveSettings = newWaveSettings;

        if (!_curWaveSettings.backup)
            backupSend = true;
        StartCoroutine(SpawnInitialFlock());

        waveStarted = true;

        
    }
    IEnumerator SpawnInitialFlock()
    {
        _FlockList.Clear();
        int curIndex = 0;


        for (int i = 0; i < _curWaveSettings.initialSpawnFlockType.Length; i++)
        {
            
            for (int j = 0; j < _curWaveSettings.initialSpawnFlockQuantity[i]; j++)
            {
                
                SpawnNewFlock(_curWaveSettings.initialSpawnFlockType[i], curIndex);
                curIndex++;
                yield return new WaitForSeconds(_curWaveSettings.timeBetweenSpawnInitial);
                
            }
        }
        StopCoroutine(SpawnInitialFlock());
        curBackupTimer = 0;


    }
    #endregion
    //---------------------------------------------------------------------//

    //---------------------------------------------------------------------//
    //------------------------ BACKUP MANAGEMENT --------------------------//
    //---------------------------------------------------------------------//
    #region Backup Management

    IEnumerator SpawnBackupFlock()
    {
        int curIndex = 0;
        for (int i = 0; i < _curWaveSettings.backupSpawnFlockType.Length; i++)
        {
            for (int j = 0; j < _curWaveSettings.backupSpawnFlockQuantity[i]; j++)
            {
                SpawnNewFlock(_curWaveSettings.backupSpawnFlockType[i], curIndex, true);
                curIndex++;
                yield return new WaitForSeconds(_curWaveSettings.timeBetweenSpawnBackup);

            }
        }
        StopCoroutine(SpawnBackupFlock());
    }



    void BackupUpdate()
    {
       
        if(waveStarted)
        {
            
            if (_curWaveSettings.backup && !backupSend)
            {
                curBackupTimer += Time.deltaTime;

                if (_FlockList.Count <= _curWaveSettings.flockLeftBeforeBackup)
                {
                    StartCoroutine(SpawnBackupFlock());
                    backupSend = true;
                }
                else if (curBackupTimer >= _curWaveSettings.timeBeforeBackup)
                {
                    StartCoroutine(SpawnBackupFlock());
                    backupSend = true;

                }
            }
        }

    }

    #endregion
    //---------------------------------------------------------------------//


    //---------------------------------------------------------------------//
    //---------------------------- END WAVE -------------------------------//
    //---------------------------------------------------------------------//
    #region End Wave
    public void FlockDestroyed(GameObject flock)
    {
        for (int i = 0; i < _FlockList.Count; i++)
        {
            if (_FlockList[i] == flock)
            {
                _FlockList.RemoveAt(i);
            }
        }


        if (_FlockList.Count == 0 && backupSend )
        {
            waveEnded = true;
            SC_PhaseManager.Instance.EndWave();
           

        }
    }

    #endregion
    //---------------------------------------------------------------------//

    //---------------------------------------------------------------------//
    //--------------------------- UTILITIES -------------------------------//
    //---------------------------------------------------------------------//
    #region Utilites
    /// <summary>
    /// Invoque un nouveau Flock
    /// </summary>
    void SpawnNewFlock(FlockSettings flockSettings,float index, bool backup = false)
    {
        Vector3Int newSensitivity = new Vector3Int(0, 0, 0);
        Vector3Int baseSensitivity = new Vector3Int(0, 0, 0);
        switch(flockSettings.attackType)
        {
            case FlockSettings.AttackType.none:
                baseSensitivity = sensitivityA;
                break;


            case FlockSettings.AttackType.Bullet:
                baseSensitivity = sensitivityB;
                break;


            case FlockSettings.AttackType.Laser:
                baseSensitivity = sensitivityC;
                break;
        }

    
        int x = baseSensitivity.x + Random.Range(-1, 2);
        if (x < 0) x = 0;if (x > 5) x = 5; 
        int y = baseSensitivity.y + Random.Range(-1, 2);
        if (y < 0) x = 0; if (y > 5) y = 5;
        int z = baseSensitivity.z + Random.Range(-1, 2);
        if (z < 0) z = 0; if (z > 5) z = 5;
        newSensitivity = new Vector3Int(x, y, z);
        
        //Instantiate new flock
        GameObject curFlock = Instantiate(_FlockPrefab);

        //Add new flock to the flock list
        _FlockList.Add(curFlock);


        float normalizedT = (1f / _curWaveSettings.getInitialFlockNumber()) * index;
        if(backup)
            normalizedT = (1f / _curWaveSettings.getBackupFlockNumber()) * index;

        //Initialize flock
        curFlock.GetComponent<SC_FlockManager>().InitializeFlock(flockSettings, normalizedT,newSensitivity);
    }


 

    /// <summary>
    /// Fusion de plusieur flock | Parametre : List de flock a fusionnés
    /// </summary>
    /// <param name="flockToMerge"></param>
    void StartMultiFlock(List<GameObject> flockToMerge)
    {
        GameObject newMultiFlock =  Instantiate(_MultiFlockManagerPrefab, flockToMerge[0].transform.position, transform.rotation); //Instantie un nouveau MultiFlock manager à la position du premier flock de la list
        newMultiFlock.GetComponent<SC_MultiFlockManager>().Initialize(flockToMerge); //Initialize la fusion des flock dans la liste
    }


    void resetVariables()
    {
        curBackupTimer = 0;

        backupSend = false;
        waveEnded = false;
        waveStarted = false;
        GenerateNewSensitivity();

    }

    void GenerateNewSensitivity()
    {
        int x;
        int y;
        int z;
        x = Random.Range(0, 6);
        y = Random.Range(0, 6);
        z = Random.Range(0, 6);

        sensitivityA = new Vector3Int(x, y, z);

        x = Random.Range(0, 6);
        y = Random.Range(0, 6);
        z = Random.Range(0, 6);

        sensitivityB = new Vector3Int(x, y, z);

        sensitivityC = new Vector3Int(GetRangedValue(x), GetRangedValue(y), GetRangedValue(z));


    }
    int GetRangedValue(int baseValue)
    {
        int newValue;

        if(baseValue >= 3)
        {
            newValue = baseValue - Random.Range(2, 5);
            if (newValue < 0)
            {
                newValue = 0;
            }
        }
        else
        {
            newValue = baseValue + Random.Range(2, 5);
            if (newValue > 5)
            {
                newValue = 5;
            }
        }

        return newValue;

    }



    #endregion
    //---------------------------------------------------------------------//

}
