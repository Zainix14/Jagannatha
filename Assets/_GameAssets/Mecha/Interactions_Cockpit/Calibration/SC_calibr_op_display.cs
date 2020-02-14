using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_calibr_op_display : MonoBehaviour
{

    SC_SyncVar_calibr sc_syncvar;
    Text text_component_cur;

    GameObject Mng_SyncVar = null;


    [SerializeField]
    Text text_component_desired;

    public int index;

    // Start is called before the first frame update
    void Start()
    {
        text_component_cur = gameObject.GetComponent<Text>();
        Mng_SyncVar = GameObject.FindGameObjectWithTag("Mng_SyncVar");
        GetReferences();
    }

    void GetReferences()
    {
        if (Mng_SyncVar == null)
            Mng_SyncVar = GameObject.FindGameObjectWithTag("Mng_SyncVar");
        if (Mng_SyncVar != null && sc_syncvar == null)
            sc_syncvar = Mng_SyncVar.GetComponent<SC_SyncVar_calibr>();

    }

    // Update is called once per frame
    void Update()
    {

        text_component_cur.text = sc_syncvar.CalibrInts[index].ToString();

    }
}
