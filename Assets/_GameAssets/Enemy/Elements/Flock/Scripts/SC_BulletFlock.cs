using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SC_BulletFlock : NetworkBehaviour
{

    public bool b_IsFire = false;
    Rigidbody rb = null;

    void Start()
    {
        GetRigidBody();
    }

    void Update()
    {
        if(isServer && b_IsFire)
            RpcDisplayFBulletOP(this.gameObject, this.transform.position, this.transform.rotation, this.transform.localScale);
    }

    private void OnTriggerEnter(Collider other)
    {
        //JE TOUCHE LE PLAYER 
        if(other.gameObject.layer == 20)
        {
            Sc_ScreenShake.Instance.ShakeIt(0.015f,0.1f);
            SC_CockpitShake.Instance.ShakeIt(0.0075f, 0.1f);

            //on fait subir des dmg au joueur
            SC_MainBreakDownManager.Instance.causeDamageOnSystem(1);

            ResetPos();
        }
        if(other.gameObject.layer == 16)
        {
            ResetPos();
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
            b_IsFire = false;
            if (isServer)
                RpcDisplayFBulletOP(this.gameObject, this.transform.position, this.transform.rotation, this.transform.localScale);
        }    

    }

    void GetRigidBody()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            Debug.LogWarning("SC_BulletMiniGun - ResetPos - Can't Find RigidBody");
    }

    [ClientRpc]
    public void RpcDisplayFBulletOP(GameObject target, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        if (!isServer)
        {
            target.transform.position = position;
            target.transform.rotation = rotation;
            target.transform.localScale = scale;
        }
    }

}
