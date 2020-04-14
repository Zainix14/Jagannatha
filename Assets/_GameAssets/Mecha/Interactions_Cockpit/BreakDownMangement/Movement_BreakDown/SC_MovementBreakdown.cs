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
        if (!b_MaxBreakdown)
        {

            int n_BreakDownLvlTemp = n_BreakDownLvl + nbBreakdown;
            SetBreakdownLvl(n_BreakDownLvlTemp);

            SetSequences(n_BreakDownLvl);

            CheckBreakdown();

        }
    }

    void SetSequences(int BreakdownLvl)
    {

        Debug.Log("StartSequences");

        tab_BreakdownSequence = new int[BreakdownLvl];
        tab_PilotSequence = new int[BreakdownLvl];
        CurPilotSeqLenght = 0;
        b_SeqIsCorrect = false;

        for (int i = 0; i < BreakdownLvl; i++)
        {
            int rnd = Random.Range(0, BreakdownLvl);
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
   
        bool b_isCorrect = true;

        for (int i = 0; i < CheckLenght; i++)
        {
            if (tab_BreakdownSequence[i] != tab_PilotSequence[i])
                b_isCorrect = false;
        }

        if (b_isCorrect)
        {
            if (CheckLenght - 1 == tab_BreakdownSequence.Length)
            {
                CurPilotSeqLenght = 0;
                b_SeqIsCorrect = true;
                //Ranger les Cords              
            }
        }
        else
        {
            //Ranger les Cords
            SetSequences(n_BreakDownLvl);
        }

        CheckBreakdown();

    }

    public void CheckBreakdown()
    {
        
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

        SetMaxBreakdown(false);
        SetBreakdownLvl(0);

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
