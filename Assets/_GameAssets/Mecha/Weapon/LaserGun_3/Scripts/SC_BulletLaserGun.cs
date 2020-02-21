using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BulletLaserGun : MonoBehaviour
{

    public Vector3Int sensitivity;
    MeshRenderer mr;

    void Start()
    {
        mr = this.GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == 26)
            other.GetComponent<Boid>().HitBoid(sensitivity);

        if (other.gameObject.layer == 25)
            other.GetComponentInParent<SC_KoaCollider>().GetHit(sensitivity);

    }

    public void ResetPos()
    {
        transform.position = new Vector3(1000, 1000, 1000);
        transform.localScale = new Vector3(1, 1, 1);
        mr.enabled = false;
    }

}
