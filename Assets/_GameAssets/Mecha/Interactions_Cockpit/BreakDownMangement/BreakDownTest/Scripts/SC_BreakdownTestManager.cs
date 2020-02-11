﻿    using System.Collections;
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

    bool b_InitPanne = false;

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
    }

    // Update is called once per frame
    void Update()
    {
        //Panne On Init
        if (!b_InitPanne)
            InitBreakdown();

        if (Input.GetKeyDown(KeyCode.Alpha0))
            StartNewBreakdown(2);
    }

    void InitBreakdown()
    {
        b_InitPanne = true;
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


        if (n_BreakdownValue > 2 && !b_BreakdownTest)
        {
            b_BreakdownTest = true;
            Mng_BreakdownMain.CheckBreakdown();
        }          
        else if (n_BreakdownValue ==0 && b_BreakdownTest)
        {
            b_BreakdownTest = false;
            Mng_BreakdownMain.CheckBreakdown();
        }



    }

    

}