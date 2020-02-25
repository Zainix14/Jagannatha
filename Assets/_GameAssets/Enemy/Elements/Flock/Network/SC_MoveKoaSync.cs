using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SC_MoveKoaSync : NetworkBehaviour
{

    public MeshRenderer mr_P;
    public GameObject mr_OP;

    [SyncVar]
    public int curboidNumber = 0;
    [SyncVar]
    public int MaxboidNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (isServer)
        {
            mr_OP.GetComponent<SphereCollider>().enabled = false;
            mr_OP.SetActive(false);
            mr_P.enabled = false;
        }
        else if (!isServer)
        {
            mr_P.enabled = false;
            mr_OP.SetActive(true);
        }         
    }

    public void SetPilotMeshActive()
    {
        if(isServer)
        {
            mr_P.enabled = true;
        }
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
    public void RpcSendStartInfo(GameObject Target, Vector3 vt3_Sensibility, int timeBeforeSpawn,string KoaID)
    {
        if (!isServer)
        {
            Target.transform.GetChild(1).GetComponent<SC_KoaSettingsOP>().SetSensibility(vt3_Sensibility);
            Target.transform.GetChild(1).GetComponent<SC_KoaSettingsOP>().SetTimeBeforeSpawn(timeBeforeSpawn);
            Target.transform.GetChild(1).GetComponent<SC_KoaSettingsOP>().SetKoaID(KoaID);
        }
    }

    public void InitOPKoaSettings(Vector3 sensibility, int timeBeforeSpawn, string KoaID)
    {
        if (isServer)
            RpcSendStartInfo(gameObject, sensibility,timeBeforeSpawn, KoaID);
    }

}
