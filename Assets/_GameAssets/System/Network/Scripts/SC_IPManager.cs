using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class SC_IPManager : NetworkBehaviour
{
    [SerializeField]
    NetworkManager _NetworkManager;

    [SerializeField]
    bool isLocalHostBuild;
    [SerializeField]
    bool isLocalHostEditor;

    // Start is called before the first frame update
    void Start()
    {
        if(isLocalHostBuild)
            _NetworkManager.networkAddress = "localhost";
        
        else
            _NetworkManager.networkAddress = "192.168.151.116";

#if UNITY_EDITOR
        if (isLocalHostEditor)
        {
            _NetworkManager.networkAddress = "localhost";
        }
        else
        {
            _NetworkManager.networkAddress = "192.168.151.116";
        }

        

#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
