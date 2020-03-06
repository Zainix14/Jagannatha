using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_main_breakdown_validation : MonoBehaviour
{

    public GameObject posUp;
    public GameObject posDown;

    Vector3 vectorUp;
    Vector3 vectorDown;

    // Start is called before the first frame update
    void Start()
    {
        vectorDown = posDown.transform.position;
        vectorUp= posUp.transform.position;


    }

    IEnumerator getTo(Vector3 destination)
    {

        float i = 0.0f;
        float rate = 1.0f / 2;

        while (i < 1.0)
        {

            i += Time.deltaTime * rate;

            transform.position = Vector3.Lerp(transform.position, destination, i);
            

            Debug.Log("je ne suis pas arrêté, fdp =3");
            yield return null;

        }
        
    }

        // Update is called once per frame
        void Update()
    {

        if (Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine("getTo", vectorDown);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine("getTo", vectorUp);
        }

    }
}
