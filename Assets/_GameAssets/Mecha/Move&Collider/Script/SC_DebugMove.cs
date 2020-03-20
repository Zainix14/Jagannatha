using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_DebugMove : MonoBehaviour, IF_BreakdownSystem
{

    #region Singleton

    private static SC_DebugMove _instance;
    public static SC_DebugMove Instance { get { return _instance; } }

    #endregion

    [Header("Breakdown Infos")]
    [SerializeField]
    bool b_InBreakdown = false;
    [SerializeField]
    bool b_BreakEngine = false;
    [SerializeField, Range(0, 3)]
    int n_BreakDownLvl = 0;

    [Header("Rotation Infos")]
    public float f_Speed = 10f;
    public float f_RotFactor = 1f;
    Rigidbody rb;
    [SerializeField]
    float f_RotationSpeedZ = 1.0f;
    public float f_LerpRotZ = 1f;

    public Transform TargetTRS;
    [Range(0.0f, 0.3f)]
    public float f_MaxRotUpX;
    Quaternion xQuaternion;
    [SerializeField]
    float f_RotationSpeedX = 1.0f;
    public float f_LerpRotX = 1f;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!b_InBreakdown && !b_BreakEngine)
            Move();
    }

    void Move()
    {

        Quaternion zQuaternion = new Quaternion();

        /*
        if (Input.GetKey(KeyCode.R))
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * f_Speed, ForceMode.Impulse);
        }
        else if (Input.GetKey(KeyCode.F))
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * (f_Speed * -1), ForceMode.Impulse);
        }
        */

        if (Input.GetKey(KeyCode.Q))
        {
            //rb.AddTorque(transform.up * (f_Speed * f_RotFactor * -1), ForceMode.Force);
            zQuaternion = Quaternion.AngleAxis(-f_RotationSpeedZ, Vector3.up);
            transform.rotation *= Quaternion.Slerp(transform.rotation, zQuaternion, f_LerpRotZ);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            //rb.AddTorque(transform.up * f_Speed * f_RotFactor, ForceMode.Force);
            zQuaternion = Quaternion.AngleAxis(f_RotationSpeedZ, Vector3.up);
            transform.rotation *= Quaternion.Slerp(transform.rotation, zQuaternion, f_LerpRotZ);
        }        

        if (Input.GetKey(KeyCode.Z) && TargetTRS.localRotation.x > -f_MaxRotUpX)
        {
            xQuaternion = Quaternion.AngleAxis(f_RotationSpeedX, Vector3.left);
            TargetTRS.localRotation *= Quaternion.Lerp(TargetTRS.localRotation, xQuaternion, f_LerpRotX);
        }
            

        if (Input.GetKey(KeyCode.S) && TargetTRS.localRotation.x < 0)
        {
            xQuaternion = Quaternion.AngleAxis(-f_RotationSpeedX, Vector3.left);
            TargetTRS.localRotation *= Quaternion.Lerp(TargetTRS.localRotation, xQuaternion, f_LerpRotX);
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
    public void AlignBreakdownLevel(int n_Level)
    {
        n_BreakDownLvl = n_Level;
    }

}
