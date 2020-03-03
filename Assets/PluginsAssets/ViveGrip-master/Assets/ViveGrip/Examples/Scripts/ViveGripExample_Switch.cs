﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveGripExample_Switch : MonoBehaviour, IInteractible
{
    public bool isEnPanne = false;

    private bool curState = false;

    private bool desiredValue = false;




    private GameObject Mng_SyncVar;
    private SC_SyncVar_BreakdownTest sc_syncvar;
    public GameObject LocalBreakdownMng;

    public int index;
    /*
    [SerializeField]
    button bouton;

    enum button
    {
        inter1,
        inter2,
        inter3

    }
    */

    void Start() {

        
        GetReferences();

        

    }

    void GetReferences()
    {
        if (LocalBreakdownMng == null)
            LocalBreakdownMng = this.transform.parent.parent.gameObject;
        if (Mng_SyncVar == null)
            Mng_SyncVar = GameObject.FindGameObjectWithTag("Mng_SyncVar");
        if (Mng_SyncVar != null && sc_syncvar == null)
            sc_syncvar = Mng_SyncVar.GetComponent<SC_SyncVar_BreakdownTest>();

       
    }

    public void Update()
    {
        
       

        if (Input.GetKeyDown(KeyCode.C))
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            /*
            SC_SyncVar_BreakdownTest.Pow babar = sc_syncvar.m_pows[index] ;
            babar.setPower(3);
            Debug.Log("Valeur de babar  : " + babar.power);
            Debug.Log("wesh : " + sc_syncvar.m_pows[index].power);
            sc_syncvar.m_pows.RemoveAt(index);
            sc_syncvar.m_pows.Insert(index, babar);
            Debug.Log("Valeur de sync  : " + sc_syncvar.m_pows[index].power);
            */
        }

        IsValueOk();
    }

    public void Flip()
    {

        Vector3 rotation = transform.localEulerAngles;
        rotation.x *= -1;
        transform.localEulerAngles = rotation;

        curState = !curState;
        sendToSynchVar(curState);






        //SON
        if (curState == false)
        {
            CustomSoundManager.Instance.PlaySound(gameObject, "SFX_p_click_button_1", false, 1, true,0.05f, 0.5f);
        }
        else
        {
            CustomSoundManager.Instance.PlaySound(gameObject, "SFX_p_click_button_2", false, 1, true, 0.05f, 0.4f);
        }
        


  }



    public bool isBreakdown()
    {
        return isEnPanne;
    }


    void sendToSynchVar(bool value)
    {

        if (sc_syncvar != null)
        {

            sc_syncvar.SwitchChangeValue(index,value);
            

        }
        else
            GetReferences();

    }

    public void ChangeDesired()
    {

        desiredValue = !curState;

        SetIsEnPanne(true);


        sc_syncvar.SwitchChangeValueWanted(index, desiredValue);
        sc_syncvar.SwitchChangeIsPanne(index, true);


    }

    public void Repair()
    {

        desiredValue = curState;

        SetIsEnPanne(false);


        sc_syncvar.SwitchChangeValueWanted(index, desiredValue);
        sc_syncvar.SwitchChangeIsPanne(index, false);


    }

    public void IsValueOk()
    {
        if (desiredValue == curState)
        {
            if (isEnPanne)
            {         

                if (sc_syncvar != null)
                {
                    SetIsEnPanne(false);
                    sc_syncvar.SwitchChangeIsPanne(index, false);
                }
                else
                    GetReferences();
            }          

        }
        else 
        {

            if (!isEnPanne)
            {              

                if (sc_syncvar != null)
                {
                    SetIsEnPanne(true);
                    sc_syncvar.SwitchChangeIsPanne(index, true);
                }
                else
                    GetReferences();
            }          

        }
    }

 
    void SetIsEnPanne(bool value)
    {

        isEnPanne = value;
        LocalBreakdownMng.GetComponent<IF_BreakdownManager>().CheckBreakdown();

    }

}
