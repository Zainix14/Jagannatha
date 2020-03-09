using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SC_LaserFlock : NetworkBehaviour
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

    public void FireLaser(float laserTime)
    {



    [SerializeField]
    float f_Scale_OP = 2f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayFlockLaser()
    {
        Vector3 ScaleOP = new Vector3(f_Scale_OP, f_Scale_OP, this.transform.localScale.z);
        RpcDisplayFLaserOP(this.gameObject, this.transform.position, this.transform.rotation, ScaleOP);
    }

    [ClientRpc]
    public void RpcDisplayFLaserOP(GameObject target, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        if (!isServer)
        {
            target.transform.position = position;
            target.transform.rotation = rotation;
            target.transform.localScale = scale;
        }
    }

}
