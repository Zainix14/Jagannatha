using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_UI_OngletSelection : MonoBehaviour, IF_clicableAction
{
    public int index;
    public SC_UI_OngletContainer ongletContainer;

    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Action()
    {
        //if(this.index != null)
        //{
        //    for(int i= 0; i < scriptWithChild.child.Length; i++)
        //    {
        //        if(i == index)
        //        {
        //            scriptWithChild.child[i].SetActive(true);
        //            scriptWithChild.checkActive();
        //        }
        //        else
        //        {
        //            scriptWithChild.child[i].SetActive(false);
        //        }

        //    }

        //}

        Debug.Log("Clic sur onglet" + index);
        for (int i = 0; i < ongletContainer.child.Length; i++)
        {
            if (i == index)
            {
                ongletContainer.curIndex = i;
                ongletContainer.checkActive();
                ongletContainer.child[i].transform.localPosition = new Vector3(0, 0, 0);
            }
            else
            {
                ongletContainer.child[i].transform.localPosition = new Vector3(0, 0, 400);
            }
        }

            
    }
}
