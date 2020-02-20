using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_LaserFlock : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        //JE TOUCHE LE PLAYER 
        if (other.tag == "Player")
        {
            //https://www.youtube.com/watch?v=GBvfiCdk-jc&list=PLbsiLVHJCH9iHz_HDGirFtRUtKbdc9czK
            Sc_ScreenShake.Instance.ShakeIt(0.01f, 0.2f);
            //https://www.youtube.com/watch?v=nfWlot6h_JM
            CustomSoundManager.Instance.PlaySound(gameObject, "SFX_p_Impact", false, 0.1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
