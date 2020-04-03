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

    public enum Window
    {
        Hub = 3,
        Display = 0,
        Weapon = 1,
        Movement = 2
    }

    Window curWindow;

    [SerializeField]
    GameObject[] onglet;

    [SerializeField]
    Vector3[] positionHubTransition;
    [SerializeField]
    Vector3[] scaleHubTransition;


    [SerializeField]
    Vector3[] positionWindowTranstion;
    [SerializeField]
    Vector3[] scaleWindowTransition;


    public Transform particleFB;


    [SerializeField]
    float ZoomInHubDuration_1;
    [SerializeField]
    float ZoomInHubDuration_2;
    [SerializeField]
    float ZoomInWindowDuration_1;
    [SerializeField]
    float ZoomInWindowDuration_2;


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


    }

    public void ChangeWindow(Window newWindow)
    {
        if(newWindow == Window.Hub)
        {
            StartCoroutine(ZoomInWindow_1(curWindow));
        }
        else
        {
            StartCoroutine(ZoomInHub_1(newWindow));
        }
        curWindow = newWindow;


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


    #region ZoomFromHub

    IEnumerator ZoomInHub_1(Window newWindow)
    {
        int hubIndex = (int)Window.Hub;
        int WindowIndex = (int)newWindow;

        Vector3 initPos = system[hubIndex].transform.localPosition;
        Vector3 endPos = positionHubTransition[WindowIndex];
        Vector3 dPosPerSec = (endPos - initPos) / ZoomInHubDuration_1;

        Vector3 initScale = system[hubIndex].transform.localScale;
        Vector3 endScale = scaleHubTransition[WindowIndex];
        Vector3 dScalePerSec = (endScale - initScale) / ZoomInHubDuration_1;


        float t = 0;
        while(t < ZoomInHubDuration_1)
        {
            t += Time.deltaTime;

            system[hubIndex].transform.localPosition += (dPosPerSec *Time.deltaTime);
            system[hubIndex].transform.localScale += (dScalePerSec * Time.deltaTime);

       
            yield return 0;
        }

        StopAllCoroutines();
        system[hubIndex].transform.localScale = Vector3.zero;
        system[hubIndex].transform.localPosition = Vector3.zero;
        system[WindowIndex].transform.localScale = Vector3.zero;
        system[WindowIndex].transform.localPosition = Vector3.zero;
        StartCoroutine(ZoomInHub_2(newWindow));
        
    }

    IEnumerator ZoomInHub_2(Window newWindow)
    {
        int WindowIndex = (int)newWindow;

        Vector3 initScale = Vector3.zero;
        Vector3 endScale = Vector3.one;
        Vector3 dScalePerSec = (endScale - initScale) / ZoomInHubDuration_2;

        float t = 0;
        while (t < ZoomInHubDuration_2)
        {
            t += Time.deltaTime;
            system[WindowIndex].transform.localScale += (dScalePerSec * Time.deltaTime);

            yield return 0;
        }

        system[WindowIndex].transform.localScale = endScale;
        StopAllCoroutines();
        //system[(int)Window.Hub].transform.localPosition = positionHubTransition[WindowIndex];

    }

    #endregion

    #region ZoomFromWindow

    IEnumerator ZoomInWindow_1(Window curWindow)
    {
        int hubIndex = (int)Window.Hub;
        int curWindowIndex = (int)curWindow;

        Vector3 initPos = system[curWindowIndex].transform.localPosition;
        Vector3 endPos = positionWindowTranstion[curWindowIndex];
        Vector3 dPosPerSec = (endPos - initPos) / ZoomInWindowDuration_1;

        Vector3 initScale = system[curWindowIndex].transform.localScale;
        Vector3 endScale = scaleWindowTransition[curWindowIndex];
        Vector3 dScalePerSec = (endScale - initScale) / ZoomInWindowDuration_1;


        float t = 0;
        while (t < ZoomInWindowDuration_1)
        {
            t += Time.deltaTime;

            system[curWindowIndex].transform.localPosition += (dPosPerSec * Time.deltaTime);
            system[curWindowIndex].transform.localScale += (dScalePerSec * Time.deltaTime);


            yield return 0;
        }

        StopAllCoroutines();
        system[hubIndex].transform.localScale = Vector3.zero;
        system[hubIndex].transform.localPosition = Vector3.zero;
        system[curWindowIndex].transform.localScale = Vector3.zero;
        system[curWindowIndex].transform.localPosition = Vector3.zero;
        StartCoroutine(ZoomInWindow_2());

    }

    IEnumerator ZoomInWindow_2()
    {
        int hubIndex = (int)Window.Hub;

        Vector3 initScale = Vector3.zero;
        Vector3 endScale = Vector3.one;
        Vector3 dScalePerSec = (endScale - initScale) / ZoomInHubDuration_2;

        float t = 0;
        while (t < ZoomInWindowDuration_2)
        {
            t += Time.deltaTime;
            system[hubIndex].transform.localScale += (dScalePerSec * Time.deltaTime);

            yield return 0;
        }


        system[hubIndex].transform.localScale = endScale;
        StopAllCoroutines();
        //system[(int)Window.Hub].transform.localPosition = positionHubTransition[WindowIndex];

    }

    #endregion
}
