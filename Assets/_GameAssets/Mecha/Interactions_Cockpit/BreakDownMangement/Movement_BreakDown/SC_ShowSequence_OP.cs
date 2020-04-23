using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ShowSequence_OP : MonoBehaviour
{

    #region Singleton

    private static SC_ShowSequence_OP _instance;
    public static SC_ShowSequence_OP Instance { get { return _instance; } }

    #endregion

    [Header("References")]
    [SerializeField]
    GameObject SeqPart01;
    [SerializeField]
    GameObject SeqPart02;
    [SerializeField]
    GameObject SeqPart03;

    [Header("Debug References")]
    [SerializeField]
    bool b_UseDebugContent = false;
    [SerializeField]
    GameObject DebugCnt01;
    [SerializeField]
    GameObject DebugCnt02;
    [SerializeField]
    GameObject DebugCnt03;

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

    public void DisplaySequence()
    {

    }

}
