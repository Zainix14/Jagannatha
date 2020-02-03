using UnityEngine;


public class ViveGripExample_Dial : MonoBehaviour, IInteractible
{
    public Transform attachedLight;
    private HingeJoint joint;

    private SC_SyncVar_Interactibles sc_syncvar;

    [SerializeField]
    button bouton;

    private float oldAngle = 0;

    public float desiredValue = 0;

    public bool isEnPanne = false;
    public float precision = 1f;

    enum button
    {
        potar1,
        potar2,
        potar3

    }

    void Start()
    {
        joint = GetComponent<HingeJoint>();
    }

    void Update()
    {

        Debug.Log(joint.angle);

        if (joint.angle != oldAngle)
        {
            sendToSynchVar(joint.angle);
        }

        oldAngle = joint.angle;

        /*float g = (joint.angle + 90) / 180;
        float r = 1 - g;
        Color newColor = new Color(r, g, 0);
            if(attachedLight != null)
            {
                attachedLight.gameObject.GetComponent<Renderer>().material.color = newColor;
                attachedLight.GetChild(0).GetComponent<Light>().color = newColor;

            }

      }
      */


        IsValueOk();
    }


    void sendToSynchVar(float value)
    {

        if (sc_syncvar == null)
        {

            sc_syncvar = GameObject.FindGameObjectWithTag("Mng_SyncVar").GetComponent<SC_SyncVar_Interactibles>();
        }
        else
        {

            switch (bouton)
            {
                case button.potar1:
                    sc_syncvar.potar1value = Mathf.Round(value*100f)/100f ;
                    break;
                default:
                    break;

            }

        }


    }


    public void ChangeDesired()
    {
        desiredValue = Random.Range(-70f, 70f);
        while (gameObject.transform.localPosition.y >= desiredValue - precision && gameObject.transform.localPosition.y <= desiredValue + precision)
        {
            desiredValue = Random.Range(-70f,70f);
        }



        isEnPanne = true;


        switch (bouton)
        {
            case button.potar1:
                sc_syncvar.potar1valueWanted = desiredValue;
                sc_syncvar.potar1isEnPanne = true;
                break;
            default:
                break;

        }
        

    }

    public bool isBreakdown()
    {

        return isEnPanne;
    }
    public void IsValueOk()
    {

        if (joint.angle >= desiredValue - precision && joint.angle <= desiredValue + precision)
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
                    case button.potar1:
                        sc_syncvar.potar1isEnPanne = false;
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
                    case button.potar1:
                        sc_syncvar.potar1isEnPanne = true;
                        break;
                    default:
                        break;

                }
                

            }
        }

    }


}
