using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_UI_Display_MapInfos_KOAShake : MonoBehaviour
{

    #region Singleton

    private static SC_UI_Display_MapInfos_KOAShake _instance;
    public static SC_UI_Display_MapInfos_KOAShake Instance { get { return _instance; } }

    #endregion
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform koaTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

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
        if (koaTransform == null)
        {
            koaTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = koaTransform.localPosition;
    }

    public void ShakeIt(float amplitude, float duration)
    {
        shakeAmount = amplitude;
        shakeDuration = shakeDuration + duration;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            koaTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            koaTransform.localPosition = originalPos;
        }

    }
}
