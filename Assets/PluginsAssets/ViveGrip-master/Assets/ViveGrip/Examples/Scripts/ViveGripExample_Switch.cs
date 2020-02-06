using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveGripExample_Switch : MonoBehaviour, IInteractible
{
    public bool isEnPanne = false;

    private bool curState = false;

    private bool desiredValue = false;


    private CustomSoundManager sc_audio_mng;



    private GameObject Mng_SyncVar;
    private SC_SyncVar_BreakdownTest sc_syncvar;
    public GameObject LocalBreakdownMng;


    [SerializeField]
    button bouton;

    enum button
    {
        inter1,
        inter2,
        inter3,
        inter4,
        inter5

    }


    void Start() {

        sc_audio_mng = GameObject.FindGameObjectWithTag("Mng_Audio").GetComponent<CustomSoundManager>();
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

        if (sc_syncvar != null)
        {
            switch (bouton)
            {
                case button.inter1:
                    sc_syncvar.inter1value = value;
                    break;
                case button.inter2:
                    sc_syncvar.inter2value = value;
                    break;
                case button.inter3:
                    sc_syncvar.inter3value = value;
                    break;
                case button.inter4:
                    sc_syncvar.inter4value = value;
                    break;
                case button.inter5:
                    sc_syncvar.inter5value = value;
                    break;
                default:
                    break;
            }
        }
        else
            GetReferences();

    }

    public void ChangeDesired()
    {

        desiredValue = !curState;

        SetIsEnPanne(true);

        switch (bouton)
        {
            case button.inter1:
                sc_syncvar.inter1valueWanted = desiredValue;
                sc_syncvar.inter1isEnPanne = true;
                break;
            case button.inter2:
                sc_syncvar.inter2valueWanted = desiredValue;
                sc_syncvar.inter2isEnPanne = true;
                break;
            case button.inter3:
                sc_syncvar.inter3valueWanted = desiredValue;
                sc_syncvar.inter3isEnPanne = true;
                break;
            case button.inter4:
                sc_syncvar.inter4valueWanted = desiredValue;
                sc_syncvar.inter4isEnPanne = true;
                break;
            case button.inter5:
                sc_syncvar.inter5valueWanted = desiredValue;
                sc_syncvar.inter5isEnPanne = true;
                break;
            default:
                break;
        }

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

                    switch (bouton)
                    {
                        case button.inter1:
                            sc_syncvar.inter1isEnPanne = false;
                            break;
                        case button.inter2:
                            sc_syncvar.inter2isEnPanne = false;
                            break;
                        case button.inter3:
                            sc_syncvar.inter3isEnPanne = false;
                            break;
                        case button.inter4:
                            sc_syncvar.inter4isEnPanne = false;
                            break;
                        case button.inter5:
                            sc_syncvar.inter5isEnPanne = false;
                            break;
                        default:
                            break;
                    }
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

                    switch (bouton)
                    {
                        case button.inter1:
                            sc_syncvar.inter1isEnPanne = true;
                            break;
                        case button.inter2:
                            sc_syncvar.inter2isEnPanne = true;
                            break;
                        case button.inter3:
                            sc_syncvar.inter3isEnPanne = true;
                            break;
                        case button.inter4:
                            sc_syncvar.inter4isEnPanne = true;
                            break;
                        case button.inter5:
                            sc_syncvar.inter5isEnPanne = true;
                            break;
                        default:
                            break;

                    }
                }
                else
                    GetReferences();
            }

            

        }
    }

 
    void SetIsEnPanne(bool value)
    {
        Debug.Log("Switch - SetIsPanne");
        isEnPanne = value;
        LocalBreakdownMng.GetComponent<IF_BreakdownManager>().CheckBreakdown();

    }

}
