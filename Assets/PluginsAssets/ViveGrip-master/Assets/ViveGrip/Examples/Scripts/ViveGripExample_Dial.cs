using UnityEngine;


public class ViveGripExample_Dial : MonoBehaviour, IInteractible
{
    public Transform attachedLight;
    private HingeJoint joint;

    private GameObject Mng_SyncVar;
    private SC_SyncVar_BreakdownTest sc_syncvar;
    public GameObject LocalBreakdownMng;

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
        GetReferences();
    }

    void GetReferences()
    {
        if (LocalBreakdownMng == null)
            LocalBreakdownMng = gameObject.GetComponentInParent<GameObject>();
        if (Mng_SyncVar == null)
            Mng_SyncVar = GameObject.FindGameObjectWithTag("Mng_SyncVar");
        if (Mng_SyncVar != null && sc_syncvar == null)
            sc_syncvar = Mng_SyncVar.GetComponent<SC_SyncVar_BreakdownTest>();
    }

    void Update()
    {

       // Debug.Log(joint.angle);

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
            GetReferences();
        }
        else
        {
            switch (bouton)
            {
                case button.potar1:
                    sc_syncvar.potar1value = Mathf.Round(value*100f)/100f ;
                    break;
                case button.potar2:
                    sc_syncvar.potar2value = Mathf.Round(value * 100f) / 100f;
                    break;
                case button.potar3:
                    sc_syncvar.potar3value = Mathf.Round(value * 100f) / 100f;
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

        SetIsEnPanne(true);

        switch (bouton)
        {
            case button.potar1:
                sc_syncvar.potar1valueWanted = desiredValue;
                sc_syncvar.potar1isEnPanne = true;
                break;
            case button.potar2:
                sc_syncvar.potar2valueWanted = desiredValue;
                sc_syncvar.potar2isEnPanne = true;
                break;
            case button.potar3:
                sc_syncvar.potar3valueWanted = desiredValue;
                sc_syncvar.potar3isEnPanne = true;
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

        if (joint.angle >= desiredValue - precision && joint.angle <= desiredValue + precision && isEnPanne)
        {

            SetIsEnPanne(false);

            if (sc_syncvar == null)
            {
                switch (bouton)
                {
                    case button.potar1:
                        sc_syncvar.potar1isEnPanne = false;
                        break;
                    case button.potar2:
                        sc_syncvar.potar2isEnPanne = false;
                        break;
                    case button.potar3:
                        sc_syncvar.potar3isEnPanne = false;
                        break;
                    default:
                        break;
                }
            }
            else
                GetReferences();

        }
        else if (!isEnPanne)
        {

            SetIsEnPanne(true);

            if (sc_syncvar != null)
            {
                switch (bouton)
                {
                    case button.potar1:
                        sc_syncvar.potar1isEnPanne = true;
                        break;
                    case button.potar2:
                        sc_syncvar.potar2isEnPanne = true;
                        break;
                    case button.potar3:
                        sc_syncvar.potar3isEnPanne = true;
                        break;
                    default:
                        break;
                }
            }
            else
                GetReferences();

        }

    }

    void SetIsEnPanne(bool value)
    {
        isEnPanne = value;
        LocalBreakdownMng.GetComponent<IF_BreakdownManager>().CheckBreakdown();
    }

}
