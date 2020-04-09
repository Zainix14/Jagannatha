using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// script sur la caméra écrivant la rendertexture (rendercamera) pour tirer le rayon de visée |
/// Script by Cycy | 
/// </summary>

public class SC_extRayCast : MonoBehaviour
{

    [Header("References")]
    //camera cockpit
    public Camera CockpitCam = null;
    //objet sur lequel est projetté la rendermap
    public GameObject Screens;
    //indicateur 3D placé au point de contact sur le terrain
    public GameObject Indicator; 

    [Header("Infos")]
    public bool b_IsVR = false;
    public bool b_IsFPS = false;

    private Vector3 v3_curDir = new Vector3(0, 0, 0);
    private RaycastHit hit;

    void Update()
    {

        if (!b_IsFPS && !b_IsVR)
            GetDeviceState();

        if (CockpitCam == null)
            GetCam();

        if (CockpitCam != null)  //en Update dans ce script pour l'instant, mais à appeler par un autre script ultérieurement?
            VT3ToRay();

    }

    void GetDeviceState()
    {

        if (SC_CheckList.Instance.Mng_Device.GetComponent<SC_DeviceManager>().b_IsVR != null)
            b_IsVR = SC_CheckList.Instance.Mng_Device.GetComponent<SC_DeviceManager>().b_IsVR;

        if (SC_CheckList.Instance.Mng_Device.GetComponent<SC_DeviceManager>().b_IsFPS != null)
            b_IsFPS = SC_CheckList.Instance.Mng_Device.GetComponent<SC_DeviceManager>().b_IsFPS;

    }

    void GetCam()
    {

        if (b_IsFPS && CockpitCam == null)
            CockpitCam = SC_CheckList.Instance.Cam_FPS;

        if (b_IsVR && CockpitCam == null)
            CockpitCam = SC_CheckList.Instance.Cam_VR;

    }

    void VT3ToRay()
    {

        Vector3 hitCockpit = CockpitCam.GetComponent<SC_raycast>().getRayVector3();
        
        if (hitCockpit != null)
        {

            hitCockpit = transform.rotation * hitCockpit;

            int layerMask = 1 << 21 | 1 << 11 | 1 << 20 | 1 << 23 | 1 << 27;
            layerMask = ~layerMask;
            if (Physics.Raycast(transform.position, hitCockpit, out hit, 5000f, layerMask))
            {
                Indicator.transform.position = hit.point;
            }
            
        }
        
    }

}
