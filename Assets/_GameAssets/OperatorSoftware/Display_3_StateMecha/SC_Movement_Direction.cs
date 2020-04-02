using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Movement_Direction : MonoBehaviour
{
    public float speedRotateInit;
    public float speedRotateBase;

    public float speedRotateUsed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.up * speedRotateBase *  Time.deltaTime, Space.World);
    }
}
