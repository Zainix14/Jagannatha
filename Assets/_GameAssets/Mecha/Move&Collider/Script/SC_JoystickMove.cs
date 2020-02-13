using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_JoystickMove : MonoBehaviour, IF_BreakdownSystem
{
    [HideInInspector]
    bool b_InBreakdown = false;
    bool b_BreakEngine = false;

    [SerializeField]
    float f_RotationSpeedH = 1.0f;
    [SerializeField]
    float f_RotationSpeedV = 1.0f;
    [SerializeField]
    float f_MaxRotSpeed;

    float f_MaxRotH = 0.4f;
    public float f_LerpRotH = 1f;
    public float f_LerpRotV = 1f;

    public Transform TargetTRS;

    Quaternion OriginalRotH;

    // Start is called before the first frame update
    void Start()
    {
        OriginalRotH = TargetTRS.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (!b_InBreakdown && !b_BreakEngine)
            Move();
    }

    void Move()
    {

        //donne une impulsion en proportion à l'axe du joystick
        float f_ImpulseZ = (Input.GetAxis("Vertical") * f_MaxRotH) * f_RotationSpeedV;
        float f_ImpulseX = Input.GetAxis("Horizontal") * f_RotationSpeedH;

        //Rotation V
        if (f_ImpulseZ != 0)
        {
            float rotationY = f_RotationSpeedV * f_ImpulseZ;
            Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, Vector3.left);
            Quaternion targetRot = OriginalRotH * yQuaternion;
            //TargetTRS.localRotation = yQuaternion;
            if (targetRot.x < OriginalRotH.x)
                TargetTRS.localRotation = Quaternion.Lerp(TargetTRS.localRotation, yQuaternion, f_LerpRotH);
            else if (targetRot.x > OriginalRotH.x)
                TargetTRS.localRotation = Quaternion.Lerp(TargetTRS.localRotation, OriginalRotH, f_LerpRotH);
        }

        //Rotation H
        if (f_ImpulseX != 0)
        {
            Quaternion yQuaternion = Quaternion.AngleAxis(f_ImpulseX, Vector3.up);
            transform.rotation *= Quaternion.Lerp(transform.rotation, yQuaternion, f_LerpRotV);
        }

    }

    public void SetBreakdownState(bool State)
    {
        b_InBreakdown = State;
    }

    public void SetEngineBreakdownState(bool State)
    {
        b_BreakEngine = State;
    }
}