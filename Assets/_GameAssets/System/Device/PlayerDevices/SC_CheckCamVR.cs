using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CheckCamVR : MonoBehaviour
{

    GameObject Mng_CheckList = null;

    void Start()
    {
        GetReferences();
    }

    void Update()
    {
        if (Mng_CheckList == null)
            GetReferences();
    }
    void GetReferences()
    {
        Mng_CheckList = GameObject.FindGameObjectWithTag("Mng_CheckList");

        if (Mng_CheckList != null) IsCheck();
    }

    void IsCheck()
    {
        if (Mng_CheckList != null)
            Mng_CheckList.GetComponent<SC_CheckList>().Cam_VR = this.gameObject.GetComponent<Camera>();
        else
            Debug.LogWarning("SC_TargetCheking - Can't Find Mng_CheckList");
    }

}
