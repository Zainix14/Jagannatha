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
        for (int i = 0; i < 4; i++)
        {
            if (i == index)
            {
                ongletContainer.curIndex = i;
                ongletContainer.checkActive();
                //ongletContainer.child[i].transform.localPosition = new Vector3(0, 0, 0);
            }
            else
            {
                //ongletContainer.child[i].transform.localPosition = new Vector3(0, 0, 400);
            }
        }   
    }

    public void isBreakdownSystem(bool state)
    {
        if(state)
        {
            this.warningNotif.SetBool("b_OnNotif", true);
            
        }
        else
        {
            this.warningNotif.SetBool("b_OnNotif", false);
        }
    }

}
