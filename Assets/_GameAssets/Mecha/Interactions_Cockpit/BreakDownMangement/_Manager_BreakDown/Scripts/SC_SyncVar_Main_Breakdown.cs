using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SC_SyncVar_Main_Breakdown : NetworkBehaviour
{
    #region Singleton

    private static SC_SyncVar_Main_Breakdown _instance;
    public static SC_SyncVar_Main_Breakdown Instance { get { return _instance; } }

    #endregion

    [SyncVar]
    public bool mustReboot;

    [SyncVar(hook = "onPanneDisplayChange")]
    public bool displayIsEnPanne;

    [SyncVar(hook = "onPanneWeaponChange")]
    public bool weaponIsEnPanne;

    [SyncVar(hook = "onPanneMovementChange")]
    public bool movementIsEnPanne;

    private void Awake()
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Sync Var " + mustReboot);
    }

    public void checkReboot(bool piloteReboot)
    {
        mustReboot = piloteReboot;
    }
    public void onPanneDisplayChange(bool state)
    {

        displayIsEnPanne = state;

        if (!isServer)
            SC_UI_OngletContainer.Instance.playDisplayTabAlert(state);
        
    }
    public void onPanneWeaponChange(bool state)
    {
        weaponIsEnPanne = state;

        if (!isServer)
            SC_UI_OngletContainer.Instance.playWeaponTabAlert(state);
    }

    public void onPanneMovementChange(bool state)
    {
       movementIsEnPanne = state;

        if (!isServer)
            SC_UI_OngletContainer.Instance.playMovementTabAlert(state);
    }
}
