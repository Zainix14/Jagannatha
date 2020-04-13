using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SC_NetPSpawnKoa_P : NetworkBehaviour
{

    public GameObject Koa;

    public GameObject SpawnKoa()
    {
        GameObject GO_Koa_Temp = (GameObject)Instantiate(Koa, Koa.transform.position, Koa.transform.rotation);
        NetworkServer.Spawn(GO_Koa_Temp);
        return GO_Koa_Temp;
    }

}
