using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CheckList_Weapons : MonoBehaviour
{

    #region Singleton

    private static SC_CheckList_Weapons _instance;
    public static SC_CheckList_Weapons Instance { get { return _instance; } }

    #endregion

    [Header("Aim References")]
    public GameObject TargetHand = null;
    public GameObject AimIndicator = null;

    [Header("Managers")]
    public GameObject Mng_Weapons = null;

    [Header("Weapons")]
    public GameObject LaserGun = null;

    [Header("Weapons (No More Used)")]
    public GameObject MiniGun = null;
    public GameObject FlameThrower = null;
    public GameObject Shrapnel = null;  

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
