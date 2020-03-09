using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_JoystickRenderMove : MonoBehaviour
{

    float f_ImpulseX;
    float f_TransImpulseZ;
    float f_TorqueImpulseZ;

    [Range(0,90)]
    public float f_MaxRotH;
    [Range(0, 90)]
    public float f_MaxRotV;
    [Range(0, 90)]
    public float f_MaxRotT;

    float f_CurRotH;
    float f_CurRotV;
    float f_CurRotT;

    [SerializeField]
    Transform TargetTRS;

    void Update()
    {
        GetRotation();
        ApplyRot();
    }

    void GetRotation()
    {

        f_TransImpulseZ = Input.GetAxis("Horizontal");
        f_ImpulseX = Input.GetAxis("Vertical");
        f_TorqueImpulseZ = Input.GetAxis("Torque");

        f_CurRotH = (Mathf.Abs(f_TransImpulseZ) * f_MaxRotH / 1) * Mathf.Sign(f_TransImpulseZ);
        f_CurRotT = (Mathf.Abs(f_TorqueImpulseZ) * f_MaxRotT / 1) * Mathf.Sign(f_TorqueImpulseZ);
        f_CurRotV = (Mathf.Abs(f_ImpulseX) * f_MaxRotV / 1) * Mathf.Sign(f_ImpulseX);

        Debug.Log((-90 + f_CurRotV) + " - " + f_MaxRotT + " - " + f_CurRotH);

    }

    void ApplyRot()
    {
        //TargetTRS.transform.Rotate(-90 + f_CurRotV, f_MaxRotT, f_CurRotH, Space.Self); 
        Quaternion targetRot = new Quaternion();
        targetRot.eulerAngles = new Vector3(-90 - f_CurRotH, f_CurRotT, f_CurRotV);
        //TargetTRS.localRotation = targetRot;

    }

}
