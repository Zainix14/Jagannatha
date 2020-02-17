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

    BezierSolution.BezierSpline[] _splineTab;
    BezierSolution.BezierSpline _curSpline;

    GameObject _Player;

    
    BoidSettings[] _BoidSettings; //Contient toute la liste des Settings de boid possible (Comportement)
    BoidSettings _curBoidSetting; //Contient le settings actuel

    FlockSettings flockSettings; //Flocksettings de la nuée (défini a la création par le waveSettings)
    GameObject _KoaManager; //Stock le Koa de la nuée
    public SC_KoaManager _SCKoaManager; //Stock le script KoaManager du Koa
    Transform _mainGuide; //Guide général que suit toujours la nuée (correspond au flock (this) mais pour des pb de lisibilité le Transform est stocké dans une varible Main Guide
    
   

    BezierSolution.BezierWalkerWithSpeed bezierWalker;
    //SC_PathBehavior pathBehavior;
    SC_FlockWeaponManager flockWeaponManager;

    bool inAttack;
    bool isActive;

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
        Roam,
        line,
        AttackPlayer,
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
        //pathBehavior = GetComponent<SC_PathBehavior>();
        flockWeaponManager = GetComponent<SC_FlockWeaponManager>();
    }


    /// <summary>
    /// Initialisation du Flock
    /// </summary>
    public void InitializeFlock(FlockSettings newFlockSettings,float NormalizedT)
    {
        flockSettings = newFlockSettings;


        inAttack = false;
        _Player = GameObject.FindGameObjectWithTag("Player");
        transform.position = new Vector3(0,50,100);
        _mainGuide = gameObject.transform; //Main guide prends la valeur de this (CF : Variable _mainGuide)

        _GuideList = new List<Transform>();//Instanciation de la guide list
        _curCurveDistanceList = new List<Vector3>(); // Instanciation de la list de distance sur les courbes pour chaque guide

      
        _BoidSettings = flockSettings.boidSettings;

        _KoaManager = Instantiate(_KoaPrefab, transform);//Instantiate Koa
        _SCKoaManager = _KoaManager.GetComponent<SC_KoaManager>(); //Récupère le Koa manager du koa instancié
        _SCKoaManager.Initialize(_mainGuide, flockSettings.boidSpawn,_BoidSettings[0],newFlockSettings);//Initialise le Koa | paramètre : Guide a suivre <> Nombre de Boids a spawn <> Comportement des boids voulu
        flockWeaponManager.Initialize(flockSettings);

        _splineTab = new BezierSolution.BezierSpline[_BoidSettings.Length];
        for (int i = 0; i < _BoidSettings.Length; i++)
        {
            if (_BoidSettings[i].spline != null)
                _splineTab[i] = Instantiate(_BoidSettings[i].spline);
        }

        Invoke("ActivateFlock", flockSettings.spawnTimer);
        
    }
    #endregion
    //---------------------------------------------------------------------//


    //---------------------------------------------------------------------//
    //----------------------------- UPDATE  -------------------------------//
    //---------------------------------------------------------------------//
    #region Update
    void Update()
    {

        if(isActive)
        {
            AttackUpdate();

            //Si le flock est split, déplace les guides
            if (_splited)
                MultiGuideMovement();


            //Si le flock n'est pas fusionné, déplace le main guide selon la spline actuel       
            bezierWalker.Execute(Time.deltaTime);

        }    
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

        if (flockSettings.attackType != FlockSettings.AttackType.none)
        {
            if (inAttack == false) startAttackTimer += Time.deltaTime;

            if (startAttackTimer >= flockSettings.timeBetweenAttacks)
            {
                inAttack = true;
                StartNewPath(PathType.AttackPlayer);
                startAttackTimer = 0;
            }
            if (inAttack)
            {
                transform.LookAt(_Player.transform);
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
            case PathType.Roam:
                StartNewBehavior(0);
                break;


            case PathType.AttackPlayer:

                StartNewBehavior(1);
                flockWeaponManager.StartFire();
                break;
        }

    }

    void ActivateFlock()
    {
        isActive = true;
        _SCKoaManager.ActivateKoa();
    }


    public void StartNewBehavior(int behaviorIndex)
    {
        _curBoidSetting = _BoidSettings[behaviorIndex];
        _curSpline = _splineTab[behaviorIndex];
        bezierWalker.speed = _curBoidSetting.speedOnSpline;

        if (_curSpline != null)
        {
            _curSpline.transform.position = transform.position;
            bezierWalker.SetNewSpline(_curSpline);
        }
        else
        {
            bezierWalker.speed = 0;
        }
        Reassemble();
        if (_curBoidSetting.split)
        {
            SplitDivision(_curBoidSetting.splitNumber);
        }
        _SCKoaManager.SetBehavior(_curBoidSetting);
        //https://www.youtube.com/watch?v=bOZT-UpRA2Y

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



    public void DestroyFlock()
    {
        GetComponent<SC_FlockWeaponManager>().DestroyFx();
        SC_WaveManager.Instance.FlockDestroyed(this.gameObject);
        Destroy(this.gameObject);
    }

    public void EndAttack()
    {
        inAttack = false;
        StartNewPath(PathType.Roam);
    }

    #endregion
    //---------------------------------------------------------------------//

}
