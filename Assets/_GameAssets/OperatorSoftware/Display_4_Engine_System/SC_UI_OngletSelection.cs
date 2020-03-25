using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_UI_OngletSelection : MonoBehaviour, IF_clicableAction
{
    public int index;
    public SC_UI_OngletContainer ongletContainer;

    bool isBreakdown;

    [SerializeField]
    Animator warningNotif;
    [SerializeField]
    Image img_warningNotif;
    // Start is called before the first frame update
    void Start()
    {
        //img_warningNotif.enabled = true;
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
            //this.GetComponentInChildren<Image>().enabled = true;
            this.warningNotif.SetBool("b_OnNotif", true);
            Debug.Log("Anim play on " + index);
        }
        else
        {
            this.warningNotif.SetBool("b_OnNotif", false);
            Debug.Log("Anim stop on " + index);
            //Invoke("desactivateImage", 0.5f);
        }
        //Debug.Log("Panne "+ state+ " à l'onglet : " + index);
    }

    void desactivateImage()
    {
        this.GetComponentInChildren<Image>().enabled = false;
    }
}
