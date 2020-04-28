using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    GameObject[] DebugContents;

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

    void Start()
    {

        if(!b_UseDebugContent)
            for (int i = 0; i < DebugContents.Length; i++)
                DebugContents[i].SetActive(false);

    }

    public void DisplaySequence()
    {

        //Y'a un truc dedans qui bug donc a debug
        /*

        for (int i = 0; i < DebugContents.Length; i++)
            DebugContents[i].SetActive(false);

        int SequenceLenght = SC_SyncVar_MovementSystem.Instance.BreakdownList.Count;

        for (int i = 0; i < SequenceLenght; i++)
        {
            DebugContents[i].SetActive(true);
            DebugContents[i].GetComponent<TextMeshPro>().text = SC_SyncVar_MovementSystem.Instance.BreakdownList[i].ToString();
        }

        */

    }

}
