using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_UI_Display_MapInfos_KoaState : MonoBehaviour
{

    public SC_RaycastRealWorld scriptRaycast; //Sur camera Full view

    [SerializeField]
    Text[] sensi = new Text[3];


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sensi[0].text = scriptRaycast.sensi.x.ToString();
        sensi[1].text = scriptRaycast.sensi.y.ToString();
        sensi[2].text = scriptRaycast.sensi.z.ToString();
    }
}
