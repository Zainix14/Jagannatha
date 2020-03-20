using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_debug_switch_hotkeys_Movement : MonoBehaviour
{


    public ViveGripExample_Switch Button1;
    public ViveGripExample_Switch Button2;
    public ViveGripExample_Switch Button3;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Button1.Flip();
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Button2.Flip();
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Button3.Flip();
        }


    }
}
