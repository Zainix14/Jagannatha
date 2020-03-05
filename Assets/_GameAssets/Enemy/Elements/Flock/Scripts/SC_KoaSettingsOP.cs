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
    int type;
    float maxKoaLife;
    float curKoaLife;


    [SerializeField]
    Color32[] Tab_color;

    public Color32 color;

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

    public void SetKoaType(int type)
    {
        this.type = type;
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
    public float GetCurKoaLife()
    {
        return curKoaLife;
    }

    public float GetMaxKoaLife()
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
                Color32 newColor = Color.white;
                switch(type)
                {
                    case 0:
                        newColor = Tab_color[0];
                        break;
                    case 1:
                        newColor = Tab_color[1];
                        break;
                    case 2:
                        newColor = Tab_color[2];
                        break;
                }
                GetComponent<MeshRenderer>().material.color = newColor;
                color = newColor;
                spawn = true;

            }

           
        }
    }
    public void ResetColor()
    {
        GetComponent<MeshRenderer>().material.color = color;
    }

}
