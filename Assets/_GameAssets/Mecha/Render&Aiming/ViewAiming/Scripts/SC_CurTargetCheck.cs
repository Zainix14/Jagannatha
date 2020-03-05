using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CurTargetCheck : MonoBehaviour
{

    [SerializeField]
    SC_AimToTarget AimIndicatorSC;

    bool b_OnKoa = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10 && !b_OnKoa)
        {
            b_OnKoa = true;
            AimIndicatorSC.SetTarget(other);
            AimIndicatorSC.b_TargetKoa = true;
        }        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 10 && b_OnKoa)
        {
            b_OnKoa = false;
            AimIndicatorSC.b_TargetKoa = false;
        }
    }

}
