using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_MovementBreakdown : MonoBehaviour, IF_BreakdownManager
{

    #region Singleton

    private static SC_MovementBreakdown _instance;
    public static SC_MovementBreakdown Instance { get { return _instance; } }

    #endregion

    #region Variables

    [Header("BreakDown Var")]
    public bool b_MaxBreakdown = false;
    [SerializeField]
    int n_MaxBreakdownLvl = 3;
    [SerializeField, Range(0, 3)]
    public int n_BreakDownLvl = 0;
    public int n_InteractibleInBreakDown = 0;

    [Header("Sequences Infos")]
    [SerializeField]
    int[] tab_BreakdownSequence;
    [SerializeField]
    int[] tab_PilotSequence;
    [SerializeField]
    int CurPilotSeqLenght = 0;
    [SerializeField]
    bool b_SeqIsCorrect = false;

    [Header("Interactibles"), SerializeField]
    public GameObject[] interactible;

    #endregion Variables

    #region Init

    void Awake()
    {

        InitSingleton();

        Invoke("Demarage", 0.5f);

    }

    void InitSingleton()
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

    void Demarage()
    {
        StartNewBreakdown(3);
    }

    #endregion Init

    public void StartNewBreakdown(int nbBreakdown)
    {
        Debug.Log("StartNewBdMov");

        if (!b_MaxBreakdown)
        {

            int n_BreakDownLvlTemp = n_BreakDownLvl + nbBreakdown;
            SetBreakdownLvl(n_BreakDownLvlTemp);

            SetSequences();

            CheckBreakdown();

        }
    }

    void SetSequences()
    {

        ResizeTab();
        CurPilotSeqLenght = 0;
        b_SeqIsCorrect = false;

        for (int i = 0; i < n_BreakDownLvl; i++)
        {
            int rnd = Random.Range(1, 3);
            tab_BreakdownSequence[i] = rnd;
        }

        //Call les Cords

    }

    public void AddToPilotSeq(int CordIndex)
    {
        if (CurPilotSeqLenght < tab_BreakdownSequence.Length)
        {
            tab_PilotSequence[CurPilotSeqLenght] = CordIndex;
            CurPilotSeqLenght++;
            CheckSequences(CurPilotSeqLenght);
        }
    }

    void CheckSequences(int CheckLenght)
    {

        Debug.Log("Start CheckSq " + CheckLenght);

        bool b_isCorrect = true;

        for (int i = 0; i < CheckLenght; i++)
        {
            if (tab_BreakdownSequence[i] != tab_PilotSequence[i])
                b_isCorrect = false;
        }

        Debug.Log("CheckSq " + b_isCorrect);

        if (b_isCorrect)
        {
            if (CheckLenght == tab_BreakdownSequence.Length)
            {
                Debug.Log("isCorrect");
                CurPilotSeqLenght = 0;
                b_SeqIsCorrect = true;
                //Ranger les Cords              
            }
        }
        else
        {
            Debug.Log("Reset");
            //Ranger les Cords
            SetSequences();
        }

        CheckBreakdown();

    }

    public void CheckBreakdown()
    {

        Debug.Log("Check");

        if (n_BreakDownLvl == n_MaxBreakdownLvl)
            SetMaxBreakdown(true);

        //Resolution du System (MaxBreakDown)
        else if (b_SeqIsCorrect && b_MaxBreakdown)
            EndBreakdown();

        //Resolution du Systeme (Normal BreakDown)
        else if (b_SeqIsCorrect && !b_MaxBreakdown && SC_main_breakdown_validation.Instance.isValidated)
            EndBreakdown();

        SC_MainBreakDownManager.Instance.CheckBreakdown();
        SC_JoystickMove.Instance.AlignBreakdownLevel(n_BreakDownLvl);

        //if (n_InteractibleInBreakDown > 0)
        if (n_BreakDownLvl > 0)
        {
            SC_SyncVar_Main_Breakdown.Instance.onPanneMovementChange(true);
        }

        else
        {
            SC_SyncVar_Main_Breakdown.Instance.onPanneMovementChange(false);
        }

    }

    public void EndBreakdown()
    {

        Debug.Log("End Mov Bd");

        SetMaxBreakdown(false);
        SetBreakdownLvl(0);
        ResizeTab();

        int rnd = Random.Range(0, 1);
        if(rnd == 0)
        {
            SC_JoystickMove.Instance.SetBrokenDir(SC_JoystickMove.Dir.Left);
        }
        else
        {
            SC_JoystickMove.Instance.SetBrokenDir(SC_JoystickMove.Dir.Right);
        }
        
    }

    void ResizeTab()
    {
        tab_BreakdownSequence = new int[n_BreakDownLvl];
        tab_PilotSequence = new int[n_BreakDownLvl];
    }

    void SetMaxBreakdown(bool TargetState)
    {
        b_MaxBreakdown = TargetState;
        SC_SyncVar_MovementSystem.Instance.b_MaxBreakdown = TargetState;
    }

    void SetBreakdownLvl(int TargetLvl)
    {
        n_BreakDownLvl = TargetLvl;
        SC_SyncVar_MovementSystem.Instance.n_BreakDownLvl = TargetLvl;
    }

    #region DebugMethod

    /// <summary>
    /// Focntion permettant de réparer tous les boutons automatiquement
    /// </summary>
    public void RepairBreakdownDebug()
    {
        for (int j = 0; j < interactible.Length; j++)
        {
            interactible[j].GetComponent<IInteractible>().Repair();
        }
    }

    public void RepairSingleBreakdownDebug()
    {

        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < interactible.Length; i++)
        {

            if (interactible[i].GetComponent<IInteractible>().isBreakdown())
            {
                list.Add(interactible[i]);
            }

        }

        int rnd = Random.Range(0, list.Count);
        list[rnd].GetComponent<IInteractible>().Repair();

    }

    #endregion DebugMethod

}
