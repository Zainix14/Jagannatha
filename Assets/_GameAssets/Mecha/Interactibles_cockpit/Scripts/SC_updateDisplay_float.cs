using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_updateDisplay_float : MonoBehaviour
{

    SC_SyncVar_Interactibles sc_syncvar;
    Text text_component_cur;


    [SerializeField]
    Text text_component_desired;

    [SerializeField]
    button bouton;

    enum button
    {
        slider1,
        slider2,
        slider3
    }

    // Start is called before the first frame update
    void Start()
    {
        text_component_cur = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sc_syncvar == null)
        {
            
            sc_syncvar = GameObject.FindGameObjectWithTag("Mng_SyncVar").GetComponent<SC_SyncVar_Interactibles>();

        }
        if( sc_syncvar != null)
        {
            switch (bouton)
            {
                case button.slider1:
                    text_component_cur.text = sc_syncvar.slider1value.ToString();
                    text_component_desired.text = sc_syncvar.slider1valueWanted.ToString();
                    if(sc_syncvar.slider1isEnPanne)
                    {
                        text_component_cur.color = Color.red;
                        text_component_desired.color = Color.red;
                    }
                    else
                    {

                        text_component_cur.color = Color.green;
                        text_component_desired.color = Color.green;
                    }
                    break;
                default:
                    break;

            }

        }

    }
    /*
    public void DisplayDesiredValue()
    {
        
    }*/
}
