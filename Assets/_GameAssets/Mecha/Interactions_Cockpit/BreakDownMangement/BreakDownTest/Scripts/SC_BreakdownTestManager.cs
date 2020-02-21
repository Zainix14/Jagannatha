using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BreakdownTestManager : MonoBehaviour, IF_BreakdownManager
{

    #region Singleton

    private static SC_BreakdownTestManager _instance;
    public static SC_BreakdownTestManager Instance { get { return _instance; } }

    #endregion
    public bool b_BreakdownTest = false;

    private IF_BreakdownManager Mng_BreakdownMain;

    public GameObject screenController;
    private SC_breakdown_displays_screens sc_screens_controller;

    [SerializeField]
    public GameObject[] interactible;

    // Start is called before the first frame update
    void Start()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        Mng_BreakdownMain = this.transform.parent.gameObject.GetComponent<IF_BreakdownManager>();
        interactible = GameObject.FindGameObjectsWithTag("Interactible");


        //get du script qui gere l'affichage des ecrans de panne
        sc_screens_controller = screenController.GetComponent<SC_breakdown_displays_screens>();

        Invoke("Demarage", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            StartNewBreakdown(2);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RepairBreakdownDebug();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(GameObject.Find("Slider").GetComponent<ViveGripExample_Slider>().desiredValue);
        }
    }

    void Demarage()
    {
        //StartNewBreakdown(interactible.Length);
    }


    public void StartNewBreakdown(int nbBreakdown)
    { 
        int curBreakdown = nbBreakdown;
        bool newBreakdown = true;
        for(int i=0;i< nbBreakdown;i++)
        {
            if (newBreakdown)
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

                    //on met en panne un écran
                    sc_screens_controller.PutOneEnPanne();

                    curBreakdown++;
                    if (curBreakdown == nbBreakdown)
                    {
                        newBreakdown = false;
                    }
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


        if (n_BreakdownValue > 5 && !b_BreakdownTest)
        {
            b_BreakdownTest = true;
            Mng_BreakdownMain.CheckBreakdown();
        }          
        else if (n_BreakdownValue ==0 && b_BreakdownTest)
        {


            b_BreakdownTest = false;
            sc_screens_controller.RepairAll();
            Mng_BreakdownMain.CheckBreakdown();
        }

        //Permet de régler les demi-pannes d'écrans
        if (n_BreakdownValue == 0)
        {
            sc_screens_controller.RepairAll();

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

