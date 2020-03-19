using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_MainBreakDownManager : MonoBehaviour, IF_BreakdownManager
{
    #region Singleton

    private static SC_MainBreakDownManager _instance;
    public static SC_MainBreakDownManager Instance { get { return _instance; } }

    #endregion


    public GameObject Mng_Checklist;
    public GameObject Mng_BreakDownAlert;

    // ecrans d'erreur

    public GameObject screenController;
    private SC_breakdown_displays_screens sc_screens_controller;

    public bool b_BreakEngine = false;

    GameObject MoveSystem;
    public bool b_BreakMove = false;

    GameObject RenderSystem;
    public bool b_BreakScreen = false;

    GameObject WeaponSystem;
    public bool b_BreakWeapons = false;


    /// //////////////////////////////////////////////


    GameObject MiniGunSystem;
    public bool b_BreakMiniGun = false;
    GameObject ShrapnelSystem;
    public bool b_BreakShrapnel = false;
    GameObject FlameSystem;
    public bool b_BreakFlameThrower = false;


    /// //////////////////////////////////////////////
    /// 
    public int nbOfBreakDownBeforeTotalBreak = 7;


    public int Displaylife = 10;
    public int WeaponLife = 10;
    public int MovementLife = 10;
    // Start is called before the first frame update

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
    }
    void Start()
    {       
        GetReferences();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {

            SC_BreakdownDisplayManager.Instance.CheckBreakdown();
            Debug.Log(SC_BreakdownDisplayManager.Instance.CurNbOfBreakdown);


        }
    }

    void GetReferences()
    {
        if (Mng_Checklist == null)
            Mng_Checklist = GameObject.FindGameObjectWithTag("Mng_CheckList");


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


        //get du script qui gere l'affichage des ecrans de panne
        sc_screens_controller = screenController.GetComponent<SC_breakdown_displays_screens>();
    }



    public void CheckBreakdown()
    {

        if( MoveSystem == null || RenderSystem == null || WeaponSystem == null)
            GetReferences();


        //Ici on additionne toutes les pannes des sytemes pour savoir si on déclanche une panne complete
        if (SC_BreakdownDisplayManager.Instance.CurNbOfBreakdown + SC_WeaponBreakdown.Instance.CurNbOfBreakdown + SC_MovementBreakdown.Instance.CurNbOfBreakdown > nbOfBreakDownBeforeTotalBreak)
        {


            //Si on est pas encore en panne totale
            if (!b_BreakEngine)
            {
                b_BreakEngine = true;


                if (SC_GameStates.Instance.CurState == SC_GameStates.GameState.Game)
                    SC_BreakdownOnBreakdownAlert.Instance.LaunchGlobalAlert();

                //on fout tous les systemes en panne à balle
                sc_screens_controller.PanneAll();


                //descendre le bouton de validation
                SC_main_breakdown_validation.Instance.isValidated = false;
                SC_main_breakdown_validation.Instance.bringDown();

            }

        }
        else if (SC_BreakdownDisplayManager.Instance.CurNbOfBreakdown == 0 && SC_WeaponBreakdown.Instance.CurNbOfBreakdown == 0 && SC_MovementBreakdown.Instance.CurNbOfBreakdown !=0 &&  !SC_main_breakdown_validation.Instance.isValidated)
        {
            //Fait clignoter le Text du bouton
            SC_main_breakdown_validation.Instance.textBlink();
        }
        //on additionne tout et on regarde si ya plus de panne et que le bouton de validation a été set par le joueur
        else if (SC_BreakdownDisplayManager.Instance.CurNbOfBreakdown == 0 && SC_WeaponBreakdown.Instance.CurNbOfBreakdown == 0 && SC_MovementBreakdown.Instance.CurNbOfBreakdown  !=0 && SC_main_breakdown_validation.Instance.isValidated)
        {


            if (SC_GameStates.Instance.CurState == SC_GameStates.GameState.Game)
                SC_BreakdownOnBreakdownAlert.Instance.StopGlobalAlert();

            //on répare touuuus les systemes
            sc_screens_controller.RepairAll();
            SC_WeaponBreakdown.Instance.EndBreakdown();

            //Stop de faire clignoter le Text du bouton


            //remonter le bouton de validation
            SC_main_breakdown_validation.Instance.bringUp();

            b_BreakEngine = false;

            //changement de state du tuto
            if (SC_GameStates.Instance.CurState == SC_GameStates.GameState.Tutorial)
            {
                SC_GameStates.Instance.RpcSetState(SC_GameStates.GameState.Tutorial2);
            }

        }




        /*
         * 
         * Ancien code qui donne envie de se foutre une balle


        if (b_BreakEngine != Mng_BreakDownTest.b_BreakdownTest)
        {


           b_BreakEngine = Mng_BreakDownTest.b_BreakdownTest;

            if (Mng_BreakDownTest.b_BreakdownTest || SC_main_breakdown_validation.Instance.isValidated == false)
                b_BreakEngine = true;

            if (b_BreakEngine)
            {
                if (SC_GameStates.Instance.CurState == SC_GameStates.GameState.Game)
                    SC_BreakDownAlert.LaunchGlobalAlert();
                sc_screens_controller.PanneAll();

                //descendre le bouton de validation
                SC_main_breakdown_validation.Instance.isValidated = false;
                SC_main_breakdown_validation.Instance.bringDown();

            }


            else if (!Mng_BreakDownTest.b_BreakdownTest && SC_main_breakdown_validation.Instance.isValidated)
            {
                if (SC_GameStates.Instance.CurState == SC_GameStates.GameState.Game)
                    SC_BreakDownAlert.StopGlobalAlert();
                sc_screens_controller.RepairAll();
                //remonter le bouton de validation
                SC_main_breakdown_validation.Instance.bringUp(); 


                //changement de state du tuto
                if (SC_GameStates.Instance.CurState == SC_GameStates.GameState.Tutorial)
                {
                    SC_GameStates.Instance.RpcSetState(SC_GameStates.GameState.Tutorial2);
                }
            }

            if (MoveSystem != null && RenderSystem != null && WeaponSystem != null)
            {
                    MoveSystem.GetComponent<IF_BreakdownSystem>().SetEngineBreakdownState(b_BreakEngine);
                    RenderSystem.GetComponent<IF_BreakdownSystem>().SetEngineBreakdownState(b_BreakEngine);
                    WeaponSystem.GetComponent<IF_BreakdownSystem>().SetEngineBreakdownState(b_BreakEngine);
            }


        }

    */


    }



    public void causeDamageOnSystem(FlockSettings.AttackFocus attackFocus, int DmgValue)
    {

        if(SC_BreakdownDisplayManager.Instance.CurNbOfBreakdown + SC_WeaponBreakdown.Instance.CurNbOfBreakdown + SC_MovementBreakdown.Instance.CurNbOfBreakdown < nbOfBreakDownBeforeTotalBreak)
        switch (attackFocus)
        {

            case FlockSettings.AttackFocus.Display:

                Displaylife -= DmgValue;

                if (Displaylife <= 0)
                {
                    int cascade = Mathf.Abs(Displaylife);
                    Displaylife = 10;
                    SC_BreakdownDisplayManager.Instance.StartNewBreakdown(2);

                    if (SC_BreakdownDisplayManager.Instance.b_MaxBreakdown && cascade != 0)
                    {
                        int rnd;
                        rnd = Random.Range(0, 2);
                        if(rnd == 0)
                        causeDamageOnSystem(FlockSettings.AttackFocus.Movement, cascade);
                        
                        else
                        causeDamageOnSystem(FlockSettings.AttackFocus.Weapon, cascade);
                    }
                }

                break;

            case FlockSettings.AttackFocus.Movement:
                    
                MovementLife -= DmgValue;

                if (MovementLife <= 0)
                {
                    int cascade = Mathf.Abs(MovementLife);
                    MovementLife = 10;
                    SC_MovementBreakdown.Instance.StartNewBreakdown(1);
                    
                    if (SC_MovementBreakdown.Instance.b_MaxBreakdown && cascade != 0)
                    {
                        causeDamageOnSystem(FlockSettings.AttackFocus.Display, cascade);
                    }
                }


                break;

            case FlockSettings.AttackFocus.Weapon:

                WeaponLife -= DmgValue;

                if (WeaponLife <= 0)
                {
                    int cascade = Mathf.Abs(WeaponLife);
                    WeaponLife = 10;
                    SC_WeaponBreakdown.Instance.StartNewBreakdown(1);

                    if(SC_WeaponBreakdown.Instance.b_MaxBreakdown && cascade != 0)
                    {
                        causeDamageOnSystem(FlockSettings.AttackFocus.Display, cascade);
                    }

                }

                break;

        }


  
    }

}
