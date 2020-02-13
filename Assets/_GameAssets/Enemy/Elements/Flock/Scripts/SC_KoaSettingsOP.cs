using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_KoaSettingsOP : MonoBehaviour
{
    Vector3 sensibility;

    void update()
    {
        Debug.Log("Sensility : " + sensibility);
    }

    public void SetSensibility(Vector3 sensibility)
    {
        this.sensibility = sensibility;
    }
    public Vector3 GetSensibility()
    {
        return sensibility;
    }

}
