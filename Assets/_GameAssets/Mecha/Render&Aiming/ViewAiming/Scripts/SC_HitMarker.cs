using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_HitMarker : MonoBehaviour
{
    
    #region Singleton

    private static SC_HitMarker _instance;
    public static SC_HitMarker Instance { get { return _instance; } }

    #endregion


    MeshRenderer meshRenderer;
    Material mat;
    bool bAnimation;

    [SerializeField]
    float animationTime;
    float curTime;

    Vector3 initialScale;
    Quaternion initialRotation;
    [SerializeField]
    float scaleFactor;

    public bool hit;
    bool turn;

    public enum HitType { Normal, Critical, none };

    public void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
        meshRenderer = this.GetComponent<MeshRenderer>();
        mat = meshRenderer.material;
        bAnimation = false;
        curTime = 0;
        initialScale = transform.localScale;
        hit = false;
    }

    public void HitMark(HitType Type)
    {
        meshRenderer.enabled = true;
        bAnimation = true;
        curTime = 0;
        switch (Type)
        {

            case HitType.Normal :
                mat.color = Color.white;
                turn = false;
                transform.rotation = initialRotation;
                transform.localScale = initialScale;


                break;

            case HitType.Critical:
                mat.color = Color.red;
                turn = true;
                break;
            case HitType.none:
                bAnimation = false;
                meshRenderer.enabled = false;
                transform.localScale = initialScale;

                turn = false;
                break;

        }
    }

    void Update()
    {
      

        if(bAnimation)
        {

            /// INSERTFLICK
            /*
            curTime += Time.deltaTime;
            float scale = animationTime / (initialScale.x * scaleFactor);

            scale = scale * Time.deltaTime;
            
            if(curTime >= animationTime)
            {
                transform.localScale = initialScale;
            }
            else if(curTime >= animationTime/2)
            {
                transform.localScale -= new Vector3(scale, scale, scale);
            }
            else
            {
                transform.localScale += new Vector3(scale, scale, scale);
            }*/
            if(turn)
            transform.Rotate(new Vector3(0, 0, 300 * Time.deltaTime));
        }
    }

}
