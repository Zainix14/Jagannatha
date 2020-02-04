using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CheckingMechCollider : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject Mng_CheckList = null;

    // Start is called before the first frame update
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
            Mng_CheckList.GetComponent<SC_CheckList_Mecha>().MechCollider = this.gameObject;
        else
            Debug.LogWarning("SC_TargetCheking - Can't Find Mng_CheckList");

    }
}
