using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_UI_RaycastOPManager : MonoBehaviour
{
    #region Singleton

    private static SC_UI_RaycastOPManager _instance;
    public static SC_UI_RaycastOPManager Instance { get { return _instance; } }

    #endregion

    private RaycastHit hit;

    Camera camRaycast;


    void Start()
    {
        camRaycast = GetComponent<Camera>();
    }
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    // Update is called once per frame
    void Update()
    {

        //Cast un ray à partir du casque
        if (Input.GetMouseButton(0))
        {
            Ray ray = camRaycast.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {

                SC_RaycastOPMapPerspective.Instance.castRayInWorld(hit);



                if (hit.collider.GetComponent<IF_clicableAction>() != null)
                {
                    hit.collider.GetComponent<IF_clicableAction>().Action();

                }



            }
        }

    }

}
