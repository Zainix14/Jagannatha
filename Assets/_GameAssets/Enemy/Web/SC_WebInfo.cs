using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script permettant de stocker toutes les splines de déplacement afin de les récupérer dans PathBehavior
///  | Sur le prefab Web (Empty conetant toutes les splines trier par catégorie en enfant)
///  | Auteur : Zainix
/// </summary>
public class SC_WebInfo : MonoBehaviour
{

    #region Singleton

    private static SC_WebInfo _instance;
    public static SC_WebInfo Instance { get { return _instance; } }

    #endregion
    //Tableaux stockant toutes les splines de déplacement par catégorie
    BezierSolution.BezierSpline[] _TabCircle; //Stock les Circle Spline
    BezierSolution.BezierSpline[] _TabLine; //Stock les Line Spline
    BezierSolution.BezierSpline[] _TabAttackPath;//stock les AttackPath Spline


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
        //Récupère les enfants contenant chacun en enfant l'intégralité des splines d'un type 
        GameObject circleParent = transform.GetChild(0).gameObject; //Parent des Cercles
        GameObject lineParent = transform.GetChild(1).gameObject; //Parent des Lines
        GameObject AttackPathParent = transform.GetChild(2).gameObject; //Parent des attack Paths


        //Instantiacion des tableaux selon le nombre d'enfants de chaque type
        _TabCircle = new BezierSolution.BezierSpline[circleParent.transform.childCount];
        _TabLine = new BezierSolution.BezierSpline[lineParent.transform.childCount];
        _TabAttackPath = new BezierSolution.BezierSpline[AttackPathParent.transform.childCount];


        //Stockage de toutes les splines par type dans chaque tableaux
        for (int i =0; i<circleParent.transform.childCount; i++)
        {
            _TabCircle[i] = circleParent.transform.GetChild(i).GetComponent<BezierSolution.BezierSpline>();
        }

        for (int i =0; i< lineParent.transform.childCount; i++)
        {
            _TabLine[i] = lineParent.transform.GetChild(i).GetComponent<BezierSolution.BezierSpline>();
        }

        for (int i =0; i< AttackPathParent.transform.childCount; i++)
        {
            _TabAttackPath[i] = AttackPathParent.transform.GetChild(i).GetComponent<BezierSolution.BezierSpline>();
        }

    }

    //Fonction Get retournant les tableaux par type;
    public BezierSolution.BezierSpline[] GetCircleTab()
    {
        return _TabCircle;
    }
    public BezierSolution.BezierSpline[] GetLineTab()
    {
        return _TabLine;
    }
    public BezierSolution.BezierSpline[] GetAttackPathTab()
    {
        return _TabAttackPath;
    }

}
