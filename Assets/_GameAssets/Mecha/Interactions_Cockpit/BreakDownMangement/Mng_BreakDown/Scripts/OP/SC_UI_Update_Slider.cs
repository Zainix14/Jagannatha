using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_UI_Update_Slider : MonoBehaviour
{

    SC_SyncVar_BreakdownTest sc_syncvar;
    GameObject Mng_SyncVar = null;

    //index du slider
    public int index;

    //Image of breakdown
    [SerializeField]
    Image barrettePanne;

    //FX (Soso)
    [SerializeField]
    GameObject warning;
    [SerializeField]
    GameObject sparkle;

    //Mesh and Material
    [SerializeField]
    GameObject visuelSlider;
    MeshRenderer curMesh;
    [SerializeField]
    Material neutralMaterial;
    [SerializeField]
    Material breakdownMaterial;
    [SerializeField]
    Material resolvedMaterial;
    float delay = 1f;

    //To put Green Mat
    bool isResolved = false;

    //Multiplier pour conversion slider (pilote) into slider (operator)
    float offSetMultiplier = 254;

    // Start is called before the first frame update
    void Start()
    {
        curMesh = visuelSlider.GetComponent<MeshRenderer>();
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
                float posY = sc_syncvar.SL_sliders[index].valueWanted * offSetMultiplier;
                warning.SetActive(true);
                sparkle.SetActive(false);
                barrettePanne.gameObject.transform.localPosition = new Vector3(barrettePanne.gameObject.transform.localPosition.x, posY, barrettePanne.gameObject.transform.localPosition.z);
                curMesh.material = breakdownMaterial;
                isResolved = false;
            }
            else
            {
                //curMesh.material = neutralMaterial;
                if(!isResolved)
                {
                    barrettePanne.enabled = false;
                    warning.SetActive(false);
                    sparkle.SetActive(true);
                    StartCoroutine(PutInStateResolved());
                }
                else
                {
                    curMesh.material = neutralMaterial;
                } 
            }
                
            
        }

    }

    void updateRenderSlider()
    {

        float posY1 = sc_syncvar.SL_sliders[index].value * offSetMultiplier;
        this.gameObject.transform.localPosition = new Vector3(this.gameObject.transform.localPosition.x, posY1, this.gameObject.transform.localPosition.z);

    }

    IEnumerator PutInStateResolved()
    {
        Debug.Log("IN THE COROUTINE BOY");
        curMesh.material = resolvedMaterial;
        yield return new WaitForSeconds(delay);
        isResolved = true;
    }

}
