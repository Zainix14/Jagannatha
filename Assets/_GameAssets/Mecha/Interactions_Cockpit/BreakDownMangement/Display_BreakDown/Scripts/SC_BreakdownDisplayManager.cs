using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BreakdownDisplayManager : MonoBehaviour, IF_BreakdownManager
{

    #region Singleton

    private static SC_BreakdownDisplayManager _instance;
    public static SC_BreakdownDisplayManager Instance { get { return _instance; } }

    #endregion

    #region Variables

    [Header("References")]
    public GameObject screenController;
    private IF_BreakdownManager Mng_BreakdownMain;
    private SC_breakdown_displays_screens sc_screens_controller;

    [Header("BreakDown Var")]
    [SerializeField]
    int n_MaxBreakInterB4MaxBD = 5;
    public bool b_MaxBreakdown = false;
    public int CurNbOfBreakdown = 0;

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

    void Start()
    {
        Mng_BreakdownMain = this.transform.parent.gameObject.GetComponent<IF_BreakdownManager>();
        //get du script qui gere l'affichage des ecrans de panne
        sc_screens_controller = screenController.GetComponent<SC_breakdown_displays_screens>();
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
        interactible = GameObject.FindGameObjectsWithTag("Interactible");
    }

    void Demarage()
    {
        StartNewBreakdown(interactible.Length);
    }

    #endregion Init

    public void StartNewBreakdown(int nbBreakdown)
    {

        int curBreakdown = nbBreakdown;
        bool newBreakdown = true;

        for(int i=0;i< nbBreakdown;i++)
        {

            if (newBreakdown && !b_MaxBreakdown && !SC_MainBreakDownManager.Instance.b_BreakEngine)
            {

                int noBreakdown = 0;

                for (int j = 0; j < interactible.Length; j++)
                {
                    if (!interactible[j].GetComponent<IInteractible>().isBreakdown())
                        noBreakdown++;                  
                }

                if (noBreakdown == 0)
                {
                    newBreakdown = false;
                    break;
                }             

                int rnd = Random.Range(0, interactible.Length);

                if (interactible[rnd].GetComponent<IInteractible>().isBreakdown() || !interactible[rnd].GetComponent<IInteractible>().testAgainstOdds())
                {
                    i--;
                }

                else
                {
                    interactible[rnd].GetComponent<IInteractible>().ChangeDesired();
                    //on itere le nombre de pannes total
                    CurNbOfBreakdown++;
                    //on met en panne un �cran
                    sc_screens_controller.PutOneEnPanne();

                    curBreakdown++;

                    if (curBreakdown == nbBreakdown)
                        newBreakdown = false;

                }

            }

        }
        
    }

    public void CheckBreakdown()
    {
        int n_BreakdownValue = 0;

        for (int j = 0; j < interactible.Length; j++)
        {

            if (interactible[j].GetComponent<IInteractible>().isBreakdown())
            {
                n_BreakdownValue++;
            }

        }

        //on update le nombre de pannes
        CurNbOfBreakdown = n_BreakdownValue;

        if (n_BreakdownValue >= n_MaxBreakInterB4MaxBD && !b_MaxBreakdown)
        {
            b_MaxBreakdown = true;
            Mng_BreakdownMain.CheckBreakdown();
        }   
        
        else if (n_BreakdownValue ==0 && b_MaxBreakdown)
        {
            b_MaxBreakdown = false;
            Mng_BreakdownMain.CheckBreakdown();
        }

        //Permet de r�gler les demi-pannes d'�crans
        else if (n_BreakdownValue == 0 && !b_MaxBreakdown && SC_main_breakdown_validation.Instance.isValidated)
        {
            sc_screens_controller.RepairAll();
        }

    }

    #region DebugMethod

    /// <summary>
    /// Focntion permettant de r�parer tous les boutons automatiquement
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
        for(int i =0; i< interactible.Length;i++)
        {

            if(interactible[i].GetComponent<IInteractible>().isBreakdown())
            {
                list.Add(interactible[i]);
            }

        }

        int rnd = Random.Range(0, list.Count);
        list[rnd].GetComponent<IInteractible>().Repair();

    }

    #endregion DebugMethod

}

