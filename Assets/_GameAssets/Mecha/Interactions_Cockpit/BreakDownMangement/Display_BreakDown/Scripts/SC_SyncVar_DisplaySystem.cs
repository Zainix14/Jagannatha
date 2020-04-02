using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SC_SyncVar_DisplaySystem : NetworkBehaviour
{

    #region Singleton

    private static SC_SyncVar_DisplaySystem _instance;
    public static SC_SyncVar_DisplaySystem Instance { get { return _instance; } }

    #endregion

    //SC_GameStates
    [Header("Var SC_GameStates")]
    [SyncVar(hook = "OnChangeGameState")]
    public SC_GameStates.GameState CurState = SC_GameStates.GameState.Lobby;

    [Header("Var SC_main_breakdown_validation")]
    [SyncVar(hook = "OnLaunch")]
    public bool b_IsLaunch = false;

    //SC_MainBreakDownManager
    [Header("Var SC_MainBreakDownManager")]
    [SyncVar]
    public float f_Displaylife = 0;
    [SyncVar(hook = "OnChangeBreakEngine")]
    public bool b_BreakEngine = false;

    [Header("Var SC_BreakdownDisplayManager")]
    [SyncVar(hook = "OnChangeNbOfBd")]
    public float f_CurNbOfBd = 0;

   

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

    void OnChangeGameState(SC_GameStates.GameState _CurState)
    {
        CurState = _CurState;
        if (!isServer)
            UpdateOnClient();
    }

    void OnChangeBreakEngine(bool Breakdown)
    {
        b_BreakEngine = Breakdown;
        if (!isServer)
            UpdateOnClient();
    }
    void OnChangeNbOfBd(float Target)
    {
        f_CurNbOfBd = Target;
        if (!isServer)
            UpdateOnClient();
    }

    void UpdateOnClient()
    {
        SC_Display_MechState.Instance.UpdateVar();
    }

}
