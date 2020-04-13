using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_UI_vision_arrow : MonoBehaviour
{

    private GameObject mechdummy;

    Vector3 oldRot;

    Vector3 rot = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        mechdummy = GameObject.FindGameObjectWithTag("mech_dummy");
        rot.x = 90;

        oldRot = this.transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {

        //serieusement je sais pas si ca fait qqchose https://www.youtube.com/watch?v=yeyXSVyjRoc

        rot.y = SC_SyncVar_vision_arrow.Instance.rotCasque+ 90 + mechdummy.transform.eulerAngles.y;


        transform.eulerAngles = Vector3.Slerp(oldRot, rot, Time.deltaTime * 800);

        if (Mathf.Abs(oldRot.y - rot.y) < 70 && Mathf.Abs(oldRot.y - rot.y) > 30)
        {

            oldRot = rot;

        }
            
    }
}
