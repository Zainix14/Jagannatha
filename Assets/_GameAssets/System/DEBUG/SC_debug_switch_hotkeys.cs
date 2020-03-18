using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_debug_switch_hotkeys : MonoBehaviour
{


    public ViveGripExample_Switch Button1;
    public ViveGripExample_Switch Button2;
    public ViveGripExample_Switch Button3;
    public ViveGripExample_Switch Button4;
    public ViveGripExample_Switch Button5;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Button1.Flip();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Button2.Flip();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Button3.Flip();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Button4.Flip();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Button5.Flip();
        }

    }
}
