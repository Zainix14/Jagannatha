using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_SyncPos : MonoBehaviour
{

    public Transform Target;
    public Rigidbody RbTarget;

    // Update is called once per frame
    void LateUpdate()
    {
        if(Target != null)
        {
            //this.transform.position = Target.position;
            //this.transform.rotation = Target.rotation;
            //this.transform.rotation *= Quaternion.Euler(0,180,0);
        }

        if (RbTarget != null)
        {
            this.transform.position = RbTarget.position;
            this.transform.rotation = RbTarget.rotation;
            //this.transform.rotation *= Quaternion.Euler(0,180,0);
        }

    }
}
