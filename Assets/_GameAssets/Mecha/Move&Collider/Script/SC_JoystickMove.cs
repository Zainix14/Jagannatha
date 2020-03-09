using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_JoystickMove : MonoBehaviour, IF_BreakdownSystem
{

    //Breakdown Infos
    [Header("Breakdown Infos")]
    [SerializeField]
    bool b_InBreakdown = false;
    [SerializeField]
    bool b_BreakEngine = false;

    //Rotation Horizontale
    [Header("Horizontal Rotation Settings")]
    [SerializeField]
    float f_RotationSpeedZ = 1.0f;
    [SerializeField]
    float f_LerpRotZ = 1f;  
    public enum RotationMode { TSR, Torque, Normalize, Higher, Clamp }
    public RotationMode TypeRotationZ;
    float f_TransImpulseZ;
    float f_TorqueImpulseZ;

    //Rotation Verticale
    [Header("Vertical Rotation Settings")]
    [SerializeField]
    bool b_InvertAxe = false;  
    [SerializeField]
    Transform TargetTRS;
    [Range(0.0f, 1.0f)]
    public float f_RotationSpeedX = 0.5f;
    [Range(0.0f, 1.0f)]
    public float f_LerpRotX = 1f;
    [Range(0.0f, 0.3f)]
    public float f_MaxRotUpX;
    float f_ImpulseX;
    Quaternion xQuaternion;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!b_InBreakdown && !b_BreakEngine)
            Move();
    }

    void Move()
    {

        #region Rotation Verticale

        f_ImpulseX = Input.GetAxis("Vertical") * f_RotationSpeedX;

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

            else if (b_InvertAxe)
            {

                xQuaternion = Quaternion.AngleAxis(-f_ImpulseX, Vector3.left);

                if (f_ImpulseX > 0 && TargetTRS.localRotation.x < 0)
                    TargetTRS.localRotation *= Quaternion.Lerp(TargetTRS.localRotation, xQuaternion, f_LerpRotX);

                if (f_ImpulseX < 0 && TargetTRS.localRotation.x > -f_MaxRotUpX)
                    TargetTRS.localRotation *= Quaternion.Lerp(TargetTRS.localRotation, xQuaternion, f_LerpRotX);

            }

        }

        #endregion

        #region Rotation Horizontale

        f_TorqueImpulseZ = Input.GetAxis("Rotation") * f_RotationSpeedZ;
        f_TransImpulseZ = Input.GetAxis("Horizontal") * f_RotationSpeedZ;

        Debug.Log(Input.GetAxis("Rotation"));
        Debug.Log(Input.GetAxis("Horizontal"));

        if (f_TorqueImpulseZ != 0 || f_TransImpulseZ != 0)
        {

            Quaternion zQuaternion = new Quaternion();
            float MixImpulseZ;

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
                    float absTorque = Mathf.Abs(f_TorqueImpulseZ);
                    float absTrans = Mathf.Abs(f_TransImpulseZ);
                    if (absTorque >= absTrans)
                        zQuaternion = Quaternion.AngleAxis(f_TorqueImpulseZ, Vector3.up);
                    else
                        zQuaternion = Quaternion.AngleAxis(f_TransImpulseZ, Vector3.up);
                    transform.rotation *= Quaternion.Lerp(transform.rotation, zQuaternion, f_LerpRotZ);
                    break;

                case RotationMode.Normalize:
                    MixImpulseZ = (Input.GetAxis("Rotation") + Input.GetAxis("Horizontal")) / 2 * f_RotationSpeedZ;
                    zQuaternion = Quaternion.AngleAxis(MixImpulseZ, Vector3.up);
                    transform.rotation *= Quaternion.Lerp(transform.rotation, zQuaternion, f_LerpRotZ);
                    break;

                case RotationMode.Clamp:
                    MixImpulseZ = Input.GetAxis("Rotation") + Input.GetAxis("Horizontal");
                    if (MixImpulseZ > 1)
                        MixImpulseZ = 1;
                    MixImpulseZ *= f_RotationSpeedZ;
                    zQuaternion = Quaternion.AngleAxis(MixImpulseZ, Vector3.up);
                    transform.rotation *= Quaternion.Lerp(transform.rotation, zQuaternion, f_LerpRotZ);
                    break;

                default:
                    break;

            }

        }

        #endregion

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