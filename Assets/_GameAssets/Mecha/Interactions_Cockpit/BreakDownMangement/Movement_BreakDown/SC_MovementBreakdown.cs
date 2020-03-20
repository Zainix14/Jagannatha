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
    int n_BreakDownLvl = 0;
    public int n_InteractibleInBreakDown = 0;

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

        if(!b_MaxBreakdown)
        {
            n_BreakDownLvl += nbBreakdown;
            SetInteractibleInBreakdown(n_BreakDownLvl);
            CheckBreakdown();
        }

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

        //on update le nombre de pannes
        n_InteractibleInBreakDown = CurNbInteractBreak();

        if(n_InteractibleInBreakDown == interactible.Length)
            b_MaxBreakdown = true;   

        //Resolution du System (MaxBreakDown)
        else if (n_InteractibleInBreakDown == 0 && b_MaxBreakdown)
            EndBreakdown();

        //Resolution du Systeme (Normal BreakDown)
        else if (n_InteractibleInBreakDown == 0 && !b_MaxBreakdown && SC_main_breakdown_validation.Instance.isValidated)
            EndBreakdown();

        SC_MainBreakDownManager.Instance.CheckBreakdown();
        SC_JoystickMove.Instance.AlignBreakdownLevel(n_BreakDownLvl);
        SC_DebugMove.Instance.AlignBreakdownLevel(n_BreakDownLvl);

    }

    public void EndBreakdown()
    {
        b_MaxBreakdown = false;
        n_BreakDownLvl = 0;
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
