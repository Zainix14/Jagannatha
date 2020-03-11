using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_WeaponBreakdown : MonoBehaviour, IF_BreakdownManager
{
    #region Singleton

    private static SC_WeaponBreakdown _instance;
    public static SC_WeaponBreakdown Instance { get { return _instance; } }

    #endregion

    public bool b_BreakdownTest = false;

    public int CurNbOfBreakdown = 0;

    public float frequency;
    public int offPercentage;
    int onPercentage;

    float curTimer;
    float offTime;
    float onTime;

    bool bCanFire;


    [SerializeField]
    public GameObject[] interactible;

    // Start is called before the first frame update
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
        offPercentage = 0;
        bCanFire = true;

        //LES ITNERACTIBLES d'ARME NECESSITENT CE TAG

        interactible = GameObject.FindGameObjectsWithTag("InteractibleWeapon");

        Invoke("Demarage", 0.5f);
    }


    void Demarage()
    {
        StartNewBreakdown(interactible.Length);
       
    }



    public void StartNewBreakdown(int nbBreakdown)
    {
        int curBreakdown = 0;
        bool newBreakdown = true;
        for (int i = 0; i < nbBreakdown; i++)
        {
            if (newBreakdown && !b_BreakdownTest)
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
                    //on itere le nombre de pannes total
                    CurNbOfBreakdown++;

                    SetNewBreakdown(50);

                    curBreakdown++;



                    if (curBreakdown == nbBreakdown)
                    {
                 
                        newBreakdown = false;
                    }
                }
            }
        }


    }



    // Update is called once per frame
    void Update()
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

        ///////DEBUG

        if (Input.GetKeyDown(KeyCode.P))
        {
            StartNewBreakdown(1);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RepairBreakdownDebug();
            
            
        }

    }

    public void SetNewBreakdown(int percent, float frequency = 25)
    {
        offPercentage += percent;

        if (offPercentage > 100)
            offPercentage = 100;

        this.frequency = frequency;
    }

    public void EndBreakdown()
    {
        offPercentage = 0;
    }

    public bool CanFire()
    {
        return bCanFire;
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

        if (n_BreakdownValue > 0)
        {
            offPercentage = 50 * CurNbOfBreakdown;

            SC_MainBreakDownManager.Instance.CheckBreakdown();
        }
        else if (n_BreakdownValue > 1)
        {
            offPercentage = 50 * CurNbOfBreakdown;

            b_BreakdownTest = true;
            SC_MainBreakDownManager.Instance.CheckBreakdown();

        }
        else if (n_BreakdownValue == 0 && b_BreakdownTest)
        {
            EndBreakdown();
            b_BreakdownTest = false;
            SC_MainBreakDownManager.Instance.CheckBreakdown();

        }

        //Permet de régler les demi-pannes 
        else if (n_BreakdownValue == 0 && !b_BreakdownTest && SC_main_breakdown_validation.Instance.isValidated)
        {
            EndBreakdown();

        }


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
