using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_UI_ModeRotate_ScriptByJean : MonoBehaviour
{
    [Range(-100,100)]
    public float speed;
    public bool onZ;
    public bool onY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(onZ)
        {
            this.transform.Rotate(0, 0, speed * Time.deltaTime);
        }
        if (onY)
        {
            this.transform.Rotate(0,  speed * Time.deltaTime,0);
        }

    }
}
