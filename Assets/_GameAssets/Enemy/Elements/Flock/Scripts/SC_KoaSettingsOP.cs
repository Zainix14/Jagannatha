using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_KoaSettingsOP : MonoBehaviour
{
    Vector3 sensibility;
    float timeBeforeSpawn = 10;
    bool spawn = false;
    public void SetSensibility(Vector3 sensibility)
    {
        this.sensibility = sensibility;
    }
    public void SetTimeBeforeSpawn(int spawnTimer)
    {
        this.timeBeforeSpawn = spawnTimer;
    }

    public Vector3 GetSensibility()
    {
        return sensibility;
    }

    public float GetTimeBeforeSpawn()
    {
        return timeBeforeSpawn;
    }

    void Update()
    {
        if(!spawn)
        {
            
            timeBeforeSpawn -= Time.deltaTime;
            if (timeBeforeSpawn <= 0)
                spawn = true;
        }
    }

}
