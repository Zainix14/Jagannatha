using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// a mettre sur le NetworkManager chelou qui fait bugé
/// </summary>
public class SC_CheckNetworkManager : MonoBehaviour
{
    GameObject Mng_Checklist;

    // Start is called before the first frame update
    void Start()
    {
        IsCheck();
    }

    void IsCheck()
    {
        Mng_Checklist = GameObject.FindGameObjectWithTag("Mng_CheckList");
        Mng_Checklist.GetComponent<SC_CheckList>().Mng_Network = this.gameObject;
    }
}
