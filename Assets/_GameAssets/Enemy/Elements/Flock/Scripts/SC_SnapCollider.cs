using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_SnapCollider : MonoBehaviour
{

    GameObject Mng_CheckList;
    Camera MechCam;

    // Start is called before the first frame update
    void Start()
    {
        GetReferences();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mng_CheckList == null || MechCam == null)
            GetReferences();

        if (MechCam != null)
            LookAtMech();
    }

    void GetReferences()
    {
        if (Mng_CheckList == null)
            Mng_CheckList = GameObject.FindGameObjectWithTag("Mng_CheckList");
        if(Mng_CheckList != null && MechCam == null)
            MechCam = Mng_CheckList.GetComponent<SC_CheckList>().GetCamMecha();
    }

    void LookAtMech()
    {
        this.transform.LookAt(MechCam.transform);
    }

}
