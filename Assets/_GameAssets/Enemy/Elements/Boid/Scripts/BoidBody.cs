using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidBody : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        target.transform.LookAt(target.transform);
    }
}
