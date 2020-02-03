using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SC_SyncVar_Interactibles : NetworkBehaviour
{

    [SyncVar]
    public float slider1value = 0;
    [SyncVar]
    public float slider1valueWanted = 0;
    [SyncVar]
    public bool slider1isEnPanne = false;





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }



}
