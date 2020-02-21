using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_AimHit : MonoBehaviour
{

    public Vector3Int sensitivity;
    public bool b_OnFire = false;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == 26 && b_OnFire)
        {
            other.GetComponent<Boid>().HitBoid(sensitivity);
        }
            

        if (other.gameObject.layer == 25 && b_OnFire)
        {
            other.GetComponentInParent<SC_KoaCollider>().GetHit(sensitivity);
        }
            

    }

}
