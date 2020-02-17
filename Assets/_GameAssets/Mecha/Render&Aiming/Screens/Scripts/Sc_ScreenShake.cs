using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_ScreenShake : MonoBehaviour
{
    /// <summary>
    /// script sur les ecrans a shake
    /// </summary>
 
    // Transform de la camera a shake
    public Transform screenTransform;

    //Combien de temps la cam va shake
    public float shakeDuration = 0f;

    //Amplitude a laquelle la camera va shake
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    void Awake()
    {
        if (screenTransform == null)
        {
            screenTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = screenTransform.localPosition;
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
            screenTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            screenTransform.localPosition = originalPos;
        }

    }
}