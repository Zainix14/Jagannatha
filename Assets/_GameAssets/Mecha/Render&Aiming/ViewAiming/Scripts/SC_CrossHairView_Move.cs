using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CrossHairView_Move : MonoBehaviour
{

    public Camera Cam_Mech = null;
    public Camera Cam_Cockpit = null;

    public bool b_IsVR = false;
    public bool b_IsFPS = false;

    public float f_CrossHairDist = 2f;

    // Update is called once per frame
    void Update()
    {

        if (!b_IsFPS && !b_IsVR)
            GetDeviceState();

        if (Cam_Cockpit == null)
            GetCockpitCam();

        if (Cam_Mech == null)
            GetMechCam();

        if (Cam_Cockpit != null && Cam_Mech != null)
            UpdatePos();

    }

    void GetDeviceState()
    {

        if (SC_CheckList.Instance.Mng_Device.GetComponent<SC_DeviceManager>().b_IsVR != null)
            b_IsVR = SC_CheckList.Instance.Mng_Device.GetComponent<SC_DeviceManager>().b_IsVR;

        if (SC_CheckList.Instance.Mng_Device.GetComponent<SC_DeviceManager>().b_IsFPS != null)
            b_IsFPS = SC_CheckList.Instance.Mng_Device.GetComponent<SC_DeviceManager>().b_IsFPS;

    }

    void GetCockpitCam()
    {

        if (b_IsFPS && Cam_Cockpit == null)
            Cam_Cockpit = SC_CheckList.Instance.Cam_FPS;

        if (b_IsVR && Cam_Cockpit == null)
            Cam_Cockpit = SC_CheckList.Instance.Cam_VR;

    }

    void GetMechCam()
    {
        Cam_Mech = SC_CheckList.Instance.Cam_Mecha;
    }

    void UpdatePos()
    {

        //Manual
        Vector3 hitCockpit = Cam_Cockpit.GetComponent<SC_raycast>().getRayVector3();

        if (hitCockpit != null)
        {

            //Manual
            hitCockpit = Cam_Mech.transform.rotation * hitCockpit;
            transform.position = Cam_Mech.transform.position + hitCockpit.normalized * f_CrossHairDist;

            transform.LookAt(Cam_Mech.transform);

        }

    }

}
