﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SC_SyncVar_MovementSystem : NetworkBehaviour
{

    #region Singleton

    private static SC_SyncVar_MovementSystem _instance;
    public static SC_SyncVar_MovementSystem Instance { get { return _instance; } }

    #endregion

    #region Var SC_GameStates

    [Header("Var SC_GameStates")]
    [SyncVar(hook = "OnChangeGameState")]
    public SC_GameStates.GameState CurState = SC_GameStates.GameState.Lobby;

    void OnChangeGameState(SC_GameStates.GameState _CurState)
    {
        CurState = _CurState;
        UpdateOnClient();
    }

    #endregion SC_GameStates

    #region Var SC_main_breakdown_validation

    [Header("Var SC_main_breakdown_validation")]
    [SyncVar(hook = "OnLaunch")]
    public bool b_IsLaunch = false;

    void OnLaunch(bool TargetBool)
    {
        b_IsLaunch = TargetBool;
        UpdateOnClient();
    }

    #endregion Var SC_main_breakdown_validation

    #region Var SC_MainBreakDownManager

    [Header("Var SC_MainBreakDownManager")]
    [SyncVar(hook = "OnChangeSystemLife")]
    public float f_MovementLife = 0;
    [SyncVar(hook = "OnChangeBreakEngine")]
    public bool b_BreakEngine = false;

    void OnChangeSystemLife(float newLife)
    {
        f_MovementLife = newLife;
        UpdateOnClient();
    }

    void OnChangeBreakEngine(bool Breakdown)
    {
        b_BreakEngine = Breakdown;
        UpdateOnClient();
    }

    #endregion Var SC_MainBreakDownManager

    #region Var SC_MovementBreakDown

    [Header("Var SC_MovementBreakDown")]
    [SyncVar(hook = "OnChangeBdLvl")]
    public int n_BreakDownLvl = 0;
    [SyncVar(hook = "OnChangeMaxBd")]
    public bool b_MaxBreakdown = false;
    [SyncVar(hook = "OnCheckSequence")]
    public bool b_SeqIsCorrect = false;
    [SyncVar(hook = "OnSeqSync")]
    public bool b_SeqIsSync = false;
    public SyncListInt BreakdownList = new SyncListInt();

    void OnChangeBdLvl(int Target)
    {
        n_BreakDownLvl = Target;
        UpdateOnClient();
    }

    void OnChangeMaxBd(bool Target)
    {
        b_MaxBreakdown = Target;
        UpdateOnClient();
    }

    void OnCheckSequence(bool Target)
    {
        b_SeqIsCorrect = Target;
        UpdateOnClient();
    }

    void OnSeqSync(bool Target)
    {
        b_SeqIsSync = Target;
        //Debug.Log("b_SeqIsSync = " + b_SeqIsSync);
        UpdateOnClient();
    }

    public override void OnStartClient()
    {
        BreakdownList.Callback = OnSequenceChanged;
    }

    private void OnSequenceChanged(SyncListInt.Operation op, int index)
    {
        //Debug.Log("SyncListChange");
    }

    #endregion Var SC_MovementBreakDown

    #region Var SC_JoystickMove

    [Header("Var SC_JoystickMove")]
    [SyncVar(hook = "OnChangeDir")]
    public SC_JoystickMove.Dir CurDir = SC_JoystickMove.Dir.None;
    [SyncVar(hook = "OnChangeBrokenDir")]
    public SC_JoystickMove.Dir CurBrokenDir = SC_JoystickMove.Dir.Left;
    [SyncVar]
    public SC_JoystickMove.Dir CoroDir = SC_JoystickMove.Dir.None;

    void OnChangeDir(SC_JoystickMove.Dir TargetDir)
    {
        CurDir = TargetDir;
        UpdateOnClient();
    }

    void OnChangeBrokenDir(SC_JoystickMove.Dir TargetDir)
    {
        CurBrokenDir = TargetDir;
        UpdateOnClient();
    }

    #endregion Var SC_JoystickMove

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

    void UpdateOnClient()
    {
        if (!isServer)
        {

            SC_Movement_MechState.Instance.UpdateVar();

            //Debug.Log("UpdateOnClient b_SeqIsSync " + b_SeqIsSync);

            if (b_SeqIsSync)
            {
                //Debug.Log("UpdateOnClient b_SeqIsSync IN");
                SC_ShowSequence_OP.Instance.DisplaySequence();
            }
                

        }
            
    }

}
