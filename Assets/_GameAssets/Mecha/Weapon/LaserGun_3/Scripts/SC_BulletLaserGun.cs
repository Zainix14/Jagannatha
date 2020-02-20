using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BulletLaserGun : MonoBehaviour
{

    public Vector3Int sensitivity;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == 26)
            other.GetComponent<Boid>().HitBoid(sensitivity);

        if (other.gameObject.layer == 25)
            other.GetComponentInParent<SC_KoaCollider>().GetHit(sensitivity);

    }

}
