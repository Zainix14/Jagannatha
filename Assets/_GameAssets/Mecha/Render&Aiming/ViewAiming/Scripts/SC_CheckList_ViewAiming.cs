using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CheckList_ViewAiming : MonoBehaviour
{

    public GameObject Screens = null;
    public GameObject Target = null;
    public GameObject CrossHair = null;

    public GameObject GetTarget()
    {
        return Target;
    }

    public GameObject GetScreens()
    {
        return Screens;
    }

    public GameObject GetCrossHair()
    {
        return CrossHair;
    }

}
