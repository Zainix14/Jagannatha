using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_UI_Display_MapInfos_StateManager : MonoBehaviour
{
    enum StateOfCanvas
    {
        neutral,
        koaView,
    }

    StateOfCanvas curState;

    public SC_RaycastRealWorld scriptRaycast; //Sur camera Full view

    int numChild;
    void Start()
    {
        curState = StateOfCanvas.neutral;
        numChild = this.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        checkState();
    }

    void checkState()
    {
        if (scriptRaycast.objectOnclic == null)
        {
            curState = StateOfCanvas.neutral;
            checkChild(0);
        }
        else if(scriptRaycast.objectOnclic.tag == "Koa")
        {
            curState = StateOfCanvas.koaView;
            checkChild(1);
        }
    }

    void checkChild(int indexToActivate)
    {
        for(int i = 0; i<numChild;i++)
        {
            if(i == indexToActivate)
            {
                activateChild(this.transform.GetChild(i));
            }
            else
            {
                desactivateChild(this.transform.GetChild(i));
            }
        }
 
    }
    
    void activateChild(Transform child)
    {
        child.transform.gameObject.SetActive(true);
    }
    void desactivateChild(Transform child)
    {
        child.transform.gameObject.SetActive(false);
    }

}
