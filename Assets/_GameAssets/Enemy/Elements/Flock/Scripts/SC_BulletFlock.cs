using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BulletFlock : MonoBehaviour
{
    Rigidbody rb = null;
    GameObject screens;
    void Start()
    {
        GetRigidBody();
        screens = GameObject.FindGameObjectWithTag("Screens");
    }

    private void OnTriggerEnter(Collider other)
    {
        //JE TOUCHE LE PLAYER 
        if(other.tag == "Player")
        {
            Debug.Log("Silence I Hit You");
            screens.GetComponent<Sc_ScreenShake>().ShakeIt();
        }
     
    }

    void ResetPos()
    {

        if (rb == null)
            GetRigidBody();

        if (rb != null)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.position = new Vector3(1000, 1000, 1000);
        }

    }


    void GetRigidBody()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            Debug.LogWarning("SC_BulletMiniGun - ResetPos - Can't Find RigidBody");
    }
}
