using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Display_MechState : MonoBehaviour
{
    #region Singleton

    private static SC_Display_MechState _instance;
    public static SC_Display_MechState Instance { get { return _instance; } }

    #endregion

    SC_SyncVar_DisplaySystem sc_syncvar_DisplaySystem;
    SC_SyncVar_StateMecha_Display sc_syncvar_StateMecha_Display;

    GameObject Mng_SyncVar = null;

    [SerializeField]
    SC_UI_SystmShield _SystmShield;

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


    // Start is called before the first frame update
    void Start()
    {
        Mng_SyncVar = GameObject.FindGameObjectWithTag("Mng_SyncVar");
        GetReferences();
    }

    // Update is called once per frame
    void Update()
    {
        if (sc_syncvar_StateMecha_Display == null || sc_syncvar_DisplaySystem == null || Mng_SyncVar == null)
            GetReferences();

        if (sc_syncvar_StateMecha_Display != null && sc_syncvar_DisplaySystem != null)
        {
            _SystmShield.simpleValue = sc_syncvar_DisplaySystem.f_Displaylife;
        }
    }

    void GetReferences()
    {
        if (Mng_SyncVar == null)
            Mng_SyncVar = GameObject.FindGameObjectWithTag("Mng_SyncVar");
        if (Mng_SyncVar != null && sc_syncvar_DisplaySystem == null && sc_syncvar_StateMecha_Display == null)
            sc_syncvar_DisplaySystem = Mng_SyncVar.GetComponent<SC_SyncVar_DisplaySystem>();
            sc_syncvar_StateMecha_Display = Mng_SyncVar.GetComponent<SC_SyncVar_StateMecha_Display>();
    }
}
