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
    [SyncVar]
    public SC_GameStates.GameState CurState = SC_GameStates.GameState.Lobby;

    //SC_MainBreakDownManager
    [Header("Var SC_MainBreakDownManager")]
    [SyncVar]
    public float f_WeaponLife = 0;
    [SyncVar]
    public bool b_BreakEngine = false;

    //SC_slider_calibr
    [Header("Var SC_slider_calibr")]
    [SyncVar]
    public float f_AmplitudeCalib = 0;
    [SyncVar]
    public float f_FrequenceCalib = 0;
    [SyncVar]
    public float f_PhaseCalib = 0;


    //CuurentTarget
    [SyncVar]
    public string s_KoaID = "";


    //Status
    //WeaNrjLevel
    [SyncVar]
    public float f_curEnergyLevel;


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
