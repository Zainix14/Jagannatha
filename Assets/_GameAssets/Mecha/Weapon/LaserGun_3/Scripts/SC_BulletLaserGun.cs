using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SC_BulletLaserGun : NetworkBehaviour
{

    public Vector3Int sensitivity;
    MeshRenderer mr;
    float timer = 0;
    public float frequency;
    [SerializeField]
    float f_Scale = 0.5f;

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

    public void DisplayLaser(GameObject helper_startPos, GameObject Target, bool Visible)
    {

        //Positionne le laser a la base de l'arme (GunPos) et l'oriente dans la direction du point visée par le joueur
        transform.position = Vector3.Lerp(helper_startPos.transform.position, Target.transform.position, .5f);
        transform.LookAt(Target.transform.position);

        if (!mr.enabled && Visible)
            mr.enabled = true;
        else if (mr.enabled && !Visible)
            mr.enabled = false;
        
        //Scale en Z le laser pour l'agrandir jusqu'a ce qu'il touche le point visée par le joueur (C STYLE TAHU)
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, Vector3.Distance(helper_startPos.transform.position, Target.transform.position));

        Debug.Log("SyncOP0");

        if (isServer)
            RpcDisplayLaserOP(gameObject, transform);        

    }

    public void ResetPos()
    {
        transform.position = new Vector3(1000, 1000, 1000);
        transform.localScale = new Vector3(f_Scale, f_Scale, f_Scale);
        mr.enabled = false;
    }

    
    [ClientRpc]
    public void RpcDisplayLaserOP(GameObject target, Transform transform)
    {
        Debug.Log("SyncOP1");
        if (!isServer)
        {
            Debug.Log("SyncOP2");
            target.transform.position = transform.position;
            target.transform.rotation = transform.rotation;
            target.transform.localScale = transform.localScale;
        }        
    }

}
