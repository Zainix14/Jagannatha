using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Permet d'animer une line renderer (ici sinus)
/// </summary>
public class SC_UI_Update_frequenceLine : MonoBehaviour
{
    //[SerializeField]
    LineRenderer line; //Lui-meme 
    [SerializeField]
    LineRenderer lineWanted; //Pattern Panne 


    [Range(0.1f,1.5f)]
    [SerializeField]
    float amplitude; //Hauteur de la courbe ---- POTAR2

    [Range(0.1f, 200)]
    //[SerializeField]
    float taille = 102;

    [Range(40, 180)]
    [SerializeField]
    float frequence; //Frequence de la courbe ---- POTAR3

    float frequenceWanted = 110;
    float amplitudeWanted = 0.8f;

    public int indexDouble1;
    public int indexDouble2;
    [Range(1,15)]
    //[SerializeField]
    int speed = 1;

    GameObject Mng_SyncVar = null;
    SC_SyncVar_BreakdownTest sc_syncvar;

    public int offset;

    void Start()
    {
        line = this.gameObject.GetComponent<LineRenderer>(); //Stockage de lui-meme
    
        lineWanted.enabled = false;
        Mng_SyncVar = GameObject.FindGameObjectWithTag("Mng_SyncVar");
    }

    void Update()
    {
        
        updateLineRender();
        
        if (sc_syncvar == null || Mng_SyncVar == null)
        {
            if (Mng_SyncVar == null)
                Mng_SyncVar = GameObject.FindGameObjectWithTag("Mng_SyncVar");

            if (Mng_SyncVar != null)
                sc_syncvar = Mng_SyncVar.GetComponent<SC_SyncVar_BreakdownTest>();

        }
        if (sc_syncvar != null)
        {

            //frequence = sc_syncvar.potar2value + 110;
            //amplitude = sc_syncvar.potar3value/100 + 0.8f ;

            frequence = sc_syncvar.SL_sliders[indexDouble1].value*155 +110;
            amplitude = sc_syncvar.SL_sliders[indexDouble2].value*1.5f + 0.8f;
            //PANNE


            if (sc_syncvar.SL_sliders[indexDouble1].isEnPanne || sc_syncvar.SL_sliders[indexDouble2].isEnPanne)
            {

                lineWanted.enabled = true;
                frequenceWanted = sc_syncvar.SL_sliders[indexDouble1].valueWanted * 155 + 110;
                amplitudeWanted = sc_syncvar.SL_sliders[indexDouble2].valueWanted * 1.5f + 0.8f;

            }
            //NO PANNE
            else
            {
                lineWanted.enabled = false;
            }

        }


        //securityCheck();
        
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

        //LIGNE PANNE
        lineWanted.positionCount = Mathf.CeilToInt(frequenceWanted); //Configuration du nombre 
        for (int i = 0; i < lineWanted.positionCount; i++)
        {
           
            float xWanted = taille / frequenceWanted * i; //Valeur de X
            float yWanted = Mathf.Sin(Time.time + i * speed) * amplitudeWanted; //Valeur de Y
            lineWanted.SetPosition(i, new Vector3(yWanted, 0f, xWanted));
        }
    }

    void securityCheck()
    {
        if (amplitude < 0.2f) amplitude = 0.2f;
        if (amplitude > 2) amplitude = 2;
        if (frequence < 10) frequence = 10;
        if (frequence > 200) frequence = 200;
            
    }
}
