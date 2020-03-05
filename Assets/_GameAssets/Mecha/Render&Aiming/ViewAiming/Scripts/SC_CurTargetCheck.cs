using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CurTargetCheck : MonoBehaviour
{

    public bool b_OnKoa = false;
    public Collider Curtarget = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10 && !b_OnKoa)
        {
            b_OnKoa = true;
            Curtarget = other;
        }        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 10 && b_OnKoa)
        {
            b_OnKoa = false;
        }
    }

}
