using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_debug_switch_hotkeys : MonoBehaviour
{


    public SC_SwitchChangeColor Button1;
    public SC_SwitchChangeColor Button2;
    public SC_SwitchChangeColor Button3;
    public SC_SwitchChangeColor Button4;
    public SC_SwitchChangeColor Button5;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Button1.LightOnOFF();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Button2.LightOnOFF();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Button3.LightOnOFF();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Button4.LightOnOFF();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Button5.LightOnOFF();
        }

    }
}
