using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveGripExample_Switch : MonoBehaviour, IInteractible
{
    public bool isEnPanne = false;

    private bool curState = false;

    private bool desiredValue = false;

    private GameObject Mng_SyncVar;
    private SC_SyncVar_BreakdownTest sc_syncvar;

    [SerializeField]
    button bouton;

    enum button
    {
        inter1,
        inter2,
        inter3

    }

    void Start()
    {
        GetReferences();
    }

    void GetReferences()
    {
        if (Mng_SyncVar == null)
            Mng_SyncVar = GameObject.FindGameObjectWithTag("Mng_SyncVar");
        if (Mng_SyncVar != null && sc_syncvar == null)
            sc_syncvar = Mng_SyncVar.GetComponent<SC_SyncVar_BreakdownTest>();
    }

    public void Flip() {
    Vector3 rotation = transform.localEulerAngles;
    rotation.x *= -1;
    transform.localEulerAngles = rotation;


        curState = !curState;
        sendToSynchVar(curState);


  }

    public bool isBreakdown()
    {

        return isEnPanne;
    }


    void sendToSynchVar(bool value)
    {

        if (sc_syncvar == null)
        {

            GetReferences();
        }
        else
        {

            switch (bouton)
            {
                case button.inter1:
                    sc_syncvar.inter1value = value;
                    break;
                default:
                    break;

            }

        }


    }




    public void ChangeDesired()
    {
        desiredValue = !curState;
        
        isEnPanne = true;

        switch (bouton)
        {
            case button.inter1:
                sc_syncvar.inter1valueWanted = desiredValue;
                sc_syncvar.inter1isEnPanne = true;
                break;
            default:
                break;

        }


    }


    public void IsValueOk()
    {

        if (desiredValue == curState)
        {

            isEnPanne = false;



            if (sc_syncvar == null)
            {

                GetReferences();
            }
            else
            {

                switch (bouton)
                {
                    case button.inter1:
                        sc_syncvar.inter1isEnPanne = false;
                        break;
                    default:
                        break;

                }


            }
        }
        else
        {
            isEnPanne = true;

            if (sc_syncvar == null)
            {

                GetReferences();
            }
            else
            {
                switch (bouton)
                {
                    case button.inter1:
                        sc_syncvar.inter1isEnPanne = true;
                        break;
                    default:
                        break;

                }


            }
        }

    }





    public void Update()
    {

        IsValueOk();
    }
}
