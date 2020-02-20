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
            _FlockList[0].GetComponent<SC_FlockManager>()._SCKoaManager.GetHit(new Vector3(0,0,0));
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
        SpawnInitialFlock();
        waveStarted = true;
        
    }
    void SpawnInitialFlock()
    {
        _FlockList.Clear();
        int curIndex = 0;
        for (int i = 0; i < _curWaveSettings.initialSpawnFlockType.Length; i++)
        {
            
            for (int j = 0; j < _curWaveSettings.initialSpawnFlockQuantity[i]; j++)
            {
                SpawnNewFlock(_curWaveSettings.initialSpawnFlockType[i], curIndex);
                curIndex++;
            }
        }

        curBackupTimer = 0;


    }
    #endregion
    //---------------------------------------------------------------------//

    //---------------------------------------------------------------------//
    //------------------------ BACKUP MANAGEMENT --------------------------//
    //---------------------------------------------------------------------//
    #region Backup Management

    void SpawnBackupFlock()
    {
        int curIndex = 0;
        for (int i = 0; i < _curWaveSettings.backupSpawnFlockType.Length; i++)
        {
            for (int j = 0; j < _curWaveSettings.backupSpawnFlockQuantity[i]; j++)
            {
                SpawnNewFlock(_curWaveSettings.backupSpawnFlockType[i], curIndex, true);
                curIndex++;
            }
        }
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
                    SpawnBackupFlock();
                    backupSend = true;
                }
                else if (curBackupTimer >= _curWaveSettings.timeBeforeBackup)
                {
                    SpawnBackupFlock();
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

        //Instantiate new flock
        GameObject curFlock = Instantiate(_FlockPrefab);

        //Add new flock to the flock list
        _FlockList.Add(curFlock);


        float normalizedT = (1f / _curWaveSettings.getInitialFlockNumber()) * index;
        if(backup)
            normalizedT = (1f / _curWaveSettings.getBackupFlockNumber()) * index;

        
        //Initialize flock
        curFlock.GetComponent<SC_FlockManager>().InitializeFlock(flockSettings, normalizedT);
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
    }




    #endregion
    //---------------------------------------------------------------------//

}
