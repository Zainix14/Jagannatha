using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// a mettre sur le NetPlayer prefab
/// </summary>
public class SC_NetPlayerAutoConnect_OP : NetworkBehaviour
{

    GameObject Mng_Network;

    [SyncVar]
    public int n_ConnectionsCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        GetReferences();
    }

    // Update is called once per frame
    void Update()
    {

        if (Mng_Network == null)
            GetReferences();

        //Mng_Network.GetComponent<NetworkManager>().connectionConfig

        /*
        else
        {
            if(Mng_Network == null)
            {
                Mng_Network = Mng_CheckList.GetComponent<SC_CheckList>().GetNetworkManager();
            }
            else
            {
                if (isServer)
                {
                    n_ConnectionsCount = NetworkServer.connections.Count;
                }

                if (!isServer && isLocalPlayer)
                    Connect();
            }
        }
        */
    }

    void GetReferences()
    {
        if (Mng_Network == null)
            Mng_Network = SC_CheckList.Instance.Mng_Network;
    }

    void Connect()
    {
        if (!isServer && isLocalPlayer)
        {
            Mng_Network.GetComponent<NetworkManager>().networkAddress = "192.168.151.72";
            Mng_Network.GetComponent<NetworkManager>().StartClient();
        }
    }

    void OnDisconnectedFromServer()
    {
        Debug.Log("Disconnected YAYA CA MARCHE");
    }


    void OnDisconnectedFromServer(NetworkConnection info)
    {
        Debug.Log("Disconnected from server: " + info);
    }

    public virtual void OnClientDisconnect(NetworkConnection conn)
    {
        Debug.Log("Server is stopped from PlayerPrefab");
    }


}
