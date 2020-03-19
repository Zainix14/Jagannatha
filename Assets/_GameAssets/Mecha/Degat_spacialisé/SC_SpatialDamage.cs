using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_SpatialDamage : MonoBehaviour
{
    public GameObject Damage;
    public Color curColor;
    public bool b_Hit = false;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        curColor = Damage.GetComponent<Renderer>().material.GetColor("_Color");
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
       while(timer <= 1f)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
        }
        if(timer > 1f)
        {
            Damage.GetComponent<Renderer>().material.SetColor("_Color", curColor);
            Debug.Log("Reset");
        }
        else
        {
            Damage.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            Debug.Log("NotReset");
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "MechBullets")
        {
            timer = 0f;
            Debug.Log("hit");
        }
    }
}
