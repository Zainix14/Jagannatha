using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_FollowHand : MonoBehaviour
{

    bool b_AlreadyCheck = false;
    public bool b_OnFollow = false;

    public enum RotationType { LookAt, SyncRot, Lerp }

    [Header("Configuration des écrans")]
    public RotationType RotationMode;

    GameObject Mng_CheckList = null;
    public GameObject TargetHand;
    public float f_PosOffsetZ = 0;
    public GameObject AimIndicator;

    void GetReferences()
    {
        if (Mng_CheckList == null)
            Mng_CheckList = GameObject.FindGameObjectWithTag("Mng_CheckList");
        if (AimIndicator == null && Mng_CheckList != null)
            AimIndicator = Mng_CheckList.GetComponent<SC_CheckList_Weapons>().GetAimIndicator();
    }

    void Update()
    {

        if (Mng_CheckList == null || AimIndicator == null)
            GetReferences();

        if (Mng_CheckList != null && TargetHand == null)
            GetTargetHand();

        if (b_OnFollow && TargetHand != null && RotationMode == RotationType.SyncRot)
            SetPos();

        if (b_OnFollow && TargetHand != null && RotationMode == RotationType.LookAt)
            SetPosII();

        if (b_OnFollow && TargetHand != null && RotationMode == RotationType.Lerp)
            SetPosIII();

        if (!b_OnFollow && this.transform.position.y >= 0)
            ResetPos();

    }

    void GetTargetHand()
    {
        TargetHand = Mng_CheckList.GetComponent<SC_CheckList_Weapons>().TargetHand;

        if (TargetHand == null)
            Debug.LogWarning("SC_FollowHand - Can't Find TargetLimb");

    }

    void ResetPos()
    {
        this.transform.position = new Vector3(0, -100, 0);
    }

    void SetPos()
    {

        this.gameObject.transform.position = new Vector3(TargetHand.transform.position.x, TargetHand.transform.position.y, TargetHand.transform.position.z);
        this.gameObject.transform.position += transform.TransformDirection(0, 0, f_PosOffsetZ);

        var rotation = TargetHand.transform.rotation;
        rotation *= Quaternion.Euler(90, 0, 0); // this adds a 90 degrees Y rotation
        this.gameObject.transform.rotation = rotation;

    }

    void SetPosII()
    {
     
        Vector3 TargetPos = new Vector3(TargetHand.transform.position.x, TargetHand.transform.position.y, TargetHand.transform.position.z);
        this.gameObject.transform.position = Vector3.Lerp(transform.position, TargetPos, 1f);
        this.gameObject.transform.position += transform.TransformDirection(0, 0, f_PosOffsetZ);

        /*
        var rotation = TargetHand.transform.rotation;
        rotation *= Quaternion.Euler(90, 0, 0); // this adds a 90 degrees Y rotation
        this.gameObject.transform.rotation = rotation;
        */

        transform.LookAt(AimIndicator.transform);

    }

    void SetPosIII()
    {

        var TargetPos = new Vector3(TargetHand.transform.position.x, TargetHand.transform.position.y, TargetHand.transform.position.z);
        TargetPos += transform.TransformDirection(0, 0, f_PosOffsetZ);
        this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, TargetPos, 0.1f);

        var rotation = TargetHand.transform.rotation;
        rotation *= Quaternion.Euler(90, 0, 0); // this adds a 90 degrees Y rotation
        this.gameObject.transform.rotation = Quaternion.Lerp(this.gameObject.transform.rotation, rotation, 0.1f); ;

    }

}
