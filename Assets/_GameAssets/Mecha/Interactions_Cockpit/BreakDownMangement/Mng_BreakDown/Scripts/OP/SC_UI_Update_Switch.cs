using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_UI_Update_Switch : MonoBehaviour
{
    SC_SyncVar_BreakdownTest sc_syncvar;
    GameObject Mng_SyncVar = null;
    /*
    [SerializeField]
    button bouton;
    */
    Color curColor = new Color(255, 159, 0, 255);
    Color panneColor = new Color(255, 0, 0, 255);

    [SerializeField]
    Image stateInterrupteur;

     public int index;
    /*
    enum button
    {
        inter1,
        inter2,
        inter3,
        inter4,
        inter5,
    }*/
    // Start is called before the first frame update
    void Start()
    {
        Mng_SyncVar = GameObject.FindGameObjectWithTag("Mng_SyncVar");
        GetReferences();
    }

    // Update is called once per frame
    void Update()
    {
        if (sc_syncvar == null || Mng_SyncVar == null)
            GetReferences();

        if (sc_syncvar != null)
        {


           // Debug.Log("CLIENT : NB éléments ds struc : " + sc_syncvar.m_pows[index].power);
            
         



            if (sc_syncvar.SL_switches[index].isEnPanne)
            {
                stateInterrupteur.color = panneColor;
            }
            //NO PANNE
            else
            {
                stateInterrupteur.color = curColor;
            }



        }
    }

    void GetReferences()
    {
        if (Mng_SyncVar == null)
            Mng_SyncVar = GameObject.FindGameObjectWithTag("Mng_SyncVar");
        if (Mng_SyncVar != null && sc_syncvar == null)
            sc_syncvar = Mng_SyncVar.GetComponent<SC_SyncVar_BreakdownTest>();
    }
}
