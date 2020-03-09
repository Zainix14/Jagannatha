using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_tourbilol : MonoBehaviour
{
    float oldRot;
    float curRot;

    float totalAngle = 0;

    // Start is called before the first frame update
    void Start()
    {
        oldRot = this.transform.localEulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {

        curRot = this.transform.localEulerAngles.z ;

        if(Mathf.Abs(oldRot - curRot)<260)
        totalAngle += oldRot-curRot;

        if (totalAngle < -360)
            totalAngle += 360;
        else if (totalAngle > 360)
            totalAngle -= 360;

        Debug.Log(totalAngle);

        oldRot = curRot;


    }
}
