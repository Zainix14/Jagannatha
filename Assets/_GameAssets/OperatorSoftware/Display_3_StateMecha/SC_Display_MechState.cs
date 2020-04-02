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

    }

    // Update is called once per frame
    void Update()
    {
        updateValue();
    }

    void updateValue()
    {
        _SystmShield.simpleValue = SC_SyncVar_DisplaySystem.Instance.f_Displaylife;
    }
}
