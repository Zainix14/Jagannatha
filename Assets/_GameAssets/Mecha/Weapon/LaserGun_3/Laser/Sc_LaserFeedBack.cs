using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_LaserFeedBack : MonoBehaviour
{

    #region Singleton

    private static Sc_LaserFeedBack _instance;
    public static Sc_LaserFeedBack Instance { get { return _instance; } }

    #endregion

    public SC_WeaponLaserGun MainLaserScript;
    public GameObject FirePoint;
    GameObject SFX_LaserBeam;
    public LineRenderer lr;
    public LineRenderer lr2;
    int SoundSourceNumb;
    [SerializeField]
    ParticleSystem Sparkle;
    [SerializeField]
    Color CurColor;
    [SerializeField]
    SC_WeaponLaserGun WeapMainSC;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void EnableLaser(RaycastHit hit)
    {
        if(SoundSourceNumb == 0)
        {
            SFX_LaserBeam = CustomSoundManager.Instance.PlaySound(gameObject, "SFX_p_LaserBeam2", true, 0.1f);
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

    public void SetColor(Color32 NewColor)
    {

        if(CurColor != NewColor)
        {

            WeapMainSC.AlignColor(CurColor);

            CurColor = NewColor;

            Gradient gradiend = new Gradient();
            GradientColorKey[] colorKeys = new GradientColorKey[3];
            GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];

            alphaKeys[0].time = 0;
            alphaKeys[0].alpha = 1;

            alphaKeys[1].time = 1;
            alphaKeys[1].alpha = 1;

            colorKeys[0].color = NewColor;
            colorKeys[1].color = NewColor;
            colorKeys[2].color = NewColor;

            gradiend.SetKeys(colorKeys, alphaKeys);
            gradiend.SetKeys(colorKeys, alphaKeys);

            lr.colorGradient = gradiend;
            lr2.colorGradient = gradiend;
        }     

    }

}
