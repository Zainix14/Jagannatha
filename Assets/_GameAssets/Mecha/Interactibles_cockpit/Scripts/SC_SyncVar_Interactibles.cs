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

    [SyncVar]
    public float slider2value = 0;
    [SyncVar]
    public float slider2valueWanted = 0;
    [SyncVar]
    public bool slider2isEnPanne = false;

    [SyncVar]
    public float slider3value = 0;
    [SyncVar]
    public float slider3valueWanted = 0;
    [SyncVar]
    public bool slider3isEnPanne = false;


    [SyncVar]
    public float potar1value = 0;
    [SyncVar]
    public float potar1valueWanted = 0;
    [SyncVar]
    public bool potar1isEnPanne = false;

    [SyncVar]
    public float potar2value = 0;
    [SyncVar]
    public float potar2valueWanted = 0;
    [SyncVar]
    public bool potar2isEnPanne = false;

    [SyncVar]
    public float potar3value = 0;
    [SyncVar]
    public float potar3valueWanted = 0;
    [SyncVar]
    public bool potar3isEnPanne = false;


    [SyncVar]
    public bool inter1value = false;
    [SyncVar]
    public bool inter1valueWanted = false;
    [SyncVar]
    public bool inter1isEnPanne = false;





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }



}
