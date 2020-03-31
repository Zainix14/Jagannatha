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
    public GameObject[] system;


    [SerializeField]
    GameObject[] onglet;
    [SerializeField]
    Vector3[] hubInOngletPos;
    [SerializeField]
    Vector3[] offsetInHub;
    [SerializeField]
    Vector3[] offsetInOnglet;
    public Transform particleFB;
    //public RectTransform particleRect;
    bool toPlace = false;
    bool hubOn = true;
    // Start is called before the first frame update
    public int curIndex;

    [SerializeField]
    float speedAnimOnglet;
    [SerializeField]
    float delayOfAnimHub;
    [SerializeField]
    float delayOfAnimOnglet;
    [SerializeField]
    float delayOfAnimOngletOut;
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
            Debug.Log("Update NO HUB");
            
            //child[0].transform.localPosition = hubInOngletPos[curIndex - 1];
        }
        else
        {
            Debug.Log("UPDATE HUB");
            
            //if(system[0].transform.localScale.x < 1)
            //{
            //    system[0].transform.localPosition = Vector3.Lerp(system[0].transform.localPosition, Vector3.zero, Time.deltaTime * speedAnimOnglet);
            //    system[0].transform.localScale = Vector3.Lerp(system[0].transform.localScale, Vector3.one, Time.deltaTime * speedAnimOnglet);
            //}
                   
        }
         
    }

    public void checkActive()
    {
        if (curIndex == 0)
        {
            hubOn = true;
            Debug.Log("Hub");
            StartCoroutine(startAnimOngletOut());
            for (int i = 0; i < onglet.Length; i++)
            {
                //child[i].transform.position = onglet[i+1].transform.localPosition;
                system[i + 1].transform.localPosition = onglet[i].transform.localPosition;
                system[i + 1].transform.localScale = Vector3.zero;
            }
        }
        else
        {
            hubOn = false;
            StartCoroutine(startAnimHub());
            for (int i = 0; i < system.Length; i++)
            {

                if (i == curIndex)
                {
                    //ongletActifPos = onglet[i - 1].transform.localPosition;
                    
                    toPlace = true;
                }
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

    IEnumerator startAnimHub()
    {
        float t = 0;
        while(t < delayOfAnimHub)
        {
            t += Time.deltaTime;
            system[0].transform.localPosition = Vector3.Lerp(system[0].transform.localPosition, offsetInHub[curIndex-1], Time.deltaTime * 1 / delayOfAnimHub);
            system[0].transform.localScale = Vector3.Lerp(system[0].transform.localScale, new Vector3(3, 3, 3), Time.deltaTime * 1 / delayOfAnimHub);

            //child[0].transform.localPosition = new Vector3(0,0,50);
            //system[curIndex].transform.localPosition = Vector3.Lerp(system[curIndex].transform.localPosition, Vector3.zero, Time.deltaTime * 1 / delayOfAnim);
            //system[curIndex].transform.localScale = Vector3.Lerp(system[curIndex].transform.localScale, Vector3.one, Time.deltaTime * 1 / delayOfAnim);
            
            Debug.Log("In coroutine");
            yield return 0;
        }
        system[0].transform.localScale = Vector3.zero;
        system[0].transform.localPosition = offsetInHub[curIndex-1];
        StopCoroutine(startAnimHub());
        StartCoroutine(startAnimOnglet());
        
    }

    IEnumerator startAnimOnglet()
    {
        float t = 0;
        while (t < delayOfAnimOnglet)
        {
            t += Time.deltaTime;
            system[curIndex].transform.localPosition = Vector3.Lerp(system[curIndex].transform.localPosition, Vector3.zero, Time.deltaTime * 1 / delayOfAnimOnglet);
            system[curIndex].transform.localScale = Vector3.Lerp(system[curIndex].transform.localScale, Vector3.one, Time.deltaTime * 1 / delayOfAnimOnglet);

            yield return 0;
        }
        system[0].transform.localPosition = offsetInHub[curIndex-1];
        StopCoroutine(startAnimOnglet());
    }

    IEnumerator startAnimOngletOut()
    {
        float t = 0;
        while (t < delayOfAnimOngletOut)
        {
            t += Time.deltaTime;
            system[curIndex].transform.localPosition = Vector3.Lerp(system[curIndex].transform.localPosition, offsetInOnglet[curIndex+1] , Time.deltaTime * 1 / delayOfAnimOnglet);
            system[curIndex].transform.localScale = Vector3.Lerp(system[curIndex].transform.localScale, new Vector3(3, 3, 3), Time.deltaTime * 1 / delayOfAnimOnglet);
            yield return 0;
        }
        system[0].transform.localPosition = offsetInHub[curIndex];
        StopCoroutine(startAnimOngletOut());
    }

}
