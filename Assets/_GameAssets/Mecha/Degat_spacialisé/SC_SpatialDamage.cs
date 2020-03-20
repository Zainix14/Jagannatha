using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_SpatialDamage : MonoBehaviour
{
    public GameObject Damage;
    public Color curColor;

    // Start is called before the first frame update
    void Start()
    {
        curColor = Damage.GetComponent<Renderer>().material.GetColor("_Color");
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator TakeDamage()
    {
        Damage.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        yield return new WaitForSeconds(0.5f);
        Damage.GetComponent<Renderer>().material.SetColor("_Color", curColor);
        StopAllCoroutines();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "MechBullets")
        {
            StartCoroutine(TakeDamage());
            Debug.Log("hit");
        }
    }
}
