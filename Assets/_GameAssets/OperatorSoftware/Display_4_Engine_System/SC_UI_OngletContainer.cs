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
    [SerializeField]
    Vector3[] hubInOngletPos;
    public Transform particleFB;
    //public RectTransform particleRect;
    bool toPlace = false;
    bool hubOn = true;
    // Start is called before the first frame update
    public int curIndex;

    [SerializeField]
    float speedAnimOnglet;
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
        checkActive();
        //preshotPos();
    }

    // Update is called once per frame
    void Update()
    {
        //if (toPlace)
        //{
        //    particleFB.localPosition = Vector3.Lerp(ongletActifPos, particleFB.localPosition, Time.deltaTime*50f);
        //    if (particleFB.transform.localPosition == ongletActifPos && ongletActifPos != Vector3.zero)
        //    {
        //        toPlace = false;
        //    }

        //}
        if (!hubOn)
        {
            child[curIndex].transform.localPosition = Vector3.Lerp(child[curIndex].transform.localPosition, Vector3.zero, Time.deltaTime * speedAnimOnglet);
            child[curIndex].transform.localScale = Vector3.Lerp(child[curIndex].transform.localScale, Vector3.one, Time.deltaTime * speedAnimOnglet);
            child[0].transform.localPosition = hubInOngletPos[curIndex - 1];
            child[0].transform.localScale = Vector3.zero;
            
        }
        else
        {
            child[0].transform.localPosition = Vector3.Lerp(child[0].transform.localPosition, Vector3.zero, Time.deltaTime * speedAnimOnglet);
            child[0].transform.localScale = Vector3.Lerp(child[0].transform.localScale, Vector3.one, Time.deltaTime * speedAnimOnglet);
        }
         
    }

    public void checkActive()
    {
        if (curIndex == 0)
        {
            hubOn = true;
            Debug.Log("Hub");
            for (int i = 0; i < onglet.Length; i++)
            {
                //child[i].transform.position = onglet[i+1].transform.localPosition;
                child[i+1].transform.localPosition = onglet[i].transform.localPosition;
                child[i + 1].transform.localScale = Vector3.zero;
            }
        }
        else
        {
            hubOn = false;
            for (int i = 0; i < child.Length; i++)
            {

                if (i == curIndex)
                {
                    //ongletActifPos = onglet[i - 1].transform.localPosition;
                    
                    toPlace = true;
                }
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
