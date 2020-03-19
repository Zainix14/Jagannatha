using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_MovementBreakdown : MonoBehaviour
{
    #region Singleton

    private static SC_MovementBreakdown _instance;
    public static SC_MovementBreakdown Instance { get { return _instance; } }

    #endregion

    #region Variables

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
        int curBreakdown = 0;
        bool newBreakdown = true;

        for (int i = 0; i < nbBreakdown; i++)
        {

            if (newBreakdown && !b_MaxBreakdown)
            {

                int noBreakdown = 0;

                for (int j = 0; j < interactible.Length; j++)
                {
                    if (!interactible[j].GetComponent<IInteractible>().isBreakdown())
                    {
                        noBreakdown++;
                    }
                }

                if (noBreakdown == 0)
                {
                    newBreakdown = false;
                    break;
                }

                int rnd = Random.Range(0, interactible.Length);
                if (interactible[rnd].GetComponent<IInteractible>().isBreakdown())
                {
                    i--;
                }
                else
                {

                    interactible[rnd].GetComponent<IInteractible>().ChangeDesired();

                    SetNewBreakdown();

                    curBreakdown++;

                    if (curBreakdown == nbBreakdown)
                    {
                        newBreakdown = false;
                    }

                }

            }

        }

        CheckBreakdown();
    }

    void SetNewBreakdown()
    {
        // A coder ici les effet de breakdown
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

        if (n_BreakdownValue == 1)
        {
            //Effet quand panne rang 1
            SC_MainBreakDownManager.Instance.CheckBreakdown();
        }
        else if (n_BreakdownValue == 2)
        {
            //Effet quand panne rang 2
            SC_MainBreakDownManager.Instance.CheckBreakdown();
        }
        else if (n_BreakdownValue > 2)
        {
            //Effet quand panne rang Max
            b_MaxBreakdown = true;
            SC_MainBreakDownManager.Instance.CheckBreakdown();

        }
        else if (n_BreakdownValue == 0 && b_MaxBreakdown)
        {
            EndBreakdown();
            b_MaxBreakdown = false;
            SC_MainBreakDownManager.Instance.CheckBreakdown();
        }

        //Permet de régler les demi-pannes 
        else if (n_BreakdownValue == 0 && !b_MaxBreakdown && SC_main_breakdown_validation.Instance.isValidated)
        {
            EndBreakdown();
        }

    }

    public void EndBreakdown()
    {
        //Reset fin de panne
    }

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

}
