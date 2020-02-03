using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_FollowHand : MonoBehaviour
{

    bool b_AlreadyCheck = false;

    public enum RotationType { LookAt, SyncRot }

    [Header("Configuration des écrans")]
    public RotationType RotationMode;

    GameObject Mng_CheckList = null;
    public GameObject TargetHand;
    public float f_PosOffsetZ = 0;

    // Update is called once per frame
    void Update()
    {

        if (Mng_CheckList == null)
            GetCheckListManager();

        if (Mng_CheckList != null && TargetHand == null)
            GetTargetHand();

    }

    void FixedUpdate()
    {

        if (TargetHand != null && RotationMode == RotationType.SyncRot)
            SetPos();

        if (TargetHand != null && RotationMode == RotationType.LookAt)
            SetPosII();

    }

    void GetCheckListManager()
    {

        Mng_CheckList = GameObject.FindGameObjectWithTag("Mng_CheckList");

        if (Mng_CheckList == null && !b_AlreadyCheck)
            Debug.LogWarning("SC_FollowHand - Can't Find Mng_CheckList");
        if (!b_AlreadyCheck)
            b_AlreadyCheck = true;

    }

    void GetTargetHand()
    {
        TargetHand = Mng_CheckList.GetComponent<SC_CheckList_Weapons>().TargetHand;

        if (TargetHand == null)
            Debug.LogWarning("SC_FollowHand - Can't Find TargetLimb");

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

        this.gameObject.transform.position = new Vector3(TargetHand.transform.position.x, TargetHand.transform.position.y, TargetHand.transform.position.z);
        this.gameObject.transform.position += transform.TransformDirection(0, 0, f_PosOffsetZ);
       
        var rotation = TargetHand.transform.rotation;
        rotation *= Quaternion.Euler(90, 0, 0); // this adds a 90 degrees Y rotation
        this.gameObject.transform.rotation = rotation;

    }

}
