using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Permet a l'objet Target (les Screens) de se declaré |
/// Made by Leni |
/// </summary>
public class SC_TargetCheking : MonoBehaviour
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
            Mng_CheckList.GetComponent<SC_CheckList_ViewAiming>().Target = this.gameObject;
        else
            Debug.LogWarning("SC_TargetCheking - Can't Find Mng_CheckList");

    }

}
