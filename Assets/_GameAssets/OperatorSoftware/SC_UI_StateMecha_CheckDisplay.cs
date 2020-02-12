using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_UI_StateMecha_CheckDisplay : MonoBehaviour
{
    SC_SyncVar_StateMecha_Display sc_syncvar;
    GameObject Mng_SyncVar = null;

    Image curImage;

    Material curMat;
    [SerializeField]
    Material breakdownMat;

    public int index;
    // Start is called before the first frame update
    void Start()
    {
        Mng_SyncVar = GameObject.FindGameObjectWithTag("Mng_SyncVar");
        GetReferences();
        curImage = this.GetComponent<Image>();
        curMat = this.GetComponent<Image>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (sc_syncvar == null || Mng_SyncVar == null)
            GetReferences();

        if (sc_syncvar != null)
        {
            
        }
    }

    void GetReferences()
    {
        if (Mng_SyncVar == null)
            Mng_SyncVar = GameObject.FindGameObjectWithTag("Mng_SyncVar");
        if (Mng_SyncVar != null && sc_syncvar == null)
            sc_syncvar = Mng_SyncVar.GetComponent<SC_SyncVar_StateMecha_Display>();
    }

    public void changeColorOnDisplayBreakdown()
    {
            curImage.material = breakdownMat;
        
    }
    public void changeColorOnDisplayNeutral()
    {
            curImage.material = curMat;

    }
}
