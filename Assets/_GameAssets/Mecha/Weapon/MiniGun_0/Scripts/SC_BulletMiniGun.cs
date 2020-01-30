using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script du Comportement des Bullets |
/// By Cycy modif par Leni |
/// </summary>
public class SC_BulletMiniGun : MonoBehaviour
{

    Rigidbody rb = null;

    void Start()
    {
        GetRigidBody();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("MechBullets"))
            ResetPos();
    }

    void ResetPos()
    {

        if(rb == null)
            GetRigidBody();

        if(rb != null)
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
