using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_RaycastVirtual : MonoBehaviour
{
    private RaycastHit hit;
    private Camera curCam;
    void Start()
    {
        curCam = this.gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //int layerMask = 1 << 9;
        Debug.Log("Virtual raycast");
        //Cast un ray à partir du casque
        if(Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = curCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.DrawRay(transform.position, hit.point * hit.distance, Color.yellow);
            }


            //Debug.Log("AHHHHAHAHAH CLIC");
            //Vector3 mousePosV3 = new Vector3(Input.mousePosition.x,0,0);
            //if (Physics.Raycast(transform.position, Scree, out hit, Mathf.Infinity))
            //{
            //    Debug.DrawRay(transform.position, transform.TransformDirection(mousePosV3) * hit.distance, Color.yellow);
            //}
            
            //Debug.Log("Rayhit = " + hit.point);
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
