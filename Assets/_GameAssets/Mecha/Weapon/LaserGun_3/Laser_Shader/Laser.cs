using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Camera cam;
    public GameObject FirePoint;
    public LineRenderer lr;
    [SerializeField]
    float maximumLenght;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, FirePoint.transform.position);
        RaycastHit hit;
        var mouse = Input.mousePosition;
        var raymouse = cam.ScreenPointToRay(mouse);
        if (Physics.Raycast(raymouse.origin, raymouse.direction, out hit, maximumLenght))
        {
            if (hit.collider)
                lr.SetPosition(1, hit.point);
            if(hit.collider.tag == "ene")
            {
                //tag boid To damaged
            }
        }
        else
        {
            var pos = raymouse.GetPoint(maximumLenght);
            lr.SetPosition(1, pos);
        }


    }
}
