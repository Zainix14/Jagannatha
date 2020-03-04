using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_SyncPos : MonoBehaviour
{

    public Transform Target;
    public Rigidbody RbTarget;

    float f_lerpTime = 1f;
    float f_currentLerpTime;

    // Update is called once per frame
    void LateUpdate()
    {

        //increment timer once per frame
        f_currentLerpTime += Time.deltaTime;
        if (f_currentLerpTime > f_lerpTime)
            f_currentLerpTime = f_lerpTime;

        //lerp!
        float perc = f_currentLerpTime / f_lerpTime;

        if (Target != null)
        {
            //this.transform.position = Target.position;
            //this.transform.rotation = Target.rotation;
            //this.transform.rotation *= Quaternion.Euler(0,180,0);
        }

        if (RbTarget != null)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, RbTarget.position, perc);
            this.transform.rotation = Quaternion.Slerp(transform.rotation, RbTarget.rotation, perc);
        }

    }
}
