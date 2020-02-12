using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_UI_Update_Slider : MonoBehaviour
{

    SC_SyncVar_BreakdownTest sc_syncvar;
    GameObject Mng_SyncVar = null;
    /*
    [SerializeField]
    button bouton;

    enum button
    {
        slider1,
        slider2,
        slider3
    }
    */

    //index du slider
    public int index;

    [SerializeField]
    Image barrettePanne;

    //va te te faire
    float tktEtienne = 254;

    // Start is called before the first frame update
    void Start()
    {

        Mng_SyncVar = GameObject.FindGameObjectWithTag("Mng_SyncVar");
        GetReferences();
    }

    void GetReferences()
    {
        if (Mng_SyncVar == null)
            Mng_SyncVar = GameObject.FindGameObjectWithTag("Mng_SyncVar");
        if (Mng_SyncVar != null && sc_syncvar == null)
            sc_syncvar = Mng_SyncVar.GetComponent<SC_SyncVar_BreakdownTest>();
    }

    // Update is called once per frame
    void Update()
    {
        updateRenderSlider();


        if (sc_syncvar == null || Mng_SyncVar == null)
            GetReferences();

        if( sc_syncvar != null)
        {

            if (Input.GetKeyDown(KeyCode.T))
            {
                //Debug.Log(sc_syncvar.SL_sliders[0].isEnPanne);
            }

            if (sc_syncvar.SL_sliders[index].isEnPanne)
            {
                barrettePanne.enabled = true;
                float posY = sc_syncvar.SL_sliders[index].valueWanted * tktEtienne;
                
                barrettePanne.gameObject.transform.localPosition = new Vector3(barrettePanne.gameObject.transform.localPosition.x, posY, barrettePanne.gameObject.transform.localPosition.z);
            }
            else
                barrettePanne.enabled = false;
            
        }

    }

    void updateRenderSlider()
    {

        float posY1 = sc_syncvar.SL_sliders[index].value * tktEtienne;
        this.gameObject.transform.localPosition = new Vector3(this.gameObject.transform.localPosition.x, posY1, this.gameObject.transform.localPosition.z);

    }

}
