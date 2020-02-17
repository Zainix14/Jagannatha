using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_ScreenShake : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform screenTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
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

    public void ShakeIt()
    {
        shakeDuration = shakeDuration + 0.1f;
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

        if (Input.GetKeyDown(KeyCode.H))
        {
            ShakeIt();
        }

    }
}