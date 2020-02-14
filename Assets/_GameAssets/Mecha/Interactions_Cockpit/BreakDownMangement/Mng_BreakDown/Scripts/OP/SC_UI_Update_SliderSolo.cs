using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_UI_Update_SliderSolo : MonoBehaviour
{
    [SerializeField]
    GameObject disquePotar;

    [SerializeField]
    Text textValue;

    [SerializeField]
    Text textWanted;

    [SerializeField]
    GameObject warning;
    [SerializeField]
    GameObject sparkle;

    GameObject Mng_SyncVar = null;
    SC_SyncVar_BreakdownTest sc_syncvar;

    public int index;
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
            updateSliderSolo();
            
            //PANNE
            if (sc_syncvar.SL_sliders[index].isEnPanne)
            {
                textWanted.enabled = true;
                warning.SetActive(true);
                sparkle.SetActive(false);
                textWanted.text = sc_syncvar.SL_sliders[index].valueWanted.ToString();
                
            }
            //NO PANNE
            else
            {
                warning.SetActive(false);
                sparkle.SetActive(true);
                textWanted.enabled = false;
            }
            
        }
    }

    void updateSliderSolo()
    {
        
        textValue.text = sc_syncvar.SL_sliders[index].value.ToString();
        float rotZ = sc_syncvar.SL_sliders[index].value * 100;

        disquePotar.gameObject.transform.eulerAngles = new Vector3(270, rotZ, 0);
        
    }
}
