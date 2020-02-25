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
    float f_TransImpulseZ;
    float f_TorqueImpulseZ;
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

    float f_lerpTime = 1f;
    float f_currentLerpTime;


    public enum RotationMode { TSR, Torque, Normalize, Higher}
    public RotationMode TypeRotationZ;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!b_InBreakdown && !b_BreakEngine)
            Move();
    }

    void Move()
    {

        //donne une impulsion en proportion à l'axe du joystick
        f_ImpulseX = Input.GetAxis("Vertical") * f_RotationSpeedX;

        f_TorqueImpulseZ = Input.GetAxis("Rotation") * f_RotationSpeedZ;
        f_TransImpulseZ = Input.GetAxis("Horizontal") * f_RotationSpeedZ;


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


        if (f_TorqueImpulseZ != 0 || f_TransImpulseZ != 0)
        {

            Quaternion zQuaternion = new Quaternion();

            /*
            //increment timer once per frame
            f_currentLerpTime += Time.deltaTime;
            if (f_currentLerpTime > f_lerpTime)
                f_currentLerpTime = f_lerpTime;

            //lerp!
            float perc = f_currentLerpTime / f_lerpTime;
            //Debug.Log(f_LerpRotZ + " | " + perc);
            */

            switch (TypeRotationZ)
            {

                case RotationMode.TSR:
                    zQuaternion = Quaternion.AngleAxis(f_TransImpulseZ, Vector3.up);
                    transform.rotation *= Quaternion.Slerp(transform.rotation, zQuaternion, f_LerpRotZ);
                    break;

                case RotationMode.Torque:
                    zQuaternion = Quaternion.AngleAxis(f_TorqueImpulseZ, Vector3.up);
                    transform.rotation *= Quaternion.Lerp(transform.rotation, zQuaternion, f_LerpRotZ);
                    break;

                case RotationMode.Higher:
                    if (f_TorqueImpulseZ >= f_TransImpulseZ)
                        zQuaternion = Quaternion.AngleAxis(f_TorqueImpulseZ, Vector3.up);
                    else
                        zQuaternion = Quaternion.AngleAxis(f_TransImpulseZ, Vector3.up);
                    transform.rotation *= Quaternion.Lerp(transform.rotation, zQuaternion, f_LerpRotZ);
                    break;

                case RotationMode.Normalize:
                    float MixImpulseZ = (Input.GetAxis("Rotation") + Input.GetAxis("Horizontal")) / 2 * f_RotationSpeedZ;
                    zQuaternion = Quaternion.AngleAxis(MixImpulseZ, Vector3.up);
                    transform.rotation *= Quaternion.Lerp(transform.rotation, zQuaternion, f_LerpRotZ);
                    break;

                default:
                    break;

            }

        }
        else if (f_currentLerpTime != 0)
            f_currentLerpTime = 0;

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