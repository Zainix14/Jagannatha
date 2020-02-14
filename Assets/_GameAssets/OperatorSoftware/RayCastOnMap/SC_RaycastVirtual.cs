using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_RaycastVirtual : MonoBehaviour
{
    private RaycastHit hit;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //int layerMask = 1 << 9;
        Debug.Log("Virtual raycast");
        //Cast un ray à partir du casque
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("AHHHHAHAHAH CLIC");
            if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, 500f))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up)* Mathf.Infinity, Color.yellow);
            }
            
            Debug.Log("Rayhit = " + hit.point);
        }

    }

    /// <summary>
    /// Renvois le raycasthit du cockpit
    /// </summary>
    /// <returns></returns>
    public RaycastHit getRay()
    {
        return hit;
    }

}
