using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_WeaponBreakdown : MonoBehaviour, IF_BreakdownManager
{
    #region Singleton

    private static SC_WeaponBreakdown _instance;
    public static SC_WeaponBreakdown Instance { get { return _instance; } }

    #endregion

    #region Variables

    [Header("BreakDown Var")]
    public bool b_MaxBreakdown = false;
    public int CurNbOfBreakdown = 0;

    [Header("BreakDownTimer Var")]
    public float frequency;
    public int offPercentage;
    int onPercentage;
    float curTimer;
    float offTime;
    float onTime;
    bool bCanFire;

    [Header("Interactibles"), SerializeField]
    public GameObject[] interactible;

    #endregion Variables

    #region Init

    // Start is called before the first frame update
    void Awake()
    {

        InitSingleton();

        offPercentage = 0;
        bCanFire = true;

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
        interactible = GameObject.FindGameObjectsWithTag("InteractibleWeapon");
    }

    void Demarage()
    {
        StartNewBreakdown(interactible.Length);     
    }

    #endregion Init

    void Update()
    {
        BreakDownTimer();
    }
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

                    SetNewBreakdown(25);

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

    public void SetNewBreakdown(int percent, float frequency = 25)
    {

        offPercentage += percent;
        if (offPercentage > 50)
            offPercentage = 50;

        this.frequency = frequency;

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

            offPercentage = 25 * CurNbOfBreakdown;


        }

        else if (n_BreakdownValue > 1)
        {

            offPercentage = 25 * CurNbOfBreakdown;

            b_MaxBreakdown = true;

        }

        else if (n_BreakdownValue == 0 && b_MaxBreakdown)
        {

            EndBreakdown();
            b_MaxBreakdown = false;

        }

        //Permet de régler les demi-pannes 
        else if (n_BreakdownValue == 0 && !b_MaxBreakdown && SC_main_breakdown_validation.Instance.isValidated)
        {
            EndBreakdown();
        }

        if (CurNbOfBreakdown > 0)
        {
            
            SC_SyncVar_Main_Breakdown.Instance.onPanneWeaponChange(true);
        }
        else
        {

            SC_SyncVar_Main_Breakdown.Instance.onPanneWeaponChange(false);
        }
        SC_MainBreakDownManager.Instance.CheckBreakdown();
    }

    public void EndBreakdown()
    {
        offPercentage = 0;
    }

    void BreakDownTimer()
    {

        if (offPercentage > 0)
        {

            offTime = offPercentage / frequency;
            onPercentage = 100 - offPercentage;
            onTime = onPercentage / frequency;
            curTimer += Time.deltaTime;

            if (curTimer >= onTime && bCanFire == true)
            {
                curTimer = 0;
                bCanFire = false;
            }

            if (curTimer >= offTime && bCanFire == false)
            {
                curTimer = 0;
                bCanFire = true;
            }

        }

        else
            bCanFire = true;

    }

    public bool CanFire()
    {
        return bCanFire;
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
