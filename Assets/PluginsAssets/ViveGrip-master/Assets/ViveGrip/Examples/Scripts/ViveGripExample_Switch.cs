using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveGripExample_Switch : MonoBehaviour, IInteractible
{
    public bool isEnPanne = false;

    private bool curState = false;

    private bool desiredValue = false;

    private CustomSoundManager sc_audio_mng;


    private SC_SyncVar_Interactibles sc_syncvar;

    [SerializeField]
    button bouton;

    enum button
    {
        inter1,
        inter2,
        inter3

    }

    void Start() {

        sc_audio_mng = GameObject.FindGameObjectWithTag("Mng_Audio").GetComponent<CustomSoundManager>();

    }

  public void Flip() {
    Vector3 rotation = transform.localEulerAngles;
    rotation.x *= -1;
    transform.localEulerAngles = rotation;


        curState = !curState;
        sendToSynchVar(curState);





        //SON
        if (curState == false)
        {
            sc_audio_mng.PlaySound(gameObject, "SFX_p_click_button_1", false, 1, 0.05f, 0.5f);
        }
        else
        {
            sc_audio_mng.PlaySound(gameObject, "SFX_p_click_button_2", false, 1, 0.05f, 0.4f);
        }
        


  }

    public bool isBreakdown()
    {

        return isEnPanne;
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

        if (Input.GetKeyDown(KeyCode.C))
        {
            Flip();
        }

        IsValueOk();
    }
}
