using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CamCockpitDemo : MonoBehaviour, IF_CamCockpitDemo
{

    public Vector3 InitPos;
    public Vector3 TargetPos;
    public float LerpTime;

    public Vector3 GetInitVt3()
    {
        return InitPos;
    }

    public Vector3 GetTargetVt3()
    {
        return TargetPos;
    }

    public float GetLerpTime()
    {
        return LerpTime;
    }

}
