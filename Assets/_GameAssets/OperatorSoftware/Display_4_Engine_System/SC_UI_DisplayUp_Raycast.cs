using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_UI_DisplayUp_Raycast : MonoBehaviour
{
    private RaycastHit hit;
    private Camera curCam;

    // Start is called before the first frame update
    void Start()
    {
        curCam = this.gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = curCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.GetComponent<IF_clicableAction>() != null)
                {
                    hit.collider.GetComponent<IF_clicableAction>().Action();

                }
            }
        }
    }
}
