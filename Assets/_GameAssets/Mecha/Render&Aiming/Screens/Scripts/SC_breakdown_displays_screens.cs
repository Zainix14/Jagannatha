using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// script sur le parent des ecrans de panne
/// </summary>

public class SC_breakdown_displays_screens : MonoBehaviour
{

    private int curNbPanne = 0;

    public Renderer[] tab_screens_renderers;

    GameObject Mng_SyncVar = null;
    SC_SyncVar_StateMecha_Display sc_syncvar_display;

    // Start is called before the first frame update
    void Start()
    {
        tab_screens_renderers = new Renderer[gameObject.transform.childCount];

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            tab_screens_renderers[i] = gameObject.transform.GetChild(i).GetComponent<Renderer>();
            tab_screens_renderers[i].enabled = false;
        }

    }

    void GetReferences()
    {
        if (Mng_SyncVar == null)
        {
            Mng_SyncVar = GameObject.FindGameObjectWithTag("Mng_SyncVar");
            sc_syncvar_display = Mng_SyncVar.GetComponent<SC_SyncVar_StateMecha_Display>();
        }


    }


        // Update is called once per frame
        void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {

                PutOneEnPanne();

        }
        
        if (Input.GetKeyDown(KeyCode.K))
        {

            RepairAll();

        }
    }
    /* unused for now
    void PutXenPanne(int x)
    {
        for (int i = 0; i < x; i++)
        {
            if (curNbPanne < tab_screens_renderers.Length)
            {
                int rand = Random.Range(0, tab_screens_renderers.Length);
                if (tab_screens_renderers[rand].enabled)
                {

                    i--;

                }
                else
                {
                    tab_screens_renderers[rand].enabled = true;
                    curNbPanne++;

                }
            }
                
        }


    }
    */

    public void PutOneEnPanne()
    {
        for (int i = 0; i < 1; i++)
        {
            if (curNbPanne < tab_screens_renderers.Length)
            {
                int rand = Random.Range(0, tab_screens_renderers.Length-1);
                if (tab_screens_renderers[rand].enabled)
                {

                    i--;

                }
                else
                {
                    SetScreenState(rand,true);

                }
            }

        }


    }

    public void PanneAll()
    {
        for (int i = 0; i < tab_screens_renderers.Length; i++)
        {
            SetScreenState(i,true);
            
           
        }
    }

    public void RepairAll()
    {
        for (int i = 0; i < tab_screens_renderers.Length; i++)
        {
            SetScreenState(i, false);
        }
    }

    //fonction qui change state l'ecran demandé des deux cotes true == panne false == repare
    private void SetScreenState(int index, bool state)
    {
        if (state == true && tab_screens_renderers[index].enabled != state)
            curNbPanne++;
        else if (tab_screens_renderers[index].enabled != state)
            curNbPanne--;

        tab_screens_renderers[index].enabled = state;

        

        if (Mng_SyncVar == null)
            GetReferences();

        //Debug.Log(curNbPanne);

        //cote operateur
        sc_syncvar_display.displayAll[index] = state;
    }
}
