using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_MainBreakDownManager : MonoBehaviour, IF_BreakdownManager
{

    public GameObject Mng_Checklist;
    public GameObject Mng_BreakDownAlert;
    public SC_BreakdownTestManager Mng_BreakDownTest;
    private SC_BreakdownOnBreakdownAlert SC_BreakDownAlert;

    public bool b_BreakEngine = false;

    GameObject MoveSystem;
    public bool b_BreakMove = false;

    GameObject RenderSystem;
    public bool b_BreakScreen = false;

    GameObject WeaponSystem;
    public bool b_BreakWeapons = false;

    GameObject MiniGunSystem;
    public bool b_BreakMiniGun = false;
    GameObject ShrapnelSystem;
    public bool b_BreakShrapnel = false;
    GameObject FlameSystem;
    public bool b_BreakFlameThrower = false;


    // Start is called before the first frame update
    void Start()
    {       
        GetReferences();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetReferences()
    {
        if (Mng_Checklist == null)
            Mng_Checklist = GameObject.FindGameObjectWithTag("Mng_CheckList");

        if(SC_BreakDownAlert == null)
            SC_BreakDownAlert = Mng_BreakDownAlert.GetComponent<SC_BreakdownOnBreakdownAlert>();

        if (Mng_Checklist != null && MoveSystem == null)
            MoveSystem = Mng_Checklist.GetComponent<SC_CheckList_Mecha>().GetMechCollider();

        if (Mng_Checklist != null && RenderSystem == null)
            RenderSystem = Mng_Checklist.GetComponent<SC_CheckList_ViewAiming>().GetScreens();

        if (Mng_Checklist != null && WeaponSystem == null)
            WeaponSystem = Mng_Checklist.GetComponent<SC_CheckList_Weapons>().GetMngWeapons();

        if (Mng_Checklist != null && MiniGunSystem == null)
            MiniGunSystem = Mng_Checklist.GetComponent<SC_CheckList_Weapons>().GetMiniGun();

        if (Mng_Checklist != null && ShrapnelSystem == null)
            ShrapnelSystem = Mng_Checklist.GetComponent<SC_CheckList_Weapons>().GetShrapnel();

        if (Mng_Checklist != null && FlameSystem == null)
            FlameSystem = Mng_Checklist.GetComponent<SC_CheckList_Weapons>().GetFlameThrower();
    }

    public void CheckBreakdown()
    {

        if(SC_BreakDownAlert == null || MoveSystem == null || RenderSystem == null || WeaponSystem == null)
            GetReferences();

        //Engine
        if (b_BreakEngine != Mng_BreakDownTest.b_BreakdownTest)
        {
            b_BreakEngine = Mng_BreakDownTest.b_BreakdownTest;
            if (b_BreakEngine)
                SC_BreakDownAlert.LaunchGlobalAlert();
            else if (!b_BreakEngine)
                SC_BreakDownAlert.StopGlobalAlert();

            if (MoveSystem != null && RenderSystem != null && WeaponSystem != null)
            {
                MoveSystem.GetComponent<IF_BreakdownSystem>().SetEngineBreakdownState(b_BreakEngine);
                RenderSystem.GetComponent<IF_BreakdownSystem>().SetEngineBreakdownState(b_BreakEngine);
                WeaponSystem.GetComponent<IF_BreakdownSystem>().SetEngineBreakdownState(b_BreakEngine);
            }
            
        }

        //Move


        //Screen


        //Weapons


        //MiniGun


        //Shrapnel


        //FlameThrower


    }

}
