using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_UI_Cockpit_FrequenceLine : MonoBehaviour
{
    LineRenderer line;

    //[Range(0.1f, 1.5f)]
    [SerializeField]
    float amplitude = 0.8f; //Hauteur de la courbe

    //[Range(0.1f, 200)]
    [SerializeField]
    float taille = 102;

    //[Range(40, 180)]
    [SerializeField]
    float frequence = 110; //Frequence de la courbe 

    //[SerializeField]
    int speed = 1;


    GameObject Mng_SyncVar = null;
    SC_SyncVar_BreakdownTest sc_syncvar;

    public int indexDouble1;
    public int indexDouble2;

    void Start()
    {
        line = this.gameObject.GetComponent<LineRenderer>(); //Stockage de lui-meme
        Mng_SyncVar = GameObject.FindGameObjectWithTag("Mng_SyncVar");
        GetReferences();
    }

    // Update is called once per frame
    void Update()
    {
        updateLineRender();
        if (sc_syncvar == null || Mng_SyncVar == null)
        {
            GetReferences();

        }
        if (sc_syncvar != null)
        {
            frequence = sc_syncvar.SL_sliders[indexDouble1].value * 155 + 110;
            amplitude = sc_syncvar.SL_sliders[indexDouble2].value * 1.5f + 0.8f;

        }
    }
    void updateLineRender()
    {
        //LIGNE JOUEUR
        line.positionCount = Mathf.CeilToInt(frequence); //Configuration du nombre 
        for (int i = 0; i < line.positionCount; i++)
        {
            float x = taille / frequence * i; //Valeur de X
            float y = Mathf.Sin(Time.time + i * speed) * amplitude; //Valeur de Y
            line.SetPosition(i, new Vector3(y, 0f, x)); //Distribution des valeurs dans le tableau (index, Vector3)
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
