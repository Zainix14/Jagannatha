﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_UI_Update_Slider : MonoBehaviour
{

    SC_SyncVar_BreakdownTest sc_syncvar;
    GameObject Mng_SyncVar = null;

    [SerializeField]
    button bouton;

    enum button
    {
        slider1,
        slider2,
        slider3
    }

    [SerializeField]
    Image barrettePanne;

    float tktEtienne = 290;

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
            switch (bouton)
            {
                #region Slider
                case button.slider1:

                    if(sc_syncvar.slider1isEnPanne)
                    {
                        //PANNE
                        barrettePanne.enabled = true;
                        float posY = sc_syncvar.slider1valueWanted * tktEtienne;
                        barrettePanne.gameObject.transform.localPosition = new Vector3(barrettePanne.gameObject.transform.localPosition.x, posY, barrettePanne.gameObject.transform.localPosition.z);
                    }
                    else
                    {
                        //OUT OF PANNE
                        barrettePanne.enabled = false;
                    }
                    break;

                case button.slider2:


                    if (sc_syncvar.slider2isEnPanne)
                    {
                        //PANNE
                        barrettePanne.enabled = true;
                        float posY = sc_syncvar.slider2valueWanted * tktEtienne;
                        barrettePanne.gameObject.transform.localPosition = new Vector3(barrettePanne.gameObject.transform.localPosition.x, posY, barrettePanne.gameObject.transform.localPosition.z);
                    }
                    else
                    {
                        //OUT OF PANNE
                        barrettePanne.enabled = false;
                    }
                    break;

                case button.slider3:


                    if (sc_syncvar.slider3isEnPanne)
                    {
                        //PANNE
                        barrettePanne.enabled = true;
                        float posY = sc_syncvar.slider3valueWanted * tktEtienne;
                        barrettePanne.gameObject.transform.localPosition = new Vector3(barrettePanne.gameObject.transform.localPosition.x, posY, barrettePanne.gameObject.transform.localPosition.z);
                    }
                    else
                    {
                        //OUT OF PANNE
                        barrettePanne.enabled = false;
                    }
                    break;

                    #endregion

            }

        }

    }

    void updateRenderSlider()
    {
        

        switch (bouton)
        {
            case button.slider1:
                float posY1 = sc_syncvar.slider1value* tktEtienne;
                this.gameObject.transform.localPosition = new Vector3(this.gameObject.transform.localPosition.x, posY1, this.gameObject.transform.localPosition.z);
                break;

            case button.slider2:
                float posY2 = sc_syncvar.slider2value * tktEtienne;
                this.gameObject.transform.localPosition = new Vector3(this.gameObject.transform.localPosition.x, posY2, this.gameObject.transform.localPosition.z);
                break;
            case button.slider3:
                float posY3 = sc_syncvar.slider3value * tktEtienne;
                this.gameObject.transform.localPosition = new Vector3(this.gameObject.transform.localPosition.x, posY3, this.gameObject.transform.localPosition.z);
                break;
        }

    }

}