using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_UI_OngletSelection : MonoBehaviour, IF_clicableAction
{
    public int index;
    public SC_UI_OngletContainer ongletContainer;

    bool isBreakdown;

    [SerializeField]
    Animator warningNotif;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isBreakdown)
        {
            
            
        }
        else
        {
            
        }

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

    public void isBreakdownSystem(bool state)
    {
        if(state)
        {
            warningNotif.SetBool("b_OnNotif", true);
        }
        else
        {
            warningNotif.SetBool("b_OnNotif", false);
        }
        Debug.Log("Panne "+ state+ " à l'onglet : " + index);
    }
}
