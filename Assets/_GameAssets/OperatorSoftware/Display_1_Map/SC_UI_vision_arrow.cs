using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_UI_vision_arrow : MonoBehaviour
{

    private GameObject mechdummy;

    Vector3 rot = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        mechdummy = GameObject.FindGameObjectWithTag("mech_dummy");
        rot.x = 90;
    }

    // Update is called once per frame
    void Update()
    {

        rot.y = SC_SyncVar_vision_arrow.Instance.rotCasque+90 + mechdummy.transform.eulerAngles.y;

        transform.eulerAngles = rot;
    }
}
