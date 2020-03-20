using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_SpatialDamage : MonoBehaviour
{
    public GameObject Light;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 21)
        {
            StartCoroutine(Blink());
        }
    }
    IEnumerator Blink()
    {
        Debug.Log("Blink");
        Light.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        Light.SetActive(false);
        StopAllCoroutines();
    }
}
