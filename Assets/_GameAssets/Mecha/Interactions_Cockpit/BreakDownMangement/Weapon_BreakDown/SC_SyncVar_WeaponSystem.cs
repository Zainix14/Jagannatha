using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SC_SyncVar_WeaponSystem : NetworkBehaviour
{

    #region Singleton

    private static SC_SyncVar_WeaponSystem _instance;
    public static SC_SyncVar_WeaponSystem Instance { get { return _instance; } }

    #endregion

    //SC_GameStates
    [Header("Var SC_GameStates")]
    [SerializeField, SyncVar]
    SC_GameStates.GameState CurState = SC_GameStates.GameState.Lobby;

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
