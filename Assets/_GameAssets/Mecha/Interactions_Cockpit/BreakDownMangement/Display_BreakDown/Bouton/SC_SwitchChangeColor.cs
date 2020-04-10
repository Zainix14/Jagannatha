using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_SwitchChangeColor : MonoBehaviour
{
    // Start is called before the first frame update
    bool b_light;

    void Start()
    {
        b_light = false;
        
    }

    // Update is called once per frame
    void Update()
    {


    }
    public void LightOnOFF()
    {
        if (b_light == false)
        {
            gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            Debug.Log("LightOn");
            b_light = true;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
            Debug.Log("LightOff");
            b_light = false;
        }
    }


}
