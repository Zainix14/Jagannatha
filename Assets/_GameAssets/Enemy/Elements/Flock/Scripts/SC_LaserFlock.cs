using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SC_LaserFlock : NetworkBehaviour
{

    [SerializeField]
    float f_Scale_OP = 2f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayFlockLaser()
    {
        Vector3 ScaleOP = new Vector3(f_Scale_OP, f_Scale_OP, this.transform.localScale.z);
        RpcDisplayFLaserOP(this.gameObject, this.transform.position, this.transform.rotation, ScaleOP);
    }

    [ClientRpc]
    public void RpcDisplayFLaserOP(GameObject target, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        if (!isServer)
        {
            target.transform.position = position;
            target.transform.rotation = rotation;
            target.transform.localScale = scale;
        }
    }

}
