using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_KoaSettingsOP : MonoBehaviour, IF_KoaForOperator
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
    Material[] Tab_mat;
    [SerializeField]
    Color32[] Tab_color;
    [SerializeField]
    Color32[] Tab_colorSpawn;

    public bool bSelected;

    [SerializeField]
    GameObject VFX_koadeath;

    [SerializeField]
    TrailRenderer trail;

    public enum koaState
    {
        spawn, 
        roam,
        atttack,
        reaction
    }

    public koaState currentState;

    public void SetSensibility(Vector3 sensibility)
    {
        this.sensibility = sensibility;
    }
    public void SetTimeBeforeSpawn(int spawnTimer)
    {
        this.timeBeforeSpawn = spawnTimer;
        initialScale = transform.localScale;
        initialRadius = GetComponent<SphereCollider>().radius;
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
        if(curLife <= 0)
        {

            //https://www.youtube.com/watch?v=VUjn2Vs65Z8
            var vfx = Instantiate(VFX_koadeath);
            vfx.transform.position = transform.position;
            vfx.GetComponent<ParticleSystem>().startColor = Tab_color[type];
            vfx.GetComponent<ParticleSystemRenderer>().trailMaterial.color = Tab_color[type];
            vfx.GetComponent<ParticleSystem>().Play();
        }
    }
    public void SetKoaState(int curState)
    {
        this.currentState = (koaState)curState;
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

    public int getState()
    {
        return (int)currentState;
    }

    void Update()
    {
        if(!spawn)
        {
            SetColor();
            float scale = ((initialScale.x*factor / timeBeforeSpawn) * Time.deltaTime);
            float radius = ((initialRadius / factor / timeBeforeSpawn) * Time.deltaTime);
            transform.localScale += new Vector3(scale, scale, scale);
            transform.GetComponent<SphereCollider>().radius -= radius;
            timer += Time.deltaTime;
            if (timer >= timeBeforeSpawn)
            {
                spawn = true;
                SetColor();
            }           
        }
    }

    public void SetColor()
    {
        Color32 newColor = Color.white;
        if(bSelected)
        {
            GetComponent<MeshRenderer>().material = Tab_mat[1];
        }
        else
        {
            GetComponent<MeshRenderer>().material = Tab_mat[0];
        }

        if (spawn)
            newColor = Tab_color[type];
        else
            newColor = Tab_colorSpawn[type];

        GetComponent<MeshRenderer>().material.color = newColor;
        GetComponent<TrailRenderer>().material.color = newColor;
    }

    public void Action()
    {

    }
}
