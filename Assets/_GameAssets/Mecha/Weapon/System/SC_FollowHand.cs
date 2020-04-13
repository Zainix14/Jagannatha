using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_FollowHand : MonoBehaviour
{

    [Header("References")]
    [SerializeField]
    GameObject TargetHand;
    [SerializeField]
    GameObject AimIndicator;   

    [Header("Parameters")]
    [SerializeField]
    float f_PosOffsetZ = 0;
    public enum RotationType { LookAt, SyncRot, Lerp }  
    public RotationType RotationMode;

    [Header("Infos")]
    bool b_AlreadyCheck = false;
    public bool b_OnFollow = false;

    void GetReferences()
    {

        if (AimIndicator == null)
            AimIndicator = SC_CheckList_Weapons.Instance.AimIndicator;

        if (TargetHand == null)
            TargetHand = SC_CheckList_Weapons.Instance.TargetHand;

    }

    void Update()
    {

        if (TargetHand == null || AimIndicator == null)
            GetReferences();

        if (b_OnFollow && TargetHand != null)
            SetPos();

        if (!b_OnFollow && this.transform.position.y >= 0)
            ResetPos();

    }

    void ResetPos()
    {
        this.transform.position = new Vector3(0, -100, 0);
    }

    void SetPos()
    {

        switch (RotationMode)
        {

            case RotationType.SyncRot:

                this.gameObject.transform.position = new Vector3(TargetHand.transform.position.x, TargetHand.transform.position.y, TargetHand.transform.position.z);
                this.gameObject.transform.position += transform.TransformDirection(0, 0, f_PosOffsetZ);

                var rotation = TargetHand.transform.rotation;
                rotation *= Quaternion.Euler(90, 0, 0); // this adds a 90 degrees Y rotation
                this.gameObject.transform.rotation = rotation;

                break;

            case RotationType.LookAt:

                Vector3 TargetPos = new Vector3(TargetHand.transform.position.x, TargetHand.transform.position.y, TargetHand.transform.position.z);
                this.gameObject.transform.position = Vector3.Lerp(transform.position, TargetPos, 1f);
                this.gameObject.transform.position += transform.TransformDirection(0, 0, f_PosOffsetZ);

                transform.LookAt(AimIndicator.transform);

                break;

            case RotationType.Lerp:

                var TargetPosLerp = new Vector3(TargetHand.transform.position.x, TargetHand.transform.position.y, TargetHand.transform.position.z);
                TargetPosLerp += transform.TransformDirection(0, 0, f_PosOffsetZ);
                this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, TargetPosLerp, 0.1f);

                var RotationLerp = TargetHand.transform.rotation;
                RotationLerp *= Quaternion.Euler(90, 0, 0); // this adds a 90 degrees Y rotation
                this.gameObject.transform.rotation = Quaternion.Lerp(this.gameObject.transform.rotation, RotationLerp, 0.1f);

                break;
        }

    }

}
