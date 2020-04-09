using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CheckList_Mecha : MonoBehaviour
{

    #region Singleton

    private static SC_CheckList_Mecha _instance;
    public static SC_CheckList_Mecha Instance { get { return _instance; } }

    #endregion

    [Header("Colliders")]
    public GameObject MechCollider = null;

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

    public GameObject GetMechCollider()
    {
        return MechCollider;
    }

}
