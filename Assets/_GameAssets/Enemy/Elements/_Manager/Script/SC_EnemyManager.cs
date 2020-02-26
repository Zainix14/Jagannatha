﻿using System.Collections;
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

 
    public void Initialize()
    {
        InitNewPhase(0);
    }

    public void InitNewPhase(int phaseIndex)
    {
        SC_PhaseManager.Instance.Initialize(phases[phaseIndex]);

        if (phaseIndex == 1)
        {
            SC_GameStates.Instance.RpcSetState(SC_GameStates.GameState.Game);
        }
    }


    public void EndPhase()
    {
        curPhaseIndex++;
        if(curPhaseIndex >= phases.Length)
        {
            SC_breakdown_displays_screens.Instance.EndScreenDisplay();
        }
        InitNewPhase(curPhaseIndex);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            SC_breakdown_displays_screens.Instance.EndScreenDisplay();
        }
    }
}
