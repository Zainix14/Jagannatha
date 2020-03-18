using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SC_SyncVar_Main_Breakdown : NetworkBehaviour
{
    [SyncVar]
    public bool mustReboot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Sync Var " + mustReboot);
    }

    public void checkReboot(bool piloteReboot)
    {
        mustReboot = piloteReboot;
    }
}
