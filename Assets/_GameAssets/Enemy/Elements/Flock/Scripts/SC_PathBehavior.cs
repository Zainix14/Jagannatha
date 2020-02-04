using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Le Pathbehavior permet de gerer le choix de spline que suit une nuée en fonction du comportement ou de la direction voulue
///  | Sur le prefab Flock
///  | Auteur : Zainix
/// </summary>
public class SC_PathBehavior : MonoBehaviour
{


    //---------------------------------------------------------------------//
    //---------------------------- VARIABLES ------------------------------//
    //---------------------------------------------------------------------//
    #region Variables
    //----------------------  STOCKAGE DES SPLINES -------------------------//

    /* Le réseau de spline est composée de trois types de spline 
            - Cercle : Spline qui loop et qui est connectée à toutes les Lignes
            - Ligne  : Spline ouverte et qui est connectée à tout les Cercles
            - Attack : Spline partant d'un point de connection entre un cercle et une ligne et qui amène a un point d'attaque
    */
    BezierSolution.BezierSpline[] _circleSplineTab; //Contient toutes les splines "Cercle"
    BezierSolution.BezierSpline[] _lineSplineTab; //Contient toutes les splines "Ligne"
    BezierSolution.BezierSpline[] _attackSplineTab; //Contient toutes les splines "Attack" 

    BezierSolution.BezierSpline curSpline;//Contient la spline actuel sur laquelle est le flock


    //------------------------  STOCKAGE DES POINTS  ----------------------//

    Transform[,] _IntersectionTab; // Le tableau Intersection contient tout les Transform des points d'intersections entre ces deux types de Splines avec en entrée  [Index du cercle , Index des Ligne qu'il croise]    

    int curCircleIndex; //Index du Circle sur lequel est le flock
    int newCircleIndex; //Index du Circle vers lequel le flock doit aller

    int newAttackPathIndex; //Index de l'AttackPath sur lequel est le flock
    int curAttackPathIndex; //Index de l'AttackPath vers lequel le flock doit aller

    int curLineIndex; //Index de la ligne sur lequel est le flock
    int newLineIndex; //Index de la ligne vers lequel le flock doit aller

    bool isAttacking; //Le flock se dirige vers une spline d'attack
    int PathPreference; //0 si le flock priorise le passage par les lignes lors d'une attaque (plus rapide), 1 si c'est les cercles qui sont priorisé


    float curAttackDuration;
    float totalAttackDuration;

    [SerializeField]
    float DistanceChangeSpline; //Distance à partir de laquelle le flock change de spline lorsqu'il se rapporche d'une intersection a prendre

    //------------------------  STOCKAGE DES POINTS  ----------------------//
    


    //Tout les types de splines possible 
    enum PathType
    {
        Circle,
        Line,
        AttackPath,
        PlayerPath
    }

    PathType _curPathType; //Type de spline surlequel le flock se déplace actuellement

    BezierSolution.BezierWalkerWithSpeed SC_bezier;//Contient le script BezierWalker du flock permettant d'envoyer les informations de changement de spline

    #endregion
    /////////////////////////////////////////////////////////////////////////



    //---------------------------------------------------------------------//
    //-------------------------- INITIALISATION ---------------------------//
    //---------------------------------------------------------------------//
    #region Initilisation
    public void InitializePathBehavior()
    {
        
        SC_bezier = GetComponent<BezierSolution.BezierWalkerWithSpeed>(); //Récupère le script du flock
        PathPreference = GetComponent<SC_FlockManager>().GetPathPreference();//Récupère le Path préference du flock (dans le flock manager)

        InitTab(); //initialise tout les tableaux


        //DEBUG
        GetOnCircleSpline(0); //Premier comportement quand la nuée est spawn
        curCircleIndex =0;
        newCircleIndex =0;
    }

    /// <summary>
    /// Initialise tout les tableaux de spline et de point d'intersection 
    /// </summary>
    void InitTab()
    {
        
        // Récupère les tableaux de spline depuis le WebInfo
        _circleSplineTab = GameObject.FindGameObjectWithTag("Web").GetComponent<SC_WebInfo>().GetCircleTab();
        _lineSplineTab = GameObject.FindGameObjectWithTag("Web").GetComponent<SC_WebInfo>().GetLineTab();
        _attackSplineTab = GameObject.FindGameObjectWithTag("Web").GetComponent<SC_WebInfo>().GetAttackPathTab();


        //Instancie le tableau d'interesection et lui attribu les valeurs
        _IntersectionTab = new Transform[_circleSplineTab.Length, _lineSplineTab.Length];

        for(int i = 0; i < _circleSplineTab.Length; i++)
        {
            for(int j =0; j< _lineSplineTab.Length-1;j++)
            {
                _IntersectionTab[i, j] = _circleSplineTab[i].transform.GetChild(j);
            }
        }

    }
    #endregion
    /////////////////////////////////////////////////////////////////////////



    //---------------------------------------------------------------------//
    //---------------------------- PATH UPDATE ----------------------------//
    //---------------------------------------------------------------------//
    #region Path Update

    void Update()
    {
 


        //Update des condition de changement de spline en fonction de la spline actuel 
        switch (_curPathType)
        {
            case PathType.Line:
                PathUpdateLine();
                break;

            case PathType.Circle:
                PathUpdateCircle();
                break;

            case PathType.AttackPath:
                //PathUpdatePlayerAttack();
                break;

            case PathType.PlayerPath:
                PathUpdatePlayerAttack();
                break;
        }


    }
    

    /// <summary>
    /// Update lorsque la spline suivi est un cercle, permet de check les conditions de sortie
    /// </summary>
    void PathUpdateCircle()
    {
        //Si le cercle actuel ne correspond pas au cercle voulue
        if (curCircleIndex != newCircleIndex)
        {

            int nextIndex; //Correspondra à l'index de la line de sortie 
            float dist; //Correspondra à la distance entre le flock et l'intersection de sortie

            BezierSolution.BezierSpline.PointIndexTuple indexTuple; //variable qui stock l'index des intersections suivante et précedente sur la spline 

            indexTuple = curSpline.GetNearestPointIndicesTo(SC_bezier.NormalizedT); //Récupère les index en fonction de la position actuel du flock


            //Si le Flock est en mode attaque et que le Path Preference est Line
            if (isAttacking && PathPreference == 0)
            {
                //L'index de sortie est l'index correspondant a la line sur lequel le path d'attaque est situé
                nextIndex = newLineIndex;
            }
            //Sinon
            else if (SC_bezier.MovingForward)
            {
                //Si le flock est en rotation Horraire recupère l'index d'interessection suivant
                nextIndex = indexTuple.index2;
            }
            else
            { 
                //Sinon récupère le précédent
                nextIndex = indexTuple.index1;
            }

            //Calcul la distance actuel entre le flock et l'intersection
            dist = Vector3.Distance(_IntersectionTab[curCircleIndex, nextIndex].position, transform.position);


            //Si la distance en inférieure a la distance minimale requise
            if (dist < DistanceChangeSpline)
            {
                //Change vers la spline Line et se déplace vers le prochain cercle
                if (curCircleIndex > newCircleIndex)
                {
                    SC_bezier.MovingForward = false;
                }
                else
                {
                    SC_bezier.MovingForward = true;
                }

                GetOnLineSpline(nextIndex);
            }
        }

        /*
        //Si le flock doit aller sur une spline d'attaque et qu'il est sur le bon index de cercle où ce situe l'entrée de la spline
        else if(isAttacking)
        {
            //check les distance entre le flock et le point d'entrée de la spline
            float dist;
            dist = Vector3.Distance(_IntersectionTab[curCircleIndex, newLineIndex].position, transform.position);


            //Si la distance en inférieure a la distance minimale requise
            if (dist < DistanceChangeSpline)
            {
                //Change de spline pour passer sur la spline Attaque
                SC_bezier.MovingForward = true;
                GetOnAttackSpline(newAttackPathIndex);
            }
        }*/
    }

    /// <summary>
    /// Update lorsque la spline suivi est une Line, permet de check les conditions de sortie
    /// </summary>
    void PathUpdateLine()
    {
        //check les distance entre le flock et le point d'entrée de la spline
        float dist;
        dist = Vector3.Distance(_IntersectionTab[newCircleIndex, curLineIndex].position, transform.position);

        //Si la distance en inférieure a la distance minimale requise
        if (dist < DistanceChangeSpline)
        {
            //Change de spline pour passer sur la spline Cercle
            GetOnCircleSpline(newCircleIndex);
        }
        
    }


    /// <summary>
    /// Update lorsque la spline suivi est une Attack, permet de check les conditions de sortie
    /// </summary>
    void PathUpdatePlayerAttack()
    {
        curAttackDuration += Time.deltaTime;
        if(curAttackDuration >= totalAttackDuration)
        {
            GetOnCircleSpline(0);
        }
    }

    #endregion
    /////////////////////////////////////////////////////////////////////////



    //---------------------------------------------------------------------//
    //--------------------------- CHANGE SPLINE ---------------------------//
    //---------------------------------------------------------------------//
    #region Change Spline
    /*Quand les conditions sont remplis, change la spline que suis le Main guide du Flock
            - Nouveau Path type défini
            - Enregistrement de la spline actuel
            - Changement du Type de comportement sur la spline
            - Transfert l'information de la nouvelle spline au BezierWalker (gère les déplacements sur la spline)
            - Actualise l'index du type de spline actuel    
    */
    void GetOnCircleSpline(int splineIndex)
    {
        _curPathType = PathType.Circle;
        curSpline = _circleSplineTab[splineIndex];

        SC_bezier.travelMode = BezierSolution.TravelMode.Loop;
        SC_bezier.SetNewSpline(curSpline);

        curCircleIndex = splineIndex;


    }
    void GetOnLineSpline(int splineIndex)
    {

        _curPathType = PathType.Line;
        curSpline = _lineSplineTab[splineIndex];

        SC_bezier.travelMode = BezierSolution.TravelMode.PingPong;
        SC_bezier.SetNewSpline(curSpline);

        curLineIndex = splineIndex;
    }

    void GetOnAttackSpline(int splineIndex)
    {
        _curPathType = PathType.AttackPath;
        curSpline = _attackSplineTab[splineIndex];

        SC_bezier.travelMode = BezierSolution.TravelMode.PingPong;
        SC_bezier.SetNewSpline(curSpline);

    }

    void GetOnPlayerSpline(int splineIndex)
    {
        _curPathType = PathType.PlayerPath;
        SC_bezier.travelMode = BezierSolution.TravelMode.Loop;
        curSpline = _attackSplineTab[splineIndex];
        SC_bezier.SetNewSpline(curSpline);
    }


    #endregion
    /////////////////////////////////////////////////////////////////////////



    //---------------------------------------------------------------------//
    //--------------------- CALLED FROM FLOCK MANAGER ---------------------//
    //---------------------------------------------------------------------//
    #region CALLED FROM FLOCK MANAGER
    /// <summary>
    /// Ordonne au flock de changer de cercle (spline) | Param : index du nouveau cercle
    /// </summary>
    /// <param name="newCircleIndex"></param>
    public void ChangeCircle(int newCircleIndex)
    {

        this.newCircleIndex = newCircleIndex;

    }

    /// <summary>
    /// Ordonne au flock d'aller sur une spline d'attaque | Param : index de la spline d'attaque
    /// </summary>
    /// <param name="newAttackPathIndex"></param>
    public void OnAttack(int newAttackPathIndex)
    {

        this.newAttackPathIndex = newAttackPathIndex;


        //Récupère l'index du cercle depuis lequel la spline d'attaque démarre
        int AttackCircleIndex = _attackSplineTab[newAttackPathIndex].GetComponent<SC_AttackPathInfo>().GetCircleEnterIndex();
        //Récupère l'index de la ligne depuis lequel la spline d'attaque démarre
        newLineIndex = _attackSplineTab[newAttackPathIndex].GetComponent<SC_AttackPathInfo>().GetLineEnterIndex();


        //Ordonne le changement de cercle avec l'index récuperer
        ChangeCircle(AttackCircleIndex);

        //Passe en mode Attaque
        isAttacking = true;
    }

    public void OnAttackPlayer(float attackDuration)
    {
        isAttacking = true;
        curAttackDuration = 0;
        totalAttackDuration = attackDuration;
        GetOnPlayerSpline(0);

    }

    public void OnStopPath()
    {
        curSpline = null;
        SC_bezier.SetNewSpline(null);
    }
    /////////////////////////////////////////////////////////////////////////
    #endregion
    /////////////////////////////////////////////////////////////////////////

}
