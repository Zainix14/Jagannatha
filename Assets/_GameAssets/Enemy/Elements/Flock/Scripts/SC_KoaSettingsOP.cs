using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_KoaSettingsOP : MonoBehaviour, IF_ClicableForOperator
{
    Vector3 sensibility;
    float timer;
    float timeBeforeSpawn;
    bool spawn = true;
    Vector3 initialScale;
    float initialRadius;
    [SerializeField]
    int factor;
    string koaID;

    int maxKoaLife;
    int curKoaLife;

    public void SetSensibility(Vector3 sensibility)
    {
        this.sensibility = sensibility;
    }
    public void SetTimeBeforeSpawn(int spawnTimer)
    {
        this.timeBeforeSpawn = spawnTimer;
        initialScale = transform.localScale;
        initialRadius = GetComponent<SphereCollider>().radius;
        GetComponent<MeshRenderer>().material.color = Color.yellow;
        spawn = false;
        timer = 0;
    }

    public void SetKoaID(string koaID)
    {
        this.koaID = koaID;
    }

    public void SetKoaLife(int curLife)
    {
        this.curKoaLife = curLife;
    }

    public void SetKoamaxLife(int maxLife)
    {
        this.maxKoaLife = maxLife;
    }

    public string GetKoaID()
    {
        return koaID;
    }
    public int GetCurKoaLife()
    {
        return curKoaLife;
    }

    public int GetMaxKoaLife()
    {
        return maxKoaLife;
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
            float scale = ((initialScale.x*factor / timeBeforeSpawn) * Time.deltaTime);
            float radius = ((initialRadius / factor / timeBeforeSpawn) * Time.deltaTime);
            transform.localScale += new Vector3(scale, scale, scale);
            transform.GetComponent<SphereCollider>().radius -= radius;
            timer += Time.deltaTime;
            if (timer >= timeBeforeSpawn)
            {
                GetComponent<MeshRenderer>().material.color = Color.red;
                spawn = true;

            }

           
        }
    }

}
