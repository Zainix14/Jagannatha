using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SC_MoveKoaSync : NetworkBehaviour
{

    public MeshRenderer mr_P;
    public MeshRenderer mr_OP;

    // Start is called before the first frame update
    void Start()
    {
        if (isServer)
        {
            mr_OP.GetComponent<SphereCollider>().enabled = false;
            mr_OP.enabled = false;
        }
        else
            mr_P.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isServer)
            RpcSendVt3Position(gameObject, transform.position);
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

    [ClientRpc]
    public void RpcSendVt3Sensibility(GameObject Target, Vector3 vt3_Sensibility)
    {
        if (!isServer)
            Target.transform.GetChild(1).GetComponent<SC_KoaSettingsOP>().SetSensibility(vt3_Sensibility);
    }

    public void InitOPKoaSettings(Vector3 sensibility)
    {
        if (isServer)
            RpcSendVt3Sensibility(gameObject, sensibility);
    }

}
