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
    float f_RotationSpeedX = 1.0f;
    [SerializeField]
    float f_MaxRotSpeed;

    float f_MaxRotH = 0.4f;
    public float f_LerpRotX = 1f;
    public float f_LerpRotZ = 1f;

    public Transform TargetTRS;

    Quaternion OriginalRotX;

    // Start is called before the first frame update
    void Start()
    {
        OriginalRotX = TargetTRS.transform.rotation;
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
        float f_ImpulseX = (Input.GetAxis("Vertical") * f_MaxRotH) * f_RotationSpeedX;
        float f_ImpulseZ = Input.GetAxis("Horizontal") * f_RotationSpeedH;

        //Rotation X
        if (f_ImpulseX != 0)
        {
            float rotationX = f_RotationSpeedX * f_ImpulseX;
            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.left);
            Quaternion targetRot = OriginalRotX * xQuaternion;
            //TargetTRS.localRotation = yQuaternion;
            if (targetRot.x < OriginalRotX.x)
                TargetTRS.localRotation = Quaternion.Lerp(TargetTRS.localRotation, xQuaternion, f_LerpRotX);
            else if (targetRot.x > OriginalRotX.x)
                TargetTRS.localRotation = Quaternion.Lerp(TargetTRS.localRotation, OriginalRotX, f_LerpRotX);
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