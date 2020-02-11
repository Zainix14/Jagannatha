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


    [SerializeField]
    GameObject _koaPrefab; //Prefab du Koa


    GameObject _koa; //Koa du 

    /// <summary>
    /// Current BoidSettings
    /// </summary>
    BoidSettings _curSettings; //Paramètres dans le scriptableObject Settings

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


    /// <summary>
    /// Avant le start, instanciation
    /// </summary>
    public void Initialize(Transform newGuide, int newSpawnCount, BoidSettings newSettings)
    {

        GetReferences();

        flockManager = newGuide.GetComponent<SC_FlockManager>();
        //Instanciation des list de Boid et de Guide
        SC_BoidPool.Instance.GetBoid(newSpawnCount);
        _guideList = new List<Transform>();

        //Récupération du comportement initial
        _curSettings = newSettings;

        //Ajout du premier guide a la liste
        _guideList.Add(newGuide);


        //Initialisation de tout les boids
        for (int i = 0; i < newSpawnCount; i++)
        {
            Boid boid = _boidsTab[i];

            //Transform
            boid.transform.position = transform.position; //Déplacement à la position
            boid.transform.forward = Random.insideUnitSphere; //Rotation random

            //Add le boid a la Boid List
           

            //Lance l'initialisation de celui-ci avec le comportement initial et le premier guide
            boid.Initialize(_curSettings, _guideList[0]);
        }

        //Instantie le Koa
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        /////////////////////// ICI LENI POUR SPAWN KOA PREFAB
        //_koa = Instantiate(_koaPrefab);
        if(_koaPrefab != null)
        {
            _koa = NetPSpawnKoa.SpawnKoa();
            _koa.GetComponent<SC_KoaCollider>().Initialize(this);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        int index = Random.RandomRange(0, _boidsTab.Length);
        _curKoaGuide = _boidsTab[index].transform;

    }


    void Update()
    {
        if (_koa != null)
        {
            _koa.transform.position = _curKoaGuide.position;

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
        int all = _boidsTab.Length; //Total des boids
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
            _curSettings = newSettings;
        }
    }



    public void GetHit()
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

}