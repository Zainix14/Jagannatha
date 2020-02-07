using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BreakdownAlert : MonoBehaviour
{

    public void LaunchAlert()
    {

        Invoke("makeVisible", Random.Range(0f, 1.5f));


    }

    void makeVisible()
    {
        gameObject.transform.GetComponent<MeshRenderer>().enabled = true;

    }

    public void StopAlert()
    {
        Invoke("makeInvisible", Random.Range(0f, 0.5f));

    }

    void makeInvisible()
    {
        gameObject.transform.GetComponent<MeshRenderer>().enabled = false;

    }

    
}
