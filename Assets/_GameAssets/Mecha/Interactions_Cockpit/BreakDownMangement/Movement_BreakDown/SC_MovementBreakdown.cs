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

        GetInteractibles();

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

    void GetInteractibles()
    {
        //LES ITNERACTIBLES d'ARME NECESSITENT CE TAG
        interactible = GameObject.FindGameObjectsWithTag("InteractibleMovement");
    }

    void Demarage()
    {
        StartNewBreakdown(interactible.Length);
    }

    #endregion Init

    public void StartNewBreakdown(int nbBreakdown)
    {
        if (!b_MaxBreakdown)
        {

            int n_BreakDownLvlTemp = n_BreakDownLvl + nbBreakdown;
            SetBreakdownLvl(n_BreakDownLvlTemp);

            //Buttons
            SetInteractibleInBreakdown(n_BreakDownLvl);
            CheckBreakdown();

            //Sequences
            //SetSequences(n_BreakDownLvl);

        }
    }

    void SetSequences(int BreakdownLvl)
    {
        tab_BreakdownSequence = new int[BreakdownLvl];
        tab_PilotSequence = new int[BreakdownLvl];
        CurPilotSeqLenght = 0;
        b_SeqIsCorrect = false;

        for (int i = 0; i < tab_BreakdownSequence.Length; i++)
        {
            int rnd = Random.Range(0, interactible.Length);
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

    void SetInteractibleInBreakdown(int n_InteractibleToBreak)
    {

        bool newBreakdown = true;

        for (int i = 0; i < n_InteractibleToBreak; i++)
        {

            if (newBreakdown && !b_MaxBreakdown)
            {

                //Nb d'Interactible deja en panne
                n_InteractibleInBreakDown = CurNbInteractBreak();

                //Si il y 'a deja plus d'Interactible en panne que demander
                if (n_InteractibleInBreakDown >= n_InteractibleToBreak)
                {
                    newBreakdown = false;
                    break;
                }
                else
                    i = CurNbInteractBreak();

                //Choix Aleatoire d'un nteractible
                int rnd = Random.Range(0, interactible.Length);

                //Si il est deja en panne la boucle recule de  (annule le passage actuel)
                if (interactible[rnd].GetComponent<IInteractible>().isBreakdown())
                    i--;

                //Met un Interactible en Panne
                else
                {

                    interactible[rnd].GetComponent<IInteractible>().ChangeDesired();

                    n_InteractibleInBreakDown = CurNbInteractBreak();

                    if (n_InteractibleInBreakDown == n_InteractibleToBreak)
                    {
                        newBreakdown = false;
                        break;
                    }

                }

            }

        }

    }   

    public void CheckBreakdown()
    {

        /*
        //Button

        //on update le nombre de pannes
        n_InteractibleInBreakDown = CurNbInteractBreak();

        if(n_InteractibleInBreakDown == interactible.Length)
            SetMaxBreakdown(true);

        //Resolution du System (MaxBreakDown)
        else if (n_InteractibleInBreakDown == 0 && b_MaxBreakdown)
            EndBreakdown();

        //Resolution du Systeme (Normal BreakDown)
        else if (n_InteractibleInBreakDown == 0 && !b_MaxBreakdown && SC_main_breakdown_validation.Instance.isValidated)
            EndBreakdown();
    */

        //Sequences
        
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

    int CurNbInteractBreak()
    {

        int n_InBreakdown = 0;

        //Cb d'interactibles sont en Breakdown
        for (int j = 0; j < interactible.Length; j++)
        {
            if (interactible[j].GetComponent<IInteractible>().isBreakdown())
                n_InBreakdown++;
        }

        return n_InBreakdown;

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
