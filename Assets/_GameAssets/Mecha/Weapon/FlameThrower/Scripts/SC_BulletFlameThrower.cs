using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BulletFlameThrower : MonoBehaviour
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
