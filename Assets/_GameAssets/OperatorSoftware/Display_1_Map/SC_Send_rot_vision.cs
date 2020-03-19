using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Send_rot_vision : MonoBehaviour
{

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        SC_SyncVar_vision_arrow.Instance.rotCasque = this.transform.eulerAngles.y;

    }
}
