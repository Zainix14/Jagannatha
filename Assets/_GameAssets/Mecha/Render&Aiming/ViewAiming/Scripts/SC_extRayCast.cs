using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// script sur la caméra écrivant la rendertexture (rendercamera) pour tirer le rayon de visée |
/// Script by Cycy | 
/// </summary>

public class SC_extRayCast : MonoBehaviour
{

    
    bool b_AlreadyCheck = false;
    GameObject Mng_CheckList = null;

    [Header("References")]
    //camera cockpit
    public Camera CockpitCam = null;
    //objet sur lequel est projetté la rendermap
    public GameObject Screens;
    //indicateur 3D placé au point de contact sur le terrain
    public GameObject Indicator; 

    [Header("Device Infos")]
    public bool b_IsVR = false;
    public bool b_IsFPS = false;

    private Vector3 v3_curDir = new Vector3(0, 0, 0);
    private RaycastHit hit;

    void Start()
    {
        GetCheckListManager();
    }


    void Update()
    {

        if (Mng_CheckList == null)
            GetCheckListManager();

        if (Mng_CheckList != null && !b_IsFPS && !b_IsVR)
            GetDeviceState();

        if (CockpitCam == null)
            GetCam();

        /*
        if(CockpitCam != null)  //en Update dans ce script pour l'instant, mais à appeler par un autre script ultérieurement?
            CastRayInWorld();
        */

        if (CockpitCam != null)  //en Update dans ce script pour l'instant, mais à appeler par un autre script ultérieurement?
            VT3ToRay();

    }

    void GetCheckListManager()
    {

        Mng_CheckList = GameObject.FindGameObjectWithTag("Mng_CheckList");

        if (Mng_CheckList == null && !b_AlreadyCheck)
            Debug.LogWarning("SC_extRayCast - Can't Find Mng_CheckList");
        if (!b_AlreadyCheck)
            b_AlreadyCheck = true;

    }

    void GetDeviceState()
    {

        if (Mng_CheckList.GetComponent<SC_CheckList>().Mng_Device.GetComponent<SC_DeviceManager>().b_IsVR != null)
            b_IsVR = Mng_CheckList.GetComponent<SC_CheckList>().Mng_Device.GetComponent<SC_DeviceManager>().b_IsVR;

        if (Mng_CheckList.GetComponent<SC_CheckList>().Mng_Device.GetComponent<SC_DeviceManager>().b_IsFPS != null)
            b_IsFPS = Mng_CheckList.GetComponent<SC_CheckList>().Mng_Device.GetComponent<SC_DeviceManager>().b_IsFPS;

    }

    void GetCam()
    {

        if (b_IsFPS && CockpitCam == null)
            CockpitCam = Mng_CheckList.GetComponent<SC_CheckList>().GetCamFPS();

        if (b_IsVR && CockpitCam == null)
            CockpitCam = Mng_CheckList.GetComponent<SC_CheckList>().GetCamVR();

    }

    /// <summary>
    /// récupère le hit du cockpit et tire un rayon depuis les coordonnées UV de la collision avec l'écran.
    /// </summary>
    void CastRayInWorld()
    {
        //récupération du ray du cockpit
        RaycastHit hitCockpit = CockpitCam.GetComponent<SC_raycast>().getRay();

        //On prend le rayon du point touché du viewport
        Ray ray = GetComponent<Camera>().ViewportPointToRay(new Vector3(hitCockpit.textureCoord.x, hitCockpit.textureCoord.y, 0));
        //Debug.Log("SC_extRayCast - TCoord - " + new Vector3(hitCockpit.textureCoord.x, hitCockpit.textureCoord.y, 0));

        //on le lance
        if (Physics.Raycast(ray, out hit))
        {
            //debug
            //Debug.DrawRay(transform.position, ray.direction * hit.distance, Color.yellow);

            //placement de l'indicateur
            Indicator.transform.position = hit.point;

        }
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
                //Debug.DrawRay(transform.position, hitCockpit * hit.distance, Color.yellow);
                Indicator.transform.position = hit.point;
            }
            
        }
        
    }

}
