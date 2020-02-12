using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_TargetHandRotation : MonoBehaviour
{

    public Transform AimIndicator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (AimIndicator != null)
            LookAt();
    }

    void LookAt()
    {
        transform.LookAt(AimIndicator);
        transform.rotation *= Quaternion.Euler(-90, -90, 90);

    }

}
