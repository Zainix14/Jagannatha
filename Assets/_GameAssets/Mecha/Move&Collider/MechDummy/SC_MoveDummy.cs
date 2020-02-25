using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SC_MoveDummy : NetworkBehaviour
{

    private GameObject Target;
    private MeshRenderer mr;

    // Start is called before the first frame update
    void Start()
    {
        if (isServer)
            GetReferences();
    }

    // Update is called once per frame
    void Update()
    {
        if (isServer)
        {
            SyncPos();
            SendRpc();
        }
    }

    void GetReferences()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
        if (Target == null)
            Debug.LogWarning("Can't Find Player Tagged Object");

        mr = this.GetComponentInChildren<MeshRenderer>();
        if (mr != null)
            mr.enabled = false;
        else
            Debug.LogWarning("Can't Find MeshRenderer");
    }

    void SyncPos()
    {
        if (Target != null)
        {
            transform.position = Target.transform.position;
            transform.rotation = Target.transform.rotation;
        }
        else
            GetReferences();
    }

    void SendRpc()
    {
        RpcSendVt3Position(gameObject, transform.position);
        RpcSendQtnRotation(gameObject, transform.rotation);
    }

    /// <summary>
    ///Vector3 Transform => change la position d'un objet dans un espace 3D
    /// </summary>
    [ClientRpc]
    public void RpcSendVt3Position(GameObject Target, Vector3 vt3_Position)
    {
        if (!isServer)
            Target.transform.position = vt3_Position;
    }

    /// <summary>
    ///Quaternion => change la rotation d'un objet à partir d'un quaternion
    /// </summary>
    [ClientRpc]
    public void RpcSendQtnRotation(GameObject Target, Quaternion qtn_Rotation)
    {
        if (!isServer)
            Target.transform.rotation = qtn_Rotation;
    }

}
