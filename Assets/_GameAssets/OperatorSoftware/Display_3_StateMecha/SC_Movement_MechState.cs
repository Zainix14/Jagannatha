using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Movement_MechState : MonoBehaviour
{
    #region Singleton

    private static SC_Movement_MechState _instance;
    public static SC_Movement_MechState Instance { get { return _instance; } }

    #endregion

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

    // Update is called once per frame
    void Update()
    {

        UpdateValue();

    }

    void UpdateValue()
    {
        _SystmShield.simpleValue = SC_SyncVar_MovementSystem.Instance.f_MovementLife;
    }

}
