using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script gerant l'enchainement des Phases
///  | Sur le prefab EnemyManager(à instantié une fois)
///  | Auteur : Zainix
/// </summary>
public class SC_EnemyManager : MonoBehaviour
{

    #region Singleton

    private static SC_EnemyManager _instance;
    public static SC_EnemyManager Instance { get { return _instance; } }

    #endregion

    public PhaseSettings[] phases;
  
    int curPhaseIndex;
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

    void Start()
    {
        Invoke("Initialize", 1);
    }

    public void Initialize()
    {
        InitNewPhase(0);
    }

    public void InitNewPhase(int phaseIndex)
    {
        SC_PhaseManager.Instance.Initialize(phases[phaseIndex]);
    }


    public void EndPhase()
    {
        curPhaseIndex++;
        InitNewPhase(curPhaseIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
