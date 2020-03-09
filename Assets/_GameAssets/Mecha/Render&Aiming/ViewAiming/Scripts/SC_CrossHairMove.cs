using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CrossHairMove : MonoBehaviour
{

    [SerializeField]
    bool b_Snapping = true;

    bool b_AlreadyCheck = false;

    public GameObject Mng_CheckList = null;

    public Camera Cam_Mech = null;
    public Camera Cam_Cockpit = null;

    [SerializeField]
    GameObject AimIndicator;
    [SerializeField]
    GameObject ViewIndicator;

    public bool b_IsVR = false;
    public bool b_IsFPS = false;

    public bool b_TargetKoa = false;
    public float f_CrossHairDist = 2f;

    [SerializeField]
    bool b_OnKoa = false;
    [Range(0,2)]
    public float f_DurationLerp = 1f;

    [SerializeField]
    bool b_GoToKoaRun = false;
    [SerializeField]
    bool b_GoToViewRun = false;

    // Start is called before the first frame update
    void Start()
    {
        GetCheckListManager();
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(b_GoToKoaRun);

        if (Mng_CheckList == null)
            GetCheckListManager();

        if (Mng_CheckList != null && !b_IsFPS && !b_IsVR)
            GetDeviceState();

        if (Cam_Cockpit == null)
            GetCockpitCam();

        if (Mng_CheckList != null && Cam_Mech == null)
            GetMechCam();

        if (Cam_Cockpit != null && Cam_Mech != null)
            UpdatePos();

    }

    void GetCheckListManager()
    {

        Mng_CheckList = GameObject.FindGameObjectWithTag("Mng_CheckList");

        if (Mng_CheckList == null && !b_AlreadyCheck)
            Debug.LogWarning("SC_CrossHairMove - Can't Find Mng_CheckList");
        if(!b_AlreadyCheck)
            b_AlreadyCheck = true;

    }

    void GetDeviceState()
    {

        if (Mng_CheckList.GetComponent<SC_CheckList>().Mng_Device.GetComponent<SC_DeviceManager>().b_IsVR != null)
            b_IsVR = Mng_CheckList.GetComponent<SC_CheckList>().Mng_Device.GetComponent<SC_DeviceManager>().b_IsVR;

        if (Mng_CheckList.GetComponent<SC_CheckList>().Mng_Device.GetComponent<SC_DeviceManager>().b_IsFPS != null)
            b_IsFPS = Mng_CheckList.GetComponent<SC_CheckList>().Mng_Device.GetComponent<SC_DeviceManager>().b_IsFPS;

    }

    void GetCockpitCam()
    {

        if (b_IsFPS && Cam_Cockpit == null)
            Cam_Cockpit = Mng_CheckList.GetComponent<SC_CheckList>().GetCamFPS();

        if (b_IsVR && Cam_Cockpit == null)
            Cam_Cockpit = Mng_CheckList.GetComponent<SC_CheckList>().GetCamVR();

    }

    void GetMechCam()
    {
        Cam_Mech = Mng_CheckList.GetComponent<SC_CheckList>().GetCamMecha();
    }

    void UpdatePos()
    {

        //Manual
        Vector3 hitCockpit = Cam_Cockpit.GetComponent<SC_raycast>().getRayVector3();

        if (hitCockpit != null)
        {

            if (b_TargetKoa && b_OnKoa && b_Snapping)
            {
                //Snap
                hitCockpit = AimIndicator.transform.position - Cam_Mech.transform.position;
                transform.position = Cam_Mech.transform.position + hitCockpit.normalized * f_CrossHairDist;
            }
            else if ( (!b_TargetKoa && !b_OnKoa) || !b_Snapping)
            {
                //Manual
                hitCockpit = Cam_Mech.transform.rotation * hitCockpit;
                transform.position = Cam_Mech.transform.position + hitCockpit.normalized * f_CrossHairDist;
            }

            transform.LookAt(Cam_Mech.transform);

        }

    }

    /// <summary>
    /// 0 = ToView
    /// 1 = ToKoa
    /// </summary>
    /// <param name="nCor"></param>
    public void GoTo(int nCor)
    {
        if(b_Snapping)
            switch (nCor)
            {
                case 0:
                    if(!b_GoToViewRun)
                        StartCoroutine(GoToView());
                    break;

                case 1:
                    if (!b_GoToKoaRun)
                        StartCoroutine(GoToKoa());
                    break;
            }
    }

    IEnumerator GoToKoa()
    {

        //Debug.Log("GoToKoa");
        b_GoToKoaRun = true;

        float i = 0.0f;
        float rate = 1.0f / f_DurationLerp;

        while (i < 1.0)
        {

            i += Time.deltaTime * rate;

            Vector3 hitCockpit = AimIndicator.transform.position - Cam_Mech.transform.position;
            transform.position = Vector3.Lerp(transform.position, Cam_Mech.transform.position + hitCockpit.normalized * f_CrossHairDist, i);

            yield return 0;

        }

        b_OnKoa = true;
        b_GoToKoaRun = false;
        
    }

    IEnumerator GoToView()
    {

        //Debug.Log("GoToView");
        b_GoToViewRun = true;

        float i = 0.0f;
        float rate = 1.0f / f_DurationLerp;

        while (i < 1.0)
        {

            i += Time.deltaTime * rate;

            Vector3 hitCockpit = ViewIndicator.transform.position - Cam_Mech.transform.position;
            transform.position = Vector3.Lerp(transform.position, Cam_Mech.transform.position + hitCockpit.normalized * f_CrossHairDist, i);

            yield return 0;

        }

        b_OnKoa = false;
        b_GoToViewRun = false;

    }

}
