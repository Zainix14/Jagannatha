using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Le Koa Manager gère tout les Boids et le Koa, exécute les ordres du Flock Manager 
///  | Sur le prefab Flock
///  | Auteur : Zainix
/// </summary>
public class SC_KoaManager : MonoBehaviour
{

    GameObject Mng_CheckList;
    GameObject NetPlayerP;
    SC_NetPSpawnKoa_P NetPSpawnKoa;
    Vector3Int sensitivity;

    [SerializeField]
    GameObject _koaPrefab; //Prefab du Koa

    int KoaLife = 10;

    GameObject _koa; //Koa du 

    /// <summary>
    /// Current BoidSettings
    /// </summary>
    BoidSettings curBoidSettings; //Paramètres dans le scriptableObject Settings
    FlockSettings curFlockSettings; //Paramètres dans le scriptableObject Settings

    //public ComputeShader compute; //Shader
    SC_FlockManager flockManager;
    /// <summary>
    /// Contient toute la liste des guides actuels 
    /// </summary>
    List<Transform> _guideList;
    /// <summary>
    /// Guide actuel du Koa
    /// </summary>
    Transform _curKoaGuide; //Target

    /// <summary>
    /// List de tout les boids contenus dans le Flock
    /// </summary>
    Boid[] _boidsTab; //Tableau contenant les boids

    float respawnTimer;
    int spawnCount;
    public bool isActive;

    /// <summary>
    /// Avant le start, instanciation
    /// </summary>
    public void Initialize(Transform newGuide, int newSpawnCount, BoidSettings newSettings, FlockSettings flockSettings)
    {

        GetReferences();
        flockManager = newGuide.GetComponent<SC_FlockManager>();
        curFlockSettings = flockSettings;
        spawnCount = newSpawnCount;

        //Instanciation des list de Boid et de Guide
        _boidsTab = SC_BoidPool.Instance.GetBoid(curFlockSettings.maxBoid);
        _guideList = new List<Transform>();

        //Récupération du comportement initial
        curBoidSettings = newSettings;
        sensitivity = GetNewSensitivity();
        //Ajout du premier guide a la liste
        _guideList.Add(newGuide);

        respawnTimer = 0;
        if (_koaPrefab != null)
        {
            _koa = NetPSpawnKoa.SpawnKoa();
            _koa.transform.position = transform.position;
            _koa.GetComponent<SC_KoaCollider>().Initialize(this);
            _koa.GetComponent<SC_MoveKoaSync>().InitOPKoaSettings(sensitivity,flockSettings.spawnTimer);
        }
        //InitBoids();
    }


    void InitBoids()
    {
        //Initialisation de tout les boids
        for (int i = 0; i < spawnCount; i++)
        {
            Boid boid = _boidsTab[i];

            //Transform
            boid.transform.position = transform.position; //Déplacement à la position
            boid.transform.forward = Random.insideUnitSphere; //Rotation random

            //Lance l'initialisation de celui-ci avec le comportement initial et le premier guide
            boid.Initialize(curBoidSettings, _guideList[0], sensitivity);
        }

        //Instantie le Koa

        _koa.GetComponent<SC_MoveKoaSync>().SetPilotMeshActive();
        _curKoaGuide = _boidsTab[1].transform;
        _boidsTab[1].GetComponent<BoxCollider>().enabled = false;

    }

    void Update()
    {
        if(isActive)
        {
            if (_koa != null)
            {
                _koa.transform.position = _curKoaGuide.position;
            }
            respawnTimer += Time.deltaTime;
            if (respawnTimer > (60f / curFlockSettings.regenerationRate))
            {
                respawnTimer = 0;
                GenerateNewBoid();
            }
        }

    }

    void GetReferences()
    {

        if (Mng_CheckList == null)
            Mng_CheckList = GameObject.FindGameObjectWithTag("Mng_CheckList");
        else
            Debug.LogWarning("SC_KoaManager - Cant Find Mng_CheckList");

        if (Mng_CheckList != null && NetPlayerP == null)
            NetPlayerP = Mng_CheckList.GetComponent<SC_CheckList>().GetNetworkPlayerPilot();
        else
            Debug.LogWarning("SC_KoaManager - Cant Find NetPlayerP");

        if (NetPlayerP != null && NetPSpawnKoa == null)
            NetPSpawnKoa = NetPlayerP.GetComponent<SC_NetPSpawnKoa_P>();
        else
            Debug.LogWarning("SC_KoaManager - Cant Find SC_NetPSpawnKoa_P");

    }

    /// <summary>
    /// Lance le split de la nuée en fonction des guides envoyé par le Flock Manager | Param : List<Transform> nouveau guides (la division dépends du nombre de guide)
    /// </summary>
    /// <param name="newGuides"></param>
    public void Split(List<Transform> newGuides)
    {

        int splitNumber = newGuides.Count;//Nombre de division en fonciton du nombre de guides envoyé
        _guideList.Clear(); //Vide la guide liste de tout les guides actuel

        //Ajoute tout les novueaux guide a la list de guides
        foreach (Transform t in newGuides)
        {
            _guideList.Add(t);
        }


        //---------------------- Répartition des boids sur les guides de facon proportionnel
        int nbActiveBoid = 0;
        for(int i = 0; i<_boidsTab.Length; i++)
        {
            if(_boidsTab[i].isActive)
            {
                nbActiveBoid++;
            }
        }

        int all = nbActiveBoid; //Total des boids
        int div = splitNumber; //Total de guides
        float val = all / div; //Nombre de boids par Guides

        //Affectation des guides
        for (int i = 0; i < div; i++)
        {
            for (int j = Mathf.CeilToInt(val * i); j < Mathf.CeilToInt(val * (i + 1)); j++)
            {
                _boidsTab[j].GetComponent<Boid>().target = _guideList[i];
            }
        }
        //Si impaire, réparti le dernier boid sur une target
        _boidsTab[all - 1].GetComponent<Boid>().target = _guideList[div - 1];

    }


    /// <summary>
    /// Changement de comportement des boids | Param : BoidSettings Nouveau comportement <> bool poids vers la target supèrieur pour le Koa
    /// </summary>
    /// <param name="newSettings"></param>
    /// <param name="KoaTargetWeight"></param>
    public void SetBehavior(BoidSettings newSettings, bool KoaTargetWeight = false)
    {
        for (int i = 0; i < _boidsTab.Length; i++)
        {
            _boidsTab[i].SetNewSettings(newSettings);
            curBoidSettings = newSettings;;
        }
    }

    public void GenerateNewBoid()
    {
        
        for(int i=0;i<curFlockSettings.maxBoid; i++)
        {
            if(!_boidsTab[i].isActive)
            { 
                _boidsTab[i].transform.position = _koa.transform.position; //Déplacement à la position
                _boidsTab[i].transform.forward = Random.insideUnitSphere; //Rotation random
                int rnd = 0;
                if(_guideList.Count>1)
                {
                    rnd = Random.Range(1, _guideList.Count);
                }
                _boidsTab[i].Initialize(curBoidSettings, _guideList[rnd],sensitivity);
                return;
            }
        }
    }

    public void GetHit(Vector3 gunSensitivity)
    {

        if (gunSensitivity == sensitivity)
        {
            KoaLife -= 5;
        }
        else KoaLife -= 2;


        if (KoaLife <= 0) DestroyFlock();
    }

    void DestroyFlock()
    {
        //SetBehavior(DeathSettings);
        foreach (Boid b in _boidsTab)
        {
            b.DestroyBoid();

        }
        Destroy(_koa.gameObject);
        flockManager.DestroyFlock();
        Destroy(this.gameObject);
    }


    Vector3Int GetNewSensitivity()
    {
        int x = Random.Range(0, 6);
        int y = Random.Range(0, 6);
        int z = Random.Range(0, 6);

        return new Vector3Int(x, y, z);
    }

    public void ActivateKoa()
    {
        InitBoids();
        isActive = true;
    }

}