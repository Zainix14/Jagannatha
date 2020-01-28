using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log(other);

        if (!other.CompareTag("MechBullets"))
            resetPos();
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
                Debug.Log("exploded");

                ft += shrapnelArmedDuration;

            }
            else
            {
                yield return new WaitForSeconds(updateShrapnelInMs);
            }

        }

        Debug.Log("Disarmed");
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<MeshRenderer>().enabled = false;
        Invoke("resetPos", stayAfterShrapnelInS);
        resetPos();

    }

    void resetPos()
    {

        if (ShrapnelCone.activeSelf)
            ShrapnelCone.SetActive(false);

        GetComponent<Rigidbody>().isKinematic = true;
        transform.position = new Vector3(1000, 1000, 1000);

    }
}
