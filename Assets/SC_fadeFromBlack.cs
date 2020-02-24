using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_fadeFromBlack : MonoBehaviour
{
    private void Awake()
    {

        SteamVR_Fade.Start(Color.black, 0f);
        SteamVR_Fade.Start(Color.clear, 4f);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
