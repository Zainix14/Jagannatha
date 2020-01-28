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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(TargetLimbRot != null && RotationMode == RotationType.SyncRot)
            SetRot();

        if (AimIndicator != null && RotationMode == RotationType.LookAt)
            LookAt();

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

}
