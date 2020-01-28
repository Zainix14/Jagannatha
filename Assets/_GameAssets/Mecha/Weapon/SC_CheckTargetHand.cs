using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Mettre sur La TargetHand que devront suivre les Weapons |
/// Made by Leni |
/// </summary>
public class SC_CheckTargetHand : MonoBehaviour
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
            Mng_CheckList.GetComponent<SC_CheckList_Weapons>().TargetHand = this.gameObject;
        else
            Debug.LogWarning("SC_CheckTargetLimb - Can't Find Mng_CheckList");

    }
}
