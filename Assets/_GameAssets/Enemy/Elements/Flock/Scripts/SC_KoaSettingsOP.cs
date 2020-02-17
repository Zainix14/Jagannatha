using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_KoaSettingsOP : MonoBehaviour, IF_ClicableForOperator
{
    Vector3 sensibility = new Vector3 (12,5,6);

    public void SetSensibility(Vector3 sensibility)
    {
        this.sensibility = sensibility;
    }
    public Vector3 GetSensibility()
    {
        return sensibility;
    }

}
