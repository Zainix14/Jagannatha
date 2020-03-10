using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CrossHairMove : MonoBehaviour
{

    [Header("References")]
    [SerializeField]
    bool b_RefsGet = false;   
    [SerializeField]
    GameObject AimIndicator;
    [SerializeField]
    GameObject ViewIndicator;
    GameObject Mng_CheckList = null;
    Camera Cam_Mech = null;
    Camera Cam_Cockpit = null;
    bool b_IsVR = false;
    bool b_IsFPS = false;

    [Header("CrossHair Parameters")]
    public float f_CrossHairDist = 5f;

    [Header("Snap Infos")]
    [SerializeField]
    bool b_Snapping = true;
    [SerializeField]
    public bool b_TargetKoa = false;
    [SerializeField]
    bool b_OnKoa = false;

    [Header("Snap Coroutine Infos")]
    [Range(0, 2)]
    public float f_Duration = 1.0f;
    [SerializeField]
    bool b_GoToKoaRun = false; 
    [SerializeField]
    bool b_GoToViewRun = false; 
    [SerializeField]
    AnimationCurve SnapCurve;

    // Start is called before the first frame update
    void Start()
    {
        GetReferences();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!b_RefsGet)
            GetReferences();

        if (Cam_Cockpit != null && Cam_Mech != null)
            UpdatePos();

    }

    #region GetReferences

    void GetReferences()
    {
        if(Mng_CheckList == null)
            Mng_CheckList = GameObject.FindGameObjectWithTag("Mng_CheckList");
        if (Mng_CheckList != null && (!b_IsVR && !b_IsFPS))
            GetDeviceState();
        if (Mng_CheckList != null && Cam_Cockpit == null && (b_IsFPS || b_IsVR))
            GetCockpitCam();
        if (Mng_CheckList != null && Cam_Mech == null)
            GetMechCam();
        if (Cam_Cockpit != null && Cam_Mech != null)
            b_RefsGet = true;
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

    #endregion

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

        b_GoToKoaRun = true;

        float t = 0.0f;
        float rate = 1.0f / f_Duration;

        while (t < 1.0)
        {

            t += Time.deltaTime * rate;
            float Lerp = SnapCurve.Evaluate(t);

            Vector3 hitCockpit = AimIndicator.transform.position - Cam_Mech.transform.position;
            transform.position = Vector3.Lerp(transform.position, Cam_Mech.transform.position + hitCockpit.normalized * f_CrossHairDist, Lerp);

            yield return 0;

        }

        b_OnKoa = true;
        b_GoToKoaRun = false;
        
    }

    IEnumerator GoToView()
    {

        b_GoToViewRun = true;

        float t = 0.0f;
        float rate = 1.0f / f_Duration;

        while (t < 1.0)
        {

            t += Time.deltaTime * rate;
            float Lerp = SnapCurve.Evaluate(t);

            Vector3 hitCockpit = ViewIndicator.transform.position - Cam_Mech.transform.position;
            transform.position = Vector3.Lerp(transform.position, Cam_Mech.transform.position + hitCockpit.normalized * f_CrossHairDist, Lerp);

            yield return 0;

        }

        b_OnKoa = false;
        b_GoToViewRun = false;

    }

}
