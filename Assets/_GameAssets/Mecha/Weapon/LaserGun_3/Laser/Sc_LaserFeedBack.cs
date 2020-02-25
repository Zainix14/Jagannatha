using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_LaserFeedBack : MonoBehaviour
{
    public SC_WeaponLaserGun MainLaserScript;
    public GameObject FirePoint;
    GameObject SFX_LaserBeam;
    public LineRenderer lr;
    public LineRenderer lr2;
    int SoundSourceNumb;
    [SerializeField]
    ParticleSystem Sparkle;

    public void EnableLaser(RaycastHit hit)
    {
        if(SoundSourceNumb == 0)
        {
            SFX_LaserBeam = CustomSoundManager.Instance.PlaySound(gameObject, "SFX_p_LaserBeam", true, 0.1f);
            SoundSourceNumb += 1;
        }
        
        if (!lr.enabled)
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
        if(SFX_LaserBeam != null && SFX_LaserBeam.GetComponent<AudioSource>().isPlaying)
        {
            SFX_LaserBeam.GetComponent<AudioSource>().Stop();
            SoundSourceNumb = 0;
        }
        if (Sparkle.isPlaying)
            Sparkle.Stop();

        if (lr.enabled)
            lr.enabled = false;
        if (lr2.enabled)
            lr2.enabled = false;

    }

}
