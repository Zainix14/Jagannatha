﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SC_SyncVar_StateMecha_Display : NetworkBehaviour
{
    
    public int nbDisplay = 12;

    //bool[] display;

    public SyncListBool displayAll = new SyncListBool();

    // Start is called before the first frame update
    void Start()
    {
        if (isServer)
        {
            for (int i = 0; i < nbDisplay; i++)
            {
                displayAll.Insert(i, false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}