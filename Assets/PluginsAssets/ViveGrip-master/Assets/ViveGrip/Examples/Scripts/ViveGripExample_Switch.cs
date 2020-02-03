using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveGripExample_Switch : MonoBehaviour, IInteractible
{
    public bool isEnPanne = false;

    private bool curState = false;

    private bool desiredValue = false;


    private SC_SyncVar_Interactibles sc_syncvar;

    [SerializeField]
    button bouton;

    enum button
    {
        inter1,
        inter2,
        inter3

    }

    void Start() {}

  public void Flip() {
    Vector3 rotation = transform.localEulerAngles;
    rotation.x *= -1;
    transform.localEulerAngles = rotation;


        curState = !curState;
        sendToSynchVar(curState);


  }




    void sendToSynchVar(bool value)
    {

        if (sc_syncvar == null)
        {

            sc_syncvar = GameObject.FindGameObjectWithTag("Mng_SyncVar").GetComponent<SC_SyncVar_Interactibles>();
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

                sc_syncvar = GameObject.FindGameObjectWithTag("Mng_SyncVar").GetComponent<SC_SyncVar_Interactibles>();
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

                sc_syncvar = GameObject.FindGameObjectWithTag("Mng_SyncVar").GetComponent<SC_SyncVar_Interactibles>();
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

        if (Input.GetKeyDown(KeyCode.A))
        {
            Flip();
        }
    }
}
