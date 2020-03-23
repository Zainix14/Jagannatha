using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_SpatialDamage : MonoBehaviour
{
    public GameObject SpatialDamage;
    public int CurIndex = 0;
    public GameObject CurSpatial;
    // Start is called before the first frame update
    void Start()
    {
        SpatialDamage = GameObject.FindGameObjectWithTag("SpatialDamage");
        CurSpatial = SpatialDamage.transform.GetChild(CurIndex).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 27)
        {
            StartCoroutine(Blink());
        }
    }
    IEnumerator Blink()
    {
        Debug.Log("Blink");
        CurSpatial.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        CurSpatial.SetActive(false);
        StopAllCoroutines();
    }
}
