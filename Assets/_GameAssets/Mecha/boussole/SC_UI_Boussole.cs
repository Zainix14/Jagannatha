using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_UI_Boussole : MonoBehaviour
{
    [SerializeField]
    Transform Target;

    Transform Boussole;
    // Start is called before the first frame update
    void Start()
    {
        Boussole = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
            Boussole.transform.localEulerAngles = new Vector3(-90, 0 , -90  - Target.transform.eulerAngles.y);
    }
}
