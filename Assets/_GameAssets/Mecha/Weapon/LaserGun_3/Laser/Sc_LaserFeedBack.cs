using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_LaserFeedBack : MonoBehaviour
{
    public SC_WeaponLaserGun MainLaserScript;
    public GameObject FirePoint;
    public LineRenderer lr;
    [SerializeField]
    ParticleSystem Sparkle;

    public void EnableLaser(RaycastHit hit)
    {

        if(!lr.enabled)
            lr.enabled = true;

        lr.SetPosition(0, FirePoint.transform.position);
        lr.SetPosition(1, hit.point);

        if (!Sparkle.isPlaying)
            Sparkle.Play();

        Sparkle.transform.position = hit.point;      

    }

    public void DiseableLaser()
    {

        if (Sparkle.isPlaying)
            Sparkle.Stop();

        if (lr.enabled)
            lr.enabled = false;

    }

}
