    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BreakdownTestManager : MonoBehaviour, IF_BreakdownManager
{

    public bool b_BreakdownTest = false;

    [SerializeField]
    public GameObject[] interactible;

    // Start is called before the first frame update
    void Start()
    {
        interactible = GameObject.FindGameObjectsWithTag("Interactible");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            StartNewBreakdown(2);
        }
    }

    void StartNewBreakdown(int nbBreakdown)
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
            if (!interactible[j].GetComponent<IInteractible>().isBreakdown())
            {
                n_BreakdownValue++;
            }
        }

        if (n_BreakdownValue > 2)
            b_BreakdownTest = true;
        else
            b_BreakdownTest = false;

    }

}
