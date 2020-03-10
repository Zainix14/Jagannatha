using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_UI_OngletSelection : MonoBehaviour, IF_clicableAction
{
    public int index;
    public SC_UI_OngletContainer scriptWithChild;
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
        Debug.Log("DO THE ACTION BY INTERFACE");

        if(this.index != null)
        {
            for(int i= 0; i < scriptWithChild.child.Length; i++)
            {
                if(i == index)
                {
                    scriptWithChild.child[i].SetActive(true);
                }
                else
                {
                    scriptWithChild.child[i].SetActive(false);
                }
                    
            }
            
        }

       
    }
}
