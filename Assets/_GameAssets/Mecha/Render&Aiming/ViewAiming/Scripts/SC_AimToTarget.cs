using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_AimToTarget : MonoBehaviour
{

    [SerializeField]
    GameObject ViewIndicator;
    [SerializeField]
    SC_CurTargetCheck AimIndicSC;

    [Range(0,1)]
    public float LerpFactor;

    public bool b_TargetKoa = false;
    public Transform TargetPos;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetTarget(Collider Target)
    {
        if (Target != null)
            TargetPos = Target.transform;
        else
            TargetPos = null;
    }

    void Move()
    {

        if (!b_TargetKoa)
            this.transform.position = ViewIndicator.transform.position;

        else if (b_TargetKoa)
        {
            if(TargetPos != null)
                this.transform.position = Vector3.Lerp(this.transform.position, TargetPos.position, LerpFactor);
            else
                AimIndicSC.Reset();
        }
           
    }

}
