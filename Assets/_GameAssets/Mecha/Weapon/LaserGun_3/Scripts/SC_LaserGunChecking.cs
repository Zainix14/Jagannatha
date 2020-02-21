using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_LaserGunChecking : MonoBehaviour
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
            Mng_CheckList.GetComponent<SC_CheckList_Weapons>().LaserGun = this.gameObject;
        else
            Debug.LogWarning("SC_LaserGunChecking - Can't Find Mng_CheckList");
    }
}
