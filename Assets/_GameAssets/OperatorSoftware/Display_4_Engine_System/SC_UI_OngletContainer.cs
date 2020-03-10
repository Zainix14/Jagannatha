using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_UI_OngletContainer : MonoBehaviour
{
    //[SerializeField]
    public GameObject[] child;


    [SerializeField]
    Image[] onglet;
    Vector3 ongletActifPos;
    public Transform particleFB;
    public RectTransform particleRect;
    bool toPlace = false;
    // Start is called before the first frame update
    void Start()
    {
        particleRect = particleFB.GetComponent<RectTransform>();
        checkActive();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (toPlace)
        {
            particleRect.localPosition = Vector3.Lerp(ongletActifPos, particleRect.localPosition, 0.5f);
            if (particleRect.transform.localPosition == ongletActifPos && ongletActifPos != Vector3.zero)
            {
                toPlace = false;
            }

        }
         
    }

    public void checkActive()
    {
        for(int i =0; i< child.Length;i++)
        {
            if(child[i].active)
            {
                ongletActifPos = onglet[i].transform.localPosition;
                toPlace = true;
            }
            
        }
    }
}
