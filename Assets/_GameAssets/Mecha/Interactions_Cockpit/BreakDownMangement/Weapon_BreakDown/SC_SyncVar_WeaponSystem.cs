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

    /*
    [Header("Var SC_main_breakdown_validation")]
    [SyncVar(hook = "OnLaunch")]
    public bool b_IsLaunch = false;
    */

    //SC_MainBreakDownManager
    [Header("Var SC_MainBreakDownManager")]
    [SyncVar]
    public float f_WeaponLife = 0;
    [SyncVar]
    public bool b_BreakEngine = false;

    /*
    [Header("Var SC_BreakdownWeaponManager")]
    [SyncVar(hook = "OnChangeNbOfBd")]
    public float f_CurNbOfBd = 0;
    */

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
