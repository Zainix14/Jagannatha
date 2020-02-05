using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_MainBreakDownManager : MonoBehaviour, IF_BreakdownManager
{

    public GameObject Mng_BreakDownAlert;
    public SC_BreakdownTestManager Mng_BreakDownTest;

    private SC_BreakdownOnBreakdownAlert SC_BreakDownAlert;

    public bool b_BreakEngine = false;

    public bool b_BreakMove = false;
    public bool b_BreakScreen = false;

    public bool b_BreakWeapons = false;

    public bool b_BreakMiniGun = false;
    public bool b_BreakFlameThrower = false;
    public bool b_BreakShrapnel = false;


    // Start is called before the first frame update
    void Start()
    {
        SC_BreakDownAlert = Mng_BreakDownAlert.GetComponent<SC_BreakdownOnBreakdownAlert>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckBreakdown()
    {
        //Debug.Log("babar");

        if(b_BreakEngine != Mng_BreakDownTest.b_BreakdownTest)
        {
            b_BreakEngine = Mng_BreakDownTest.b_BreakdownTest;
            if (b_BreakEngine)
                SC_BreakDownAlert.LaunchGlobalAlert();
            else if (!b_BreakEngine)
                SC_BreakDownAlert.StopGlobalAlert();
        }
    }

}
