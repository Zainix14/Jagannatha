using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_UI_Update_StateMecha_Display : MonoBehaviour
{
    SC_SyncVar_StateMecha_Display sc_syncvar;
    GameObject Mng_SyncVar = null;

    [SerializeField]
    Image exampleDisplay;

    [SerializeField]
    GameObject warning;
    [SerializeField]
    GameObject sparkle;

    int goodDisplay = 0;
    int badDisplay = 0;

    Image[] displays ;

    Material curMaterial;
    Material breakdownMaterial;

    GridLayoutGroup groupOfDisplays;
    RectTransform transformCaca;
    // Start is called before the first frame update
    void Start()
    {
        transformCaca = this.GetComponent<RectTransform>();
        groupOfDisplays= this.GetComponent<GridLayoutGroup>();
        
        Mng_SyncVar = GameObject.FindGameObjectWithTag("Mng_SyncVar");
        GetReferences();
        groupOfDisplays.constraintCount = (sc_syncvar.nbDisplay / 4) +1;
        displays = new Image[sc_syncvar.nbDisplay];

        for (int i =0; i< sc_syncvar.nbDisplay ; i++)
        {
            displays[i] = Instantiate(exampleDisplay, this.transform);
            displays[i].GetComponent<SC_UI_StateMecha_CheckDisplay>().index = i;
        }

        float sizeX = (transformCaca.sizeDelta.x / groupOfDisplays.constraintCount) - groupOfDisplays.spacing.x*2;
        float sizeY = (transformCaca.sizeDelta.y / (sc_syncvar.nbDisplay/ groupOfDisplays.constraintCount +1) - groupOfDisplays.spacing.y * 2) ;

        groupOfDisplays.cellSize = new Vector2(sizeX,sizeY);
    }

    // Update is called once per frame
    void Update()
    {
        if (sc_syncvar == null || Mng_SyncVar == null)
            GetReferences();

        if (sc_syncvar != null)
        {
            goodDisplay = 0;

            for (int i = 0; i < sc_syncvar.nbDisplay; i++)
            {
                if (sc_syncvar.displayAll[i] == true)
                {
                    warning.SetActive(true);
                    sparkle.SetActive(false);
                    goodDisplay = 0;
                    displays[i].GetComponent<SC_UI_StateMecha_CheckDisplay>().changeColorOnDisplayBreakdown();
                }
                else
                {
                    goodDisplay++;
                    displays[i].GetComponent<SC_UI_StateMecha_CheckDisplay>().changeColorOnDisplayNeutral();
                }
            }
            if (goodDisplay >= sc_syncvar.nbDisplay)
            {
                warning.SetActive(false);
                sparkle.SetActive(true);
            }

            Debug.Log(goodDisplay);
        }
    }

    void GetReferences()
    {
        if (Mng_SyncVar == null)
            Mng_SyncVar = GameObject.FindGameObjectWithTag("Mng_SyncVar");
        if (Mng_SyncVar != null && sc_syncvar == null)
            sc_syncvar = Mng_SyncVar.GetComponent<SC_SyncVar_StateMecha_Display>();
    }

    
}

