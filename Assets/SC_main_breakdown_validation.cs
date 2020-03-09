using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_main_breakdown_validation : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {

    }
    /*
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
    */
        // Update is called once per frame
        void Update()
    {

        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("alaide");
            this.GetComponent<Animator>().SetBool("BOUGE", true);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            this.GetComponent<Animator>().SetBool("BOUGE", false);
        }

    }
}
