using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_JoystickMove : MonoBehaviour, IF_BreakdownSystem
{

    public Transform TargetTRS;

    [HideInInspector]
    bool b_InBreakdown = false;
    bool b_BreakEngine = false;

    public bool b_OnTorque = false;
    float f_ImpulseZ;
    [SerializeField]
    float f_RotationSpeedZ = 1.0f;
    public float f_LerpRotZ = 1f;

    public bool b_InvertAxe = false;
    float f_ImpulseX;
    [SerializeField]
    float f_RotationSpeedX = 1.0f;
    public float f_LerpRotX = 1f;
    [Range(0.0f, 0.3f)]
    public float f_MaxRotUpX;
    Quaternion xQuaternion;

    // Update is called once per frame
    void Update()
    {
        if (!b_InBreakdown && !b_BreakEngine)
            Move();
    }

    void Move()
    {

        //donne une impulsion en proportion à l'axe du joystick
        f_ImpulseX = Input.GetAxis("Vertical") * f_RotationSpeedX;

        if(b_OnTorque)
            f_ImpulseZ = Input.GetAxis("Rotation") * f_RotationSpeedZ;
        else
            f_ImpulseZ = Input.GetAxis("Horizontal") * f_RotationSpeedZ;


        //Rotation X
        if (f_ImpulseX != 0)
        {

            if (!b_InvertAxe)
            {

                xQuaternion = Quaternion.AngleAxis(f_ImpulseX, Vector3.left);

                if (f_ImpulseX > 0 && TargetTRS.localRotation.x > -f_MaxRotUpX)
                    TargetTRS.localRotation *= Quaternion.Lerp(TargetTRS.localRotation, xQuaternion, f_LerpRotX);

                if (f_ImpulseX < 0 && TargetTRS.localRotation.x < 0)
                    TargetTRS.localRotation *= Quaternion.Lerp(TargetTRS.localRotation, xQuaternion, f_LerpRotX);

            }
                

            if (b_InvertAxe)
            {

                xQuaternion = Quaternion.AngleAxis(-f_ImpulseX, Vector3.left);

                if (f_ImpulseX > 0 && TargetTRS.localRotation.x < 0)
                    TargetTRS.localRotation *= Quaternion.Lerp(TargetTRS.localRotation, xQuaternion, f_LerpRotX);

                if (f_ImpulseX < 0 && TargetTRS.localRotation.x > -f_MaxRotUpX)
                    TargetTRS.localRotation *= Quaternion.Lerp(TargetTRS.localRotation, xQuaternion, f_LerpRotX);

            }

        }

        //Rotation Z
        if (f_ImpulseZ != 0)
        {
            Quaternion zQuaternion = Quaternion.AngleAxis(f_ImpulseZ, Vector3.up);
            transform.rotation *= Quaternion.Lerp(transform.rotation, zQuaternion, f_LerpRotZ);
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