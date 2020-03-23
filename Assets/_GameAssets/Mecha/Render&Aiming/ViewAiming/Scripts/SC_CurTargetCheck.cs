using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CurTargetCheck : MonoBehaviour
{

    [Header("Aim Parameters")]
    [SerializeField]
    SC_AimToTarget AimIndicatorSC;
    [SerializeField]
    SC_CrossHairMove CrossHairSC;

    [Header("Target Infos")]
    [SerializeField]
    bool b_OnKoa = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10 && !b_OnKoa)
        {
            b_OnKoa = true;
            AimIndicatorSC.SetTarget(other);
            AimIndicatorSC.b_TargetKoa = true;
            CrossHairSC.SnapTarget = SC_CrossHairMove.Target.Koa;
        }        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 10 && b_OnKoa)
            Reset();
    }

    public void Reset()
    {
        //Debug.Log("reset");
        b_OnKoa = false;
        AimIndicatorSC.SetTarget(null);
        AimIndicatorSC.b_TargetKoa = false;
        CrossHairSC.SnapTarget = SC_CrossHairMove.Target.View;
    }

}
