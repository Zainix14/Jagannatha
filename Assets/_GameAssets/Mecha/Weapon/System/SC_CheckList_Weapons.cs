using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CheckList_Weapons : MonoBehaviour
{

    public GameObject TargetHand = null;

    public GameObject Mng_Weapons = null;
    public GameObject MiniGun = null;
    public GameObject FlameThrower = null;
    public GameObject Shrapnel = null;   

    public GameObject GetMngWeapons()
    {
        return Mng_Weapons;
    }

    public GameObject GetTargetHand()
    {
        return TargetHand;
    }

}
