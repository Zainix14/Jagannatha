using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BulletLaserGun : MonoBehaviour
{

    public Vector3Int sensitivity;
    MeshRenderer mr;
    float timer = 0;
    public float frequency;

    void Start()
    {
        mr = this.GetComponent<MeshRenderer>();
    }

    /*
    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.layer == 26)
        {

            if (timer > (1 / frequency))
            {
                timer = 0;
                other.GetComponent<Boid>().HitBoid(sensitivity);
            }

            timer += Time.deltaTime;
        }              

        if (other.gameObject.layer == 25)
        {

            if (timer > (1 / frequency))
            {
                timer = 0;
                other.GetComponentInParent<SC_KoaCollider>().GetHit(sensitivity);
            }

            timer += Time.deltaTime;
            
        }
                
    }
    */

    public void ResetPos()
    {
        transform.position = new Vector3(1000, 1000, 1000);
        transform.localScale = new Vector3(1, 1, 1);
        mr.enabled = false;
    }

}
