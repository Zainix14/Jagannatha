using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_UI_Update_PotarSolo : MonoBehaviour
{
    [SerializeField]
    GameObject disquePotar;

    [SerializeField]
    Text textValue;

    [SerializeField]
    Text textWanted;


    GameObject Mng_SyncVar = null;
    SC_SyncVar_BreakdownTest sc_syncvar;

    // Start is called before the first frame update
    void Start()
    {
        Mng_SyncVar = GameObject.FindGameObjectWithTag("Mng_SyncVar");
    }

    // Update is called once per frame
    void Update()
    {
        

        if (sc_syncvar == null || Mng_SyncVar == null)
        {
            if (Mng_SyncVar == null)
                Mng_SyncVar = GameObject.FindGameObjectWithTag("Mng_SyncVar");

            if (Mng_SyncVar != null)
                sc_syncvar = Mng_SyncVar.GetComponent<SC_SyncVar_BreakdownTest>();

        }
        if (sc_syncvar != null)
        {
            updatePotarRender();
            /*
            //PANNE
            if (sc_syncvar.potar1isEnPanne)
            {
                textWanted.enabled = true;
                textWanted.text = sc_syncvar.potar1valueWanted.ToString();
            }
            //NO PANNE
            else
            {
                textWanted.enabled = false;
            }
            */
        }
    }

    void updatePotarRender()
    {
        /*
        textValue.text = sc_syncvar.potar1value.ToString();
        float rotZ = sc_syncvar.potar1value;

        disquePotar.gameObject.transform.eulerAngles = new Vector3(270, rotZ, 0);
        */
    }
}
