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
        slider3,
        potar1,
        inter1,
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

                case button.slider2:
                    text_component_cur.text = sc_syncvar.slider2value.ToString();
                    text_component_desired.text = sc_syncvar.slider2valueWanted.ToString();
                    if (sc_syncvar.slider2isEnPanne)
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

                case button.slider3:
                    text_component_cur.text = sc_syncvar.slider3value.ToString();
                    text_component_desired.text = sc_syncvar.slider3valueWanted.ToString();
                    if (sc_syncvar.slider3isEnPanne)
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

                case button.potar1:
                    text_component_cur.text = sc_syncvar.potar1value.ToString();
                    text_component_desired.text = sc_syncvar.potar1valueWanted.ToString();
                    if (sc_syncvar.potar1isEnPanne)
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

                case button.inter1:
                    text_component_cur.text = sc_syncvar.inter1value.ToString();
                    text_component_desired.text = sc_syncvar.inter1valueWanted.ToString();
                    if (sc_syncvar.inter1isEnPanne)
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
