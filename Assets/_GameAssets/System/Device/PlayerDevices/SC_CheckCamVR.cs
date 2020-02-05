using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CheckCamVR : MonoBehaviour
{

    GameObject Mng_CheckList = null;

    // Start is called before the first frame update
    void Start()
    {
        IsCheck();
    }

    void IsCheck()
    {
        Mng_CheckList = GameObject.FindGameObjectWithTag("Mng_CheckList");
        Mng_CheckList.GetComponent<SC_CheckList>().Cam_VR = this.gameObject.GetComponent<Camera>();
    }

}
