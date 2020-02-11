using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Le flock Manager contient les guides que suivent une nuée et éxecute les ordres de l'EnemyManager
///  | Sur le prefab Flock
///  | Auteur : Zainix
/// </summary>
public class SC_FlockManager : MonoBehaviour
{

    //---------------------------------------------------------------------//
    //---------------------------- VARIABLES ------------------------------//
    //---------------------------------------------------------------------//
    #region Variables

    //-----------PREFAB
    [SerializeField]
    GameObject _KoaPrefab;
    [SerializeField]
    GameObject _GuidePrefab;



    GameObject _Player;

    
    BoidSettings[] _BoidSettings; //Contient toute la liste des Settings de boid possible (Comportement)
    BoidSettings _curBoidSetting; //Contient le settings actuel

    FlockSettings flockSettings; //Flocksettings de la nuée (défini a la création par le waveSettings)
    GameObject _KoaManager; //Stock le Koa de la nuée
    public SC_KoaManager _SCKoaManager; //Stock le script KoaManager du Koa
    Transform _mainGuide; //Guide général que suit toujours la nuée (correspond au flock (this) mais pour des pb de lisibilité le Transform est stocké dans une varible Main Guide

   

    BezierSolution.BezierWalkerWithSpeed bezierWalker;
    SC_PathBehavior pathBehavior;

    bool inAttack;
    float DistanceGetOnPlayerSpline = 20;

    //Attack
    float attackDelay = 9f;
    float attackTimer = 0;

    //---------------------------------------------      MultiGuide Variables  (Split)   ----------------------------------------------------------//

    [HideInInspector]
    public bool _splited = false; //Booléen d'état : nuée split 
    List<Transform> _GuideList; //Permet de stocké la liste des guides lors du split
    List<Vector3> _curCurveDistanceList; //Permet de stocké la distance sur les courbes pour chaque guide lors du split

//--------------------------------------------------------------------------------------------------------------------------------------------//


    [HideInInspector]
    public bool _merged = false;  //Booléen d'état : nuée fusionnée avec une/des autre(s)

    float startAttackTimer =0;

    enum PathType
    {
        circle,
        line,
        GoingPlayer,
        AttackPlayer,
        attackCity
    }

    PathType curtype;


    #endregion
    //---------------------------------------------------------------------//


    //---------------------------------------------------------------------//
    //------------------------------- INIT --------------------------------//
    //---------------------------------------------------------------------//
    #region Init
    void Awake()
    {
        bezierWalker = GetComponent<BezierSolution.BezierWalkerWithSpeed>();
        pathBehavior = GetComponent<SC_PathBehavior>();
    }


    /// <summary>
    /// Initialisation du Flock
    /// </summary>
    public void InitializeFlock(FlockSettings newFlockSettings,float NormalizedT)
    {
        flockSettings = newFlockSettings;


        inAttack = false;
        _Player = GameObject.FindGameObjectWithTag("Player");
        transform.position = flockSettings.spawnPosition;
        _mainGuide = gameObject.transform; //Main guide prends la valeur de this (CF : Variable _mainGuide)

        _GuideList = new List<Transform>();//Instanciation de la guide list
        _curCurveDistanceList = new List<Vector3>(); // Instanciation de la list de distance sur les courbes pour chaque guide

      
        _BoidSettings = flockSettings.boidSettings;

        _KoaManager = Instantiate(_KoaPrefab, transform);//Instantiate Koa
        _SCKoaManager = _KoaManager.GetComponent<SC_KoaManager>(); //Récupère le Koa manager du koa instancié
        _SCKoaManager.Initialize(_mainGuide, flockSettings.boidSpawn,_BoidSettings[0]);//Initialise le Koa | paramètre : Guide a suivre <> Nombre de Boids a spawn <> Comportement des boids voulu


        pathBehavior.InitializePathBehavior();

        StartNewBehavior(0);

        //bezierWalker.NormalizedT = NormalizedT;


    }
    #endregion
    //---------------------------------------------------------------------//


    //---------------------------------------------------------------------//
    //----------------------------- UPDATE  -------------------------------//
    //---------------------------------------------------------------------//
    #region Update
    void Update()
    {

        AttackUpdate();

        //Si le flock est split, déplace les guides
        if (_splited)
            MultiGuideMovement();


        //Si le flock n'est pas fusionné, déplace le main guide selon la spline actuel
        if (!_merged)         
           bezierWalker.Execute(Time.deltaTime);

    
    }

    /// <summary>
    /// Guides movement when the flock is Split
    /// </summary>
    void MultiGuideMovement()
    {
    
        //Set every Guide position
        for (int i =0; i<_GuideList.Count;i++)
        {
            //Init new translate value
            float valueX = 0;
            float valueY = 0;
            float valueZ = 0;
            //Curve offset by guide
            float fguideOffset = (1 / _GuideList.Count) * (i + 1);
            Vector3 v3guideOffset =  new Vector3 (fguideOffset,fguideOffset,fguideOffset);

            //Add new dt to curv distance (x oscilliation speed)
            _curCurveDistanceList[i] += Time.deltaTime * _curBoidSetting.frequence;
            _curCurveDistanceList[i] += v3guideOffset;

            //curv distance in relation of guide number



            //Keep current curve distance values between 0 - 1
            if (_curCurveDistanceList[i].x > 1)
            {
                _curCurveDistanceList[i] = new Vector3(_curCurveDistanceList[i].x - 1, _curCurveDistanceList[i].y, _curCurveDistanceList[i].z);
            }
            if (_curCurveDistanceList[i].y > 1)
            {
                _curCurveDistanceList[i] = new Vector3(_curCurveDistanceList[i].x, _curCurveDistanceList[i].y - 1, _curCurveDistanceList[i].z);
            }
            if (_curCurveDistanceList[i].z > 1)
            {
                _curCurveDistanceList[i] = new Vector3(_curCurveDistanceList[i].x, _curCurveDistanceList[i].y, _curCurveDistanceList[i].z - 1);
            }
 

            //Get Z and Y translate value on the curve
            valueX = _curBoidSetting.curveX.Evaluate(_curCurveDistanceList[i].x) * _curBoidSetting.amplitude.x;
            valueY = _curBoidSetting.curveY.Evaluate(_curCurveDistanceList[i].y) * _curBoidSetting.amplitude.y;
            valueZ = _curBoidSetting.curveZ.Evaluate(_curCurveDistanceList[i].z) * _curBoidSetting.amplitude.z;

            if(_curBoidSetting.invert && i%2 == 1)
            {
                valueX -= (valueX * 2) * _curBoidSetting.invertAxis.x;
                valueY -= (valueY * 2) * _curBoidSetting.invertAxis.y;
                valueZ -= (valueZ * 2) * _curBoidSetting.invertAxis.z;
               
            }

            //Add the offset value
            _GuideList[i].transform.localPosition =new Vector3(valueX , valueY, valueZ);
        }

        
    }


    void AttackUpdate()
    {

        if(inAttack == false) startAttackTimer += Time.deltaTime;

        if(startAttackTimer >= flockSettings.timeBeforeAttack )
        {
            if (flockSettings.attackCity)
            {
                StartNewPath(PathType.attackCity);
                
            }
            else if (flockSettings.attackPlayer)
            {

                StartNewPath(PathType.GoingPlayer);
            }
            startAttackTimer = 0;
        }

        if(inAttack && curtype == PathType.GoingPlayer)
        {

            transform.position = Vector3.Lerp(transform.position, _Player.transform.position, Time.deltaTime * flockSettings.speedToPlayer);

            //check les distance entre le flock et le player
            float dist;
            dist = Vector3.Distance(transform.position, _Player.transform.position);



            //Si la distance en inférieure a la distance minimale requise
            if (dist < DistanceGetOnPlayerSpline)
            {

                //Change de spline pour passer sur la spline Cercle
                StartNewPath(PathType.AttackPlayer);
            }
        }
        if(inAttack && curtype == PathType.AttackPlayer)
        {
            attackTimer += Time.deltaTime;
            if(attackTimer >= attackDelay)
            {
                attackTimer = 0;
                SC_BreakdownTestManager.Instance.StartNewBreakdown(1);
            }
        }

    }
    #endregion
    //---------------------------------------------------------------------//



    //---------------------------------------------------------------------//
    //---------------------------- UTILITIES ------------------------------//
    //---------------------------------------------------------------------//
    #region Utilities

    void StartNewPath(PathType pathType)
    {
        curtype = pathType;
        switch (pathType)
        {
            case PathType.circle:
                inAttack = false;
                StartNewBehavior(0);
                break;

            case PathType.attackCity:
                inAttack = true;
                StartNewBehavior(1);
                break;

            case PathType.GoingPlayer:
                inAttack = true;
                StartNewBehavior(1);
                pathBehavior.OnStopPath();

                break;
            case PathType.AttackPlayer:
                StartNewBehavior(2);
                pathBehavior.OnAttackPlayer(flockSettings.attackDuration);
                break;
        }

    }



    public void StartNewBehavior(int behaviorIndex)
    {
        _curBoidSetting = _BoidSettings[behaviorIndex];
        bezierWalker.speed = flockSettings.speedOnSpline;
        if(flockSettings.speedOnSpline != 0 && !inAttack)
        {
            pathBehavior.GetOnRandomSpline();
        }
        Reassemble();
        if (_curBoidSetting.split)
        {
            SplitDivision(_curBoidSetting.splitNumber);
        }
        _SCKoaManager.SetBehavior(_curBoidSetting);


    }


    /// <summary>
    /// Start the Flock split [Param : (int)Split Number]
    /// </summary>
    /// <param name="splitNumber"></param>
    /// 
    void SplitDivision(int splitNumber)
    {
        //Register old guides to destroy after creating new ones
        List<Transform> _oldGuideList = new List<Transform>();
        foreach (Transform guide in _GuideList)
        {
            if (!GameObject.Equals(guide, transform))
            {
                _oldGuideList.Add(guide);
            }
        }

        //Set splited to true
        _splited = true;

        //Clear current guide list
        _GuideList.Clear();

        //Clear Curve distance value List for multi guides
        _curCurveDistanceList.Clear();

        //Set new values link to split number
        for (int i = 0; i < splitNumber; i++)
        {
            //Create new guides 
            Transform curGuide = Instantiate<GameObject>(_GuidePrefab).transform;
            //Add them to guide list
            _GuideList.Add(curGuide);
            curGuide.SetParent(this.transform);

            //Calculate Offset for Curve distance
            float offset = (1f / splitNumber) * (i);
            //Register curve offset distance to Curve distance list
            _curCurveDistanceList.Add(new Vector3(offset, offset, offset));


        }

        _SCKoaManager.Split(_GuideList);

        //Destroy old useless guides
        foreach (Transform guide in _oldGuideList)
        {
            Destroy(guide.gameObject);
        }

    }

    void Reassemble()
    {
        foreach (Transform guide in _GuideList)
        {
            if (!GameObject.Equals(guide, transform))
                Destroy(guide.gameObject);
        }
        //Set splited to false
        _splited = false;

        //Clear current guide list
        _GuideList.Clear();

        //Clear Curve distance value List for multi guides
        _curCurveDistanceList.Clear();

        //Add main guide to guide list
        _GuideList.Add(_mainGuide);

        _SCKoaManager.Split(_GuideList);
    }

    /// <summary>
    /// Return true si chemin par Ligne(Rapide) |  False si chemin par Cercle(Lent)
    /// </summary>
    /// <returns></returns>
    public int GetPathPreference()
    {
        return (int)flockSettings.pathPreference;
    }

    public void DestroyFlock()
    {
        SC_WaveManager.Instance.FlockDestroyed(this.gameObject);
        Destroy(this.gameObject);
    }

    public void EndAttack()
    {
        StartNewPath(PathType.circle);
    }
    #endregion
    //---------------------------------------------------------------------//

}
