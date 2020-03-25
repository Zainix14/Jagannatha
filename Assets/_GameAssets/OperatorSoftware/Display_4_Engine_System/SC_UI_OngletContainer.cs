using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_UI_OngletContainer : MonoBehaviour
{

    #region Singleton

    private static SC_UI_OngletContainer _instance;
    public static SC_UI_OngletContainer Instance { get { return _instance; } }

    #endregion

    //[SerializeField]
    public GameObject[] child;


    [SerializeField]
    GameObject[] onglet;
    Vector3 ongletActifPos;
    public Transform particleFB;
    //public RectTransform particleRect;
    bool toPlace = false;
    // Start is called before the first frame update
    public int curIndex;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    void Start()
    {
        particleFB = particleFB.GetComponent<RectTransform>();
        curIndex = 0;
        //checkActive();
        preshotPos();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (toPlace)
        {
            particleFB.localPosition = Vector3.Lerp(ongletActifPos, particleFB.localPosition, Time.deltaTime*50f);
            if (particleFB.transform.localPosition == ongletActifPos && ongletActifPos != Vector3.zero)
            {
                toPlace = false;
            }

        }
         
    }

    public void checkActive()
    {
        for(int i =0; i< child.Length;i++)
        {
            if(i == curIndex)
            {
                ongletActifPos = onglet[i].transform.localPosition;
                toPlace = true;
            }
            
        }
    }
    //Only for the start
    void preshotPos()
    {
        
        for (int i = 0; i < child.Length; i++)
        {
            if (i == curIndex)
            {
               child[i].transform.localPosition = new Vector3(0, 0, 0);
            }
            else
            {
                child[i].transform.localPosition = new Vector3(0, 0, 400);
            }
        }
    }

    public void playDisplayTabAlert(bool state)
    {
        onglet[0].GetComponent<SC_UI_OngletSelection>().isBreakdownSystem(state);
    }

    public void playWeaponTabAlert(bool state)
    {
        onglet[1].GetComponent<SC_UI_OngletSelection>().isBreakdownSystem(state);
    }
    public void playMovementTabAlert(bool state)
    {
        onglet[2].GetComponent<SC_UI_OngletSelection>().isBreakdownSystem(state);
    }
}
