using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_UI_BlinkBreakdown : MonoBehaviour
{
    #region Singleton

    private static SC_UI_BlinkBreakdown _instance;
    public static SC_UI_BlinkBreakdown Instance { get { return _instance; } }
    #endregion

    SC_UI_WireBlink wireBlink;

    int nbIndex;

    Coroutine blinkCoro;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        wireBlink = GetComponent<SC_UI_WireBlink>();
        nbIndex = wireBlink.img_ToBreakDown.Length;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            StartTotalBreakDownBlink();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            StopTotalBreakDownBlink();
        }
    }
    // Start is called before the first frame update
    public void StartTotalBreakDownBlink()
    {
        if(blinkCoro != null)
        StopCoroutine(blinkCoro);
        blinkCoro = StartCoroutine(NewCoroutine());
    }

    IEnumerator NewCoroutine()
    {
        while(true)
        {
            int rndInt = Random.Range(0, 3);
            if(rndInt >0)
            {
                for (int i = 0; i < nbIndex; i++)
                {
                    wireBlink.SetBreakDown(i, true, 0.5f, 0.1f, 0.1f);
                }
                yield return new WaitForSeconds(0.5f);

            }
            else
            {
                float rnd = Random.Range(0.05f, 0.2f);
                for (int i = 0; i < nbIndex; i++)
                {
                    wireBlink.SetBreakDown(i, true, rnd, 0.1f, 1);
                }

                yield return new WaitForSeconds(rnd);
            }
       
        }
    }


    public void StopTotalBreakDownBlink()
    {
        StopCoroutine(blinkCoro);
        for (int i = 0; i < nbIndex; i++)
        {
            wireBlink.SetBreakDown(i, false);
        }
    }
}
