using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

/// <summary>
/// Sur Mng_Device | 
/// Verifie la presence du casque VR.
/// </summary>
public class SC_DeviceManager : MonoBehaviour
{

    GameObject Mng_CheckList = null;

    public GameObject VR_Assets;

    public bool b_IsVR = false;
    public bool b_IsFPS = false;

    string[] tab_Device;

    // Start is called before the first frame update
    void Start()
    {

        IsCheck();

        CheckDevice();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
            GetJoyStickName();
    }

    void IsCheck()
    {
        Mng_CheckList = GameObject.FindGameObjectWithTag("Mng_CheckList");
        Mng_CheckList.GetComponent<SC_CheckList>().Mng_Device = this.gameObject;
    }

    void CheckDevice()
    {
        if (XRSettings.isDeviceActive)
        {
            b_IsVR = true;
        }
        else
        {
            b_IsFPS = true;
        }
    }

    void GetJoyStickName()
    {
        tab_Device = Input.GetJoystickNames();

        for(int i = 0; i < tab_Device.Length; i++)
        {
            Debug.Log(tab_Device[i]);
        }

    }

}
