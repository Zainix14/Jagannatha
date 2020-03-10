using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script sur la camera de cockpit pour tirer un ray depuis le casque | 
/// Script by Cycy Mod by Leni | 
/// </summary>

public class SC_raycast : MonoBehaviour
{

    GameObject Mng_CheckList = null;

    public GameObject Target;
    private RaycastHit hit;
    public Vector3 VT3_CockpitAimRaycast;
    public Vector3 VT3_HitPos;

    private void Start()
    {
        Mng_CheckList = GameObject.FindGameObjectWithTag("Mng_CheckList");
    }

    void Update()
    {

        if (Target == null)
            GetTarget();

        if (Target != null)
            CastRayInCockpit();

    }

    void GetTarget()
    {
        Target = Mng_CheckList.GetComponent<SC_CheckList_ViewAiming>().GetTarget();
    }

    void CastRayInCockpit()
    {

        //int layerMask = 1 << 9;

        //Cast un ray à partir du casque
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 100f))
        {
            if (hit.collider == Target.GetComponent<Collider>())
            {
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                VT3_CockpitAimRaycast = hit.point - Target.transform.position;
                VT3_HitPos = hit.point;
            }
        }

    }

    /// <summary>
    /// Renvois le raycasthit du cockpit
    /// </summary>
    /// <returns></returns>
    public RaycastHit getRay()
    {
        return hit;
    }

    /// <summary>
    /// Renvois le Vt3 du raycast cockpit
    /// </summary>
    /// <returns></returns>
    public Vector3 getRayVector3()
    {
        return VT3_CockpitAimRaycast;
    }

    public Vector3 getRayHitPoint()
    {
        return VT3_HitPos;
    }

}
