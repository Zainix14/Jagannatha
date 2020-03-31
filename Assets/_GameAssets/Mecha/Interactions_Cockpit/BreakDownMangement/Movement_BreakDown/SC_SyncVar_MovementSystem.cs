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
    [SerializeField, SyncVar]
    SC_GameStates.GameState CurState = SC_GameStates.GameState.Lobby;

    //SC_MainBreakDownManager
    [Header("Var SC_MainBreakDownManager")]
    [SerializeField, SyncVar]
    int n_MovementLife = 0;

    //SC_MovementBreakDown
    [Header("Var SC_MovementBreakDown")]
    [SerializeField, SyncVar]
    int n_BreakDownLvl = 0;
    [SerializeField, SyncVar]
    bool b_MaxBreakdown = false;

    //SC_JoystickMove
    [Header("Var SC_JoystickMove")]
    [SerializeField, SyncVar]
    SC_JoystickMove.Dir CurDir = SC_JoystickMove.Dir.None;
    [SerializeField, SyncVar]
    SC_JoystickMove.Dir CurBrokenDir = SC_JoystickMove.Dir.Left;

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
