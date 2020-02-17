using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_LaserFlock : MonoBehaviour
{
    GameObject screens;
    // Start is called before the first frame update
    void Start()
    {
        screens = GameObject.FindGameObjectWithTag("Screens");
    }
    private void OnTriggerEnter(Collider other)
    {
        //JE TOUCHE LE PLAYER 
        if (other.tag == "Player")
        {
            Debug.Log("Silence I Beam You");
            screens.GetComponent<Sc_ScreenShake>().ShakeIt(0.01f, 0.2f);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
