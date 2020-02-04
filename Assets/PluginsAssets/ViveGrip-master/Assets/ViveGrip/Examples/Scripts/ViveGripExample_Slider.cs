using UnityEngine;
using System.Collections;

public class ViveGripExample_Slider : MonoBehaviour, IInteractible {
  private ViveGrip_ControllerHandler controller;
  private float oldX;
  private int VIBRATION_DURATION_IN_MILLISECONDS = 50;
  private float MAX_VIBRATION_STRENGTH = 0.2f;
  private float MAX_VIBRATION_DISTANCE = 0.03f;


    private float _localX = 0;
    private float _localY = 0;
    private float _localZ = 0;
    public bool _freezeAlongX = true;
    public bool _freezeAlongY = false;
    public bool _freezeAlongZ = true;


    public float desiredValue = 0;
    public bool isEnPanne = false;
    public float precision = 0.05f;


    private Rigidbody sliderRigidbody;

    private SC_SyncVar_Interactibles sc_syncvar;

    [SerializeField]
    button bouton;

     enum button
    {
        slider1,
        slider2,
        slider3

    }

    void Start () {
    oldX = transform.position.x;

        sliderRigidbody = gameObject.GetComponent<Rigidbody>();
  }

  void ViveGripGrabStart(ViveGrip_GripPoint gripPoint) {
    controller = gripPoint.controller;
        sliderRigidbody.isKinematic = false;
  }

  void ViveGripGrabStop() {
    controller = null;
        sliderRigidbody.isKinematic = true;
    }

	void Update () {
        
        //on traduit la position en position locale pour la freeze
        _localX = transform.localPosition.x;
        _localY = transform.localPosition.y;
        _localZ = transform.localPosition.z;

        if (_freezeAlongX) _localX = 0;
        if (_freezeAlongY) _localY = 0;
        if (_freezeAlongZ) _localZ = 0;
        gameObject.transform.localPosition = new Vector3(_localX, _localY, _localZ);

        
     

        float newX = gameObject.transform.localPosition.y;

        //on envoie la valeur à la syncvar si celle ci a changé
        if (newX != oldX) sendToSynchVar(Mathf.Round(gameObject.transform.localPosition.y*100)/100);



    if (controller != null) {
      float distance = Mathf.Min(Mathf.Abs(newX - oldX), MAX_VIBRATION_DISTANCE);
      float vibrationStrength = (distance / MAX_VIBRATION_DISTANCE) * MAX_VIBRATION_STRENGTH;
      controller.Vibrate(VIBRATION_DURATION_IN_MILLISECONDS, vibrationStrength);
    }
    oldX = newX;

 
    IsValueOk();
    }


    public bool isBreakdown()
    {
        return isEnPanne;
    }

    void sendToSynchVar (float value)
    {

        if (sc_syncvar == null)
        {

            sc_syncvar = GameObject.FindGameObjectWithTag("Mng_SyncVar").GetComponent<SC_SyncVar_Interactibles>();
        }
        else
        {

            switch (bouton)
            {
                case button.slider1:
                    sc_syncvar.slider1value = value;
                    break;
                case button.slider2:
                    sc_syncvar.slider2value = value;
                    break;
                case button.slider3:
                    sc_syncvar.slider3value = value;
                    break;
                default:
                    break;

            }

        }
            

    }


    public void ChangeDesired()
    {
        desiredValue = Random.Range(-0.4f, 0.4f);
        while (gameObject.transform.localPosition.y >= desiredValue - precision && gameObject.transform.localPosition.y <= desiredValue + precision)
        {
            desiredValue = Random.Range(-0.4f, 0.4f);
        }



         isEnPanne = true;


        switch (bouton)
        {
            case button.slider1:
                sc_syncvar.slider1valueWanted = desiredValue;
                sc_syncvar.slider1isEnPanne = true;
                break;
            case button.slider2:
                sc_syncvar.slider2valueWanted = desiredValue;
                sc_syncvar.slider2isEnPanne = true;
                break;
            case button.slider3:
                sc_syncvar.slider3valueWanted = desiredValue;
                sc_syncvar.slider3isEnPanne = true;
                break;
            default:
                break;

        }
        

    }


    public void IsValueOk()
    {

        if (gameObject.transform.localPosition.y >= desiredValue - precision && gameObject.transform.localPosition.y <= desiredValue + precision)
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
                    case button.slider1:
                        sc_syncvar.slider1isEnPanne = false;
                        break;
                    case button.slider2:
                        sc_syncvar.slider2isEnPanne = false;
                        break;
                    case button.slider3:
                        sc_syncvar.slider3isEnPanne = false;
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
                    case button.slider1:
                        sc_syncvar.slider1isEnPanne = true;
                        break;
                    case button.slider2:
                        sc_syncvar.slider2isEnPanne = true;
                        break;
                    case button.slider3:
                        sc_syncvar.slider3isEnPanne = true;
                        break;
                    default:
                        break;

                }
                

            }
        }

    }

}
