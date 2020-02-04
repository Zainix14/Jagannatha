using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BreakdownOnBreakdownAlert : MonoBehaviour
{


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            LaunchGlobalAlert();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            StopGlobalAlert();
        }
    }

    public void LaunchGlobalAlert()
    {
        SC_BreakdownAlert[] childs = transform.GetComponentsInChildren<SC_BreakdownAlert>();
        foreach (SC_BreakdownAlert child in childs)
        {
            child.LaunchAlert();
        }

    }


    public void StopGlobalAlert()
    {
        SC_BreakdownAlert[] childs = transform.GetComponentsInChildren<SC_BreakdownAlert>();
        foreach (SC_BreakdownAlert child in childs)
        {
            child.StopAlert();
        }

    }

}
