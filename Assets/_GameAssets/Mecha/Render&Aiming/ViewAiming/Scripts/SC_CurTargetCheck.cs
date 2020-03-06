using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CurTargetCheck : MonoBehaviour
{

    [SerializeField]
    SC_AimToTarget AimIndicatorSC;
    [SerializeField]
    SC_CrossHairMove CrossHairSC;

    bool b_OnKoa = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10 && !b_OnKoa)
        {
            b_OnKoa = true;
            AimIndicatorSC.SetTarget(other);
            AimIndicatorSC.b_TargetKoa = true;
            CrossHairSC.b_TargetKoa = true;
            CrossHairSC.GoTo(1);
        }        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 10 && b_OnKoa)
            Reset();
    }

    public void Reset()
    {      
        b_OnKoa = false;
        AimIndicatorSC.SetTarget(null);
        AimIndicatorSC.b_TargetKoa = false;
        CrossHairSC.b_TargetKoa = false;
        CrossHairSC.GoTo(0);
    }

}
