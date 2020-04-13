 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmLight : MonoBehaviour
{
    private Light redLight;
    public float maxIntensity = 10f;
    public float minIntensity = 1f;
    public float pulseSpeed = 0.5f; //here, a value of 0.5f would take 2 seconds and a value of 2f would take half a second
    private float targetIntensity = 10f;
    private float currentIntensity;


    void Start()
    {
        redLight = GetComponent<Light>();
    }
    void Update()
    {
        currentIntensity = Mathf.MoveTowards(redLight.intensity, targetIntensity, Time.deltaTime * pulseSpeed);
        if (currentIntensity >= maxIntensity)
        {
            currentIntensity = maxIntensity;
            targetIntensity = minIntensity;
        }
        else if (currentIntensity <= minIntensity)
        {
            currentIntensity = minIntensity;
            targetIntensity = maxIntensity;
        }
        redLight.intensity = currentIntensity;
    }
}
