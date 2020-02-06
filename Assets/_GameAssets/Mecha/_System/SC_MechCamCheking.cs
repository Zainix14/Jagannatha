using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Permet a La Camera de Render de se déclaré |
/// Made by Leni |
/// </summary>
public class SC_MechCamCheking : MonoBehaviour
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

        if (Mng_CheckList != null)
            Mng_CheckList.GetComponent<SC_CheckList>().Cam_Mecha = this.gameObject.GetComponent<Camera>();
        else
            Debug.LogWarning("SC_MechCamCheking - Can't Find Mng_CheckList");

    }
}
