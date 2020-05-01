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
        for(int i =0; i < nbIndex; i++)
        {
            wireBlink.SetBreakDown(i, true,0.5f,1,1);
        }
    }

    public void StopTotalBreakDownBlink()
    {
        for (int i = 0; i < nbIndex; i++)
        {
            wireBlink.SetBreakDown(i, false);
        }
    }
}
