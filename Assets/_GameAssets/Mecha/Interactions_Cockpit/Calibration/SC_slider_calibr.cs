using UnityEngine;
using System.Collections;

public class SC_slider_calibr : MonoBehaviour {
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

    /// <summary>
    /// Index du slider pour sa structList
    /// </summary>
    /// 

    public int index;


    private GameObject Mng_SyncVar;
    private Rigidbody sliderRigidbody;


    private SC_SyncVar_calibr sc_syncvar_calibr;

    private GameObject minigun;
    private SC_WeaponMiniGun sc_minigun;

    void Start ()
    {
        oldX = transform.position.x;
        sliderRigidbody = gameObject.GetComponent<Rigidbody>();
        GetReferences();
    }

    void GetReferences()
    {
        
        if (Mng_SyncVar == null)
            Mng_SyncVar = GameObject.FindGameObjectWithTag("Mng_SyncVar");
        if (Mng_SyncVar != null && sc_syncvar_calibr == null)
            sc_syncvar_calibr = Mng_SyncVar.GetComponent<SC_SyncVar_calibr>();

        if (minigun == null)
            minigun = GameObject.FindGameObjectWithTag("Weapon_minigun");
        if (minigun != null && sc_minigun==null)
            sc_minigun = minigun.GetComponent<SC_WeaponMiniGun>();
        
    }

    void ViveGripGrabStart(ViveGrip_GripPoint gripPoint)
    {
    controller = gripPoint.controller;
        sliderRigidbody.isKinematic = false;
    }

    void ViveGripGrabStop()
    {
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

 


    }


    

    void sendToSynchVar (float value)
    {

        if (sc_syncvar_calibr == null || minigun == null)
        {
            GetReferences();
        }
        else
        {


            value = (value + 0.4f) * 6.25f;

            int intvalue = (int)value;

            sc_syncvar_calibr.CalibrInts[index] = intvalue;
            sc_minigun.SetSensitivity(index, intvalue);
            //Debug.Log(intvalue);

        }          

    }

    
}
