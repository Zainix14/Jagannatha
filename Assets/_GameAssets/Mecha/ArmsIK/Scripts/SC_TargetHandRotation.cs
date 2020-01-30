using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_TargetHandRotation : MonoBehaviour
{

    public enum RotationType { LookAt, SyncRot }

    [Header("Configuration des écrans")]
    public RotationType RotationMode;

    public GameObject TargetLimbRot;
    public Transform AimIndicator;
    public float f_LerpDur = 1;

    Vector3 Vt3_GlobalDir;
    Vector3 Vt3_ScaleDir;
    Vector3 TargetPos;

    Rigidbody ConstraintAnchor;
    ConfigurableJoint ConnectedJoint;

    // Start is called before the first frame update
    void Start()
    {
        ConnectedJoint = this.GetComponent<ConfigurableJoint>();
        ConstraintAnchor = ConnectedJoint.connectedBody;      
    }

    // Update is called once per frame
    void Update()
    {

        if(TargetLimbRot != null && RotationMode == RotationType.SyncRot)
            SetRot();

        if (AimIndicator != null && RotationMode == RotationType.LookAt)
        {
            LookAt();
            Aiming();
        }

    }

    void SetRot()
    {
        this.gameObject.transform.rotation = TargetLimbRot.transform.rotation;
    }

    void LookAt()
    {
        transform.LookAt(AimIndicator);
        transform.rotation *= Quaternion.Euler(0, -90, 90);
    }

    void Aiming()
    {
        Vt3_GlobalDir = AimIndicator.transform.position - ConstraintAnchor.transform.position;
        if(Vt3_GlobalDir.magnitude <= ConnectedJoint.linearLimit.limit )
            TargetPos = ConstraintAnchor.transform.position + Vt3_GlobalDir;
        else
        {
            Vt3_ScaleDir = Vt3_GlobalDir.normalized * ConnectedJoint.linearLimit.limit;
            TargetPos = ConstraintAnchor.transform.position + Vt3_ScaleDir;
        }

        transform.position = Vector3.Lerp(transform.position, TargetPos, f_LerpDur*Time.deltaTime);
    }

}
