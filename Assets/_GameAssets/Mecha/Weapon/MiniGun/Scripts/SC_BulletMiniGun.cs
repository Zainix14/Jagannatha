using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script du Comportement des Bullets |
/// By Cycy modif par Leni |
/// </summary>
public class SC_BulletMiniGun : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("MechBullets"))
            resetPos();
    }

    void resetPos()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        transform.position = new Vector3(1000, 1000, 1000);
    }
}
