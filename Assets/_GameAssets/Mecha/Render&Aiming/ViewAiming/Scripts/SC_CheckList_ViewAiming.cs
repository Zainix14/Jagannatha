using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CheckList_ViewAiming : MonoBehaviour
{

    #region Singleton

    private static SC_CheckList_ViewAiming _instance;
    public static SC_CheckList_ViewAiming Instance { get { return _instance; } }

    #endregion

    [Header("Aim References")]
    public GameObject Screens = null;
    public GameObject Target = null;

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
