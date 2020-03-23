using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_SyncRot : MonoBehaviour
{

    [Header("References")]
    public Transform Target;

    // Update is called once per frame
    void Update()
    {
        if (Target != null)
            this.transform.rotation = Target.rotation;
    }

}