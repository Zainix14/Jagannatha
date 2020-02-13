using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SC_SyncVar_calibr : NetworkBehaviour
{
    /// <summary>
    /// //////////////////////////////////List struct des Sliders
    /// </summary>
    /// 
    public int nbOfFloatButtons;

    //nombre de sliders pour l'init
    public SyncListFloat CalibrFloats = new SyncListFloat();



    void Start()
    {
        if (isServer)
        {
            for (int i = 0; i < nbOfFloatButtons; i++)
            {
                CalibrFloats.Insert(i, 0f);
            }
        }
    }





}
