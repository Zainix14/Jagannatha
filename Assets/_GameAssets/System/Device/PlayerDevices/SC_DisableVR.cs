using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sur VR Assets | 
/// Desactive Automatiquement la VR si aucun casque n'est branché.
/// </summary>
public class SC_DisableVR : MonoBehaviour
{

    GameObject Mng_CheckList = null;

    public GameObject VR_Assets;
    public Camera Cam_FPS;

    bool b_IsVR = false;
    bool b_IsFPS = false;

    // Start is called before the first frame update
    void Start()
    {
        Mng_CheckList = GameObject.FindGameObjectWithTag("Mng_CheckList");
    }

    // Update is called once per frame
    void Update()
    {

        if (!b_IsFPS && !b_IsVR)
            GetDeviceState();

        if (b_IsFPS)
            DisableVR();

    }

    void GetDeviceState()
    {

        if(Mng_CheckList.GetComponent<SC_CheckList>().Mng_Device.GetComponent<SC_DeviceManager>().b_IsVR != null)
            b_IsVR = Mng_CheckList.GetComponent<SC_CheckList>().Mng_Device.GetComponent<SC_DeviceManager>().b_IsVR;

        if (Mng_CheckList.GetComponent<SC_CheckList>().Mng_Device.GetComponent<SC_DeviceManager>().b_IsFPS != null)
            b_IsFPS = Mng_CheckList.GetComponent<SC_CheckList>().Mng_Device.GetComponent<SC_DeviceManager>().b_IsFPS;

    }

    void DisableVR()
    {
        VR_Assets.gameObject.SetActive(false);
        Cam_FPS.gameObject.SetActive(true);
    }

}
