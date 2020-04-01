using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SC_SyncVar_MovementSystem : NetworkBehaviour
{

    #region Singleton

    private static SC_SyncVar_MovementSystem _instance;
    public static SC_SyncVar_MovementSystem Instance { get { return _instance; } }

    #endregion

    //SC_GameStates
    [Header("Var SC_GameStates")]
    [SyncVar]
    public SC_GameStates.GameState CurState = SC_GameStates.GameState.Lobby;

    //SC_MainBreakDownManager
    [Header("Var SC_MainBreakDownManager")]
    [SyncVar]
    public float f_MovementLife = 0;
    [SyncVar]
    public bool b_BreakEngine = false;

    //SC_MovementBreakDown
    [Header("Var SC_MovementBreakDown")]
    [SyncVar]
    public int n_BreakDownLvl = 0;
    [SyncVar]
    public bool b_MaxBreakdown = false;

    //SC_JoystickMove
    [Header("Var SC_JoystickMove")]
    [SyncVar]
    public SC_JoystickMove.Dir CurDir = SC_JoystickMove.Dir.None;
    [SyncVar]
    public SC_JoystickMove.Dir CurBrokenDir = SC_JoystickMove.Dir.Left;

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

}
