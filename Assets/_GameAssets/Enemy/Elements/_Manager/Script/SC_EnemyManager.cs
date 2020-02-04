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
    public PhaseSettings[] phases;
    SC_PhaseManager phaseManager;
    int curPhaseIndex;

    void Start()
    {
        phaseManager = GetComponent<SC_PhaseManager>();
        Invoke("Initialize", 1);
    }

    public void Initialize()
    {
        InitNewPhase(0);
    }

    public void InitNewPhase(int phaseIndex)
    {
        phaseManager.Initialize(phases[phaseIndex]);
    }


    public void EndPhase()
    {
        curPhaseIndex++;
        InitNewPhase(curPhaseIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
           
        }
        
    }
}
