using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_LaserFeedBack : MonoBehaviour
{
    public SC_WeaponLaserGun MainLaserScript;
    public GameObject FirePoint;
    public LineRenderer lr;
    public LineRenderer lr2;
    [SerializeField]
    ParticleSystem Sparkle;

    public void EnableLaser(RaycastHit hit)
    {

        if(!lr.enabled)
            lr.enabled = true;
        if (!lr2.enabled)
            lr2.enabled = true;

        var targetPoint = lr.worldToLocalMatrix.MultiplyPoint(hit.point);
        lr.SetPosition(0, Vector3.zero);
        lr.SetPosition(1, targetPoint);

        lr2.SetPosition(0, Vector3.zero);
        lr2.SetPosition(1, lr.GetPosition(1));

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
        if (lr2.enabled)
            lr2.enabled = false;

    }

}
