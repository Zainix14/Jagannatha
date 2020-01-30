using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script du Comportement des Bullets |
/// By Cycy modif par Leni |
/// </summary>
public class SC_BulletShrapnel : MonoBehaviour
{

    public float shrapnelArmedDuration = 5;
    public float updateShrapnelInMs = 0.1f;
    public GameObject rayStart;
    public GameObject ShrapnelCone;
    public ParticleSystem shrapnelEffect;
    public float shrapnelDistance = 2f;
    public float stayAfterShrapnelInS = 3f;

    private RaycastHit hit;

    Rigidbody rb = null;
    MeshRenderer mr = null;

    void Start()
    {
        GetReferences();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("MechBullets"))
            ResetPos();
    }

    public void ArmShrapnel()
    {
        ShrapnelCone.SetActive(true);
        StartCoroutine("ShrapnelCheck");
    }

    IEnumerator ShrapnelCheck()
    {

        for (float ft = 0; ft <= shrapnelArmedDuration; ft += updateShrapnelInMs)
        {

            if (Physics.Raycast(rayStart.transform.position, transform.TransformDirection(Vector3.forward), out hit, shrapnelDistance))
            {

                Debug.DrawRay(rayStart.transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);

                shrapnelEffect.Play();
                //Debug.Log("exploded");

                ft += shrapnelArmedDuration;

            }
            else
            {
                yield return new WaitForSeconds(updateShrapnelInMs);
            }

        }

        //Debug.Log("Disarmed");

        if (rb == null || mr == null)
            GetReferences();
        if (rb != null)
            GetComponent<Rigidbody>().isKinematic = true;
        if (mr != null)
            mr.enabled = false;
        Invoke("ResetPos", stayAfterShrapnelInS);
        ResetPos();

    }

    void ResetPos()
    {

        if (ShrapnelCone.activeSelf)
            ShrapnelCone.SetActive(false);

        if (rb == null)
            GetReferences();

        if (rb != null)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.position = new Vector3(1000, 1000, 1000);
        }        

    }

    void GetReferences()
    {      
        if (rb == null)
            rb = GetComponent<Rigidbody>();
        if (rb == null)
            Debug.LogWarning("SC_BulletShrapnel - ResetPos - Can't Find RigidBody");
        if (mr == null)
            mr = GetComponentInChildren<MeshRenderer>();
        if (mr == null)
            Debug.LogWarning("SC_BulletShrapnel - ResetPos - Can't Find MeshRenderer");
    }

}
