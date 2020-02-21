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

    [Range(0.0f, 20f)]
    [SerializeField]
    float phase = 0.0f;
    float curPhase;

    [Range(0f,1f)]
    [SerializeField]
    float amplitude; //Hauteur de la courbe
    float curAmplitude; //Hauteur de la courbe
    

    [Range(0.0f, 1.0f)]
    [SerializeField]
    float frequence; //Frequence de la courbe 
    float curFrequence; //Frequence de la courbe 

    [Range(0, 255)]
    [SerializeField]
    Vector2 colorRcolorV;

    Color32 curColor;

    float frequenceWanted = 110;
    float amplitudeWanted = 0.8f;

    public int indexDouble1;
    public int indexDouble2;
    [Range(1,30)]
    [SerializeField]
    float speed = 1;

    //[SerializeField]
    GameObject warning;
    //[SerializeField]
    GameObject sparkle;

    GameObject Mng_SyncVar = null;
    SC_SyncVar_calibr sc_syncvar;


    public SC_RaycastRealWorld scriptRaycast;


    void Start()
    {
        line = this.gameObject.GetComponent<LineRenderer>(); //Stockage de lui-meme
        //lineColor =  this.get
        lineWanted.enabled = false;
        Mng_SyncVar = GameObject.FindGameObjectWithTag("Mng_SyncVar");
        GetReferences();
        lineWanted.enabled = true;
    }

    void Update()
    {
        
        updateLineRender();
        
        if (sc_syncvar == null || Mng_SyncVar == null)
        {
            GetReferences();

        }
        if (sc_syncvar != null)
        {
            //frequence = sc_syncvar.SL_sliders[indexDouble1].value*155 +110;
            //amplitude = sc_syncvar.SL_sliders[indexDouble2].value*1.5f + 0.8f;
            Debug.Log("index 0 : " + sc_syncvar.CalibrInts[0]);
            Debug.Log("index 1 : " + sc_syncvar.CalibrInts[1]);
            Debug.Log("index 2 : " + sc_syncvar.CalibrInts[2]);

            
            //frequence = sc_syncvar.CalibrInts[0] * 28 + 40;
            //amplitude = sc_syncvar.CalibrInts[1] * 0.28f + 0.1f;

            //if (sc_syncvar.SL_sliders[indexDouble1].isEnPanne || sc_syncvar.SL_sliders[indexDouble2].isEnPanne)
            //{
            //    warning.SetActive(true);
            //    sparkle.SetActive(false);
            //    lineWanted.enabled = true;
            //    frequenceWanted = sc_syncvar.SL_sliders[indexDouble1].valueWanted * 155 + 110;
            //    amplitudeWanted = sc_syncvar.SL_sliders[indexDouble2].valueWanted * 1.5f + 0.8f;

            //}
            //NO PANNE
            //else
            //{
            //    lineWanted.enabled = false;
            //    warning.SetActive(false);
            //    sparkle.SetActive(true);
            //}

        }


        //securityCheck();
        
    }

    void updateLineRender()
    {
        //LIGNE JOUEUR
        /*
        line.positionCount = Mathf.CeilToInt(frequence); //Configuration du nombre 
        for (int i = 0; i < line.positionCount; i++)
        {
            float x = taille / frequence * i; //Valeur de X
            float y = Mathf.Sin(Time.time + i * speed) * amplitude; //Valeur de Y
            line.SetPosition(i, new Vector3(y, 0f, x)); //Distribution des valeurs dans le tableau (index, Vector3)

        }
        */


        curAmplitude =  ratio(sc_syncvar.CalibrInts[0], 6, 1.5f, 0, 0.1f);
        curFrequence =  ratio(sc_syncvar.CalibrInts[1], 6, 0.33f, 0, 0);
        curPhase = ratio(sc_syncvar.CalibrInts[2], 6, 20, 0, 0);

        line.positionCount = 300; //Configuration du nombre 
        for (int i = 0; i < line.positionCount; i++)
        {
            float x = (i*100f/line.positionCount); //Valeur de X
            float y = Mathf.Sin((Time.time * speed) + (i+curPhase) * curFrequence) * curAmplitude; //0;// Mathf.Sin(Time.time + i * speed) * amplitude; //Valeur de Y
            line.SetPosition(i, new Vector3(y, 0f, x)); //Distribution des valeurs dans le tableau (index, Vector3)

        }

        //LIGNE PANNE

        lineWanted.positionCount = 300; //Configuration du nombre 
        for (int i = 0; i < lineWanted.positionCount; i++)
        {
            /*
             float xWanted = 100f / frequenceWanted * i; //Valeur de X
             float yWanted = Mathf.Sin(Time.time + i * speed) * amplitudeWanted; //Valeur de Y
             lineWanted.SetPosition(i, new Vector3(yWanted, 0f, xWanted));
             */
            float x = (i * 100f / lineWanted.positionCount); //Valeur de X
            float y = Mathf.Sin((Time.time * speed) + (i + ratio(scriptRaycast.sensi.z,6,20,0,0)) * ratio(scriptRaycast.sensi.y, 6, 0.33f,0,0)) * ratio(scriptRaycast.sensi.x, 6, 1.5f, 0, 0.1f); //0;// Mathf.Sin(Time.time + i * speed) * amplitude; //Valeur de Y
            lineWanted.SetPosition(i, new Vector3(y, 0f, x)); //Distribution des valeurs dans le tableau (index, Vector3)
        }
    }

    void GetReferences()
    {
        if (Mng_SyncVar == null)
            Mng_SyncVar = GameObject.FindGameObjectWithTag("Mng_SyncVar");
        if (Mng_SyncVar != null && sc_syncvar == null)
            sc_syncvar = Mng_SyncVar.GetComponent<SC_SyncVar_calibr>();
    }

    void securityCheck()
    {
        if (amplitude < 0.2f) amplitude = 0.2f;
        if (amplitude > 2) amplitude = 2;
        if (frequence < 10) frequence = 10;
        if (frequence > 200) frequence = 200;
            
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
