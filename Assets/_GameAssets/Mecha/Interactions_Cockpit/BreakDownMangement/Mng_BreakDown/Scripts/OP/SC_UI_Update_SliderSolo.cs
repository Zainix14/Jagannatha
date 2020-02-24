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
                Debug.Log(sc_syncvar.SL_sliders[index].valueWanted);
                textWanted.text = Mathf.RoundToInt(ratio(sc_syncvar.SL_sliders[index].valueWanted,0.4f,10,-0.4f,0)).ToString();
                
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

        textValue.text = Mathf.RoundToInt(ratio(sc_syncvar.SL_sliders[index].value, 0.4f, 10, -0.4f, 0)).ToString();
        float rotZ = sc_syncvar.SL_sliders[index].value * 100;

        disquePotar.gameObject.transform.eulerAngles = new Vector3(270, rotZ, 0);
        
    }

    /**
     * Return the input value according a given range translated to an other range.
     * @param float inputValue
     * @param float inputMax
     * @param float outputMax
     * @param float inputMin
     * @param float outputMin
     * @return float
     */
    float ratio(float inputValue, float inputMax, float outputMax, float inputMin = 0.0f, float outputMin = 0.0f)
    {
        float product = (inputValue - inputMin) / (inputMax - inputMin);
        float output = ((outputMax - outputMin) * product) + outputMin;
        return output;
    }
}
