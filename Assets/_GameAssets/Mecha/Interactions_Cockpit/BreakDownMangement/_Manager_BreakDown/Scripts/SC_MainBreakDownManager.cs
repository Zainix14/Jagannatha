using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_MainBreakDownManager : MonoBehaviour, IF_BreakdownManager
{

    #region Singleton

    private static SC_MainBreakDownManager _instance;
    public static SC_MainBreakDownManager Instance { get { return _instance; } }

    #endregion

    #region Variables

    [Header("References")]
    public GameObject Mng_Checklist;
    public GameObject Mng_BreakDownAlert;
    public GameObject screenController;
    private SC_breakdown_displays_screens sc_screens_controller;

    [Header("References breakDown SC")]
    [SerializeField]
    SC_BreakdownDisplayManager DisplayBreakdownSC;
    GameObject RenderSystem;
    [SerializeField]
    SC_WeaponBreakdown WeaponBreakdownSC;
    GameObject WeaponSystem;
    [SerializeField]
    SC_MovementBreakdown MovementBreakdownSC;
    GameObject MoveSystem;

    [Header("System Lifes")]
    public int nbOfBreakDownBeforeTotalBreak;
    [SerializeField, Range(0,10)]
    int Displaylife = 10;
    [SerializeField, Range(0, 10)]
    int WeaponLife = 10;
    [SerializeField, Range(0, 10)]
    int MovementLife = 10;

    [Header("Systems States")]
    public bool b_BreakEngine = false;
    /*
    public bool b_BreakMove = false;
    public bool b_BreakScreen = false;
    public bool b_BreakWeapons = false;
    */

    /*
    [Header("Old Var (Normally no Used)")]
    public bool b_BreakMiniGun = false;
    GameObject MiniGunSystem;
    public bool b_BreakShrapnel = false;
    GameObject ShrapnelSystem;
    public bool b_BreakFlameThrower = false;
    GameObject FlameSystem;
    */

    #endregion Variables

    #region Init

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

    void GetReferences()
    {

        if (Mng_Checklist == null)
            Mng_Checklist = GameObject.FindGameObjectWithTag("Mng_CheckList");

        if (Mng_Checklist != null && MoveSystem == null)
            MoveSystem = Mng_Checklist.GetComponent<SC_CheckList_Mecha>().GetMechCollider();

        if (Mng_Checklist != null && RenderSystem == null)
            RenderSystem = Mng_Checklist.GetComponent<SC_CheckList_ViewAiming>().GetScreens();

        //get du script qui gere l'affichage des ecrans de panne
        if (screenController != null && sc_screens_controller == null)
            sc_screens_controller = screenController.GetComponent<SC_breakdown_displays_screens>();

        if (Mng_Checklist != null && WeaponSystem == null)
            WeaponSystem = Mng_Checklist.GetComponent<SC_CheckList_Weapons>().GetMngWeapons();
        /*
        if (Mng_Checklist != null && MiniGunSystem == null)
            MiniGunSystem = Mng_Checklist.GetComponent<SC_CheckList_Weapons>().GetMiniGun();

        if (Mng_Checklist != null && ShrapnelSystem == null)
            ShrapnelSystem = Mng_Checklist.GetComponent<SC_CheckList_Weapons>().GetShrapnel();

        if (Mng_Checklist != null && FlameSystem == null)
            FlameSystem = Mng_Checklist.GetComponent<SC_CheckList_Weapons>().GetFlameThrower();
        */

    }

    #endregion Init

    void Update()
    {
        DebugInput();
    }

    void DebugInput()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            SC_BreakdownDisplayManager.Instance.CheckBreakdown();
            Debug.Log("display : " +SC_BreakdownDisplayManager.Instance.CurNbOfBreakdown);
            SC_WeaponBreakdown.Instance.CheckBreakdown();
            Debug.Log("weapon : " +SC_WeaponBreakdown.Instance.CurNbOfBreakdown);
            SC_MovementBreakdown.Instance.CheckBreakdown();
            Debug.Log("movement" +SC_MovementBreakdown.Instance.n_InteractibleInBreakDown);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            DisplayBreakdownSC.RepairBreakdownDebug();
            WeaponBreakdownSC.RepairBreakdownDebug();
            MovementBreakdownSC.RepairBreakdownDebug();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            CauseDamageOnSystem(FlockSettings.AttackFocus.Display, 1);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            CauseDamageOnSystem(FlockSettings.AttackFocus.Movement, 1);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            CauseDamageOnSystem(FlockSettings.AttackFocus.Weapon, 1);
        }

    }

    public void CheckBreakdown()
    {

        if( MoveSystem == null || RenderSystem == null || WeaponSystem == null)
            GetReferences();

        #region Verif si chaque system est en panne

        if (SC_BreakdownDisplayManager.Instance.CurNbOfBreakdown > 0)
        {
            SC_SyncVar_Main_Breakdown.Instance.onPanneDisplayChange(true);
        }
        else
        {
            SC_SyncVar_Main_Breakdown.Instance.onPanneDisplayChange(false);
        }

        if (SC_WeaponBreakdown.Instance.CurNbOfBreakdown > 0)
        {
            SC_SyncVar_Main_Breakdown.Instance.onPanneWeaponChange(true);
        }
        else
        {
            SC_SyncVar_Main_Breakdown.Instance.onPanneWeaponChange(false);
        }
            

        if (SC_MovementBreakdown.Instance.n_InteractibleInBreakDown > 0)
        {
            SC_SyncVar_Main_Breakdown.Instance.onPanneMovementChange(true);
        }
        else
        {
            SC_SyncVar_Main_Breakdown.Instance.onPanneMovementChange(false);
        }

        #endregion

        //Ici on additionne toutes les pannes des sytemes pour savoir si on déclanche une panne complete
        if (SC_BreakdownDisplayManager.Instance.CurNbOfBreakdown + SC_WeaponBreakdown.Instance.CurNbOfBreakdown + SC_MovementBreakdown.Instance.n_InteractibleInBreakDown >= nbOfBreakDownBeforeTotalBreak)
        {

            //Si on est pas encore en panne totale
            if (!b_BreakEngine)
            {

                ///////important doit etre avant le changement de booleen puisque checké dans le lancement de panne locale

                if (SC_WeaponBreakdown.Instance.CurNbOfBreakdown == 0)
                    SC_WeaponBreakdown.Instance.StartNewBreakdown(1);

                if (SC_MovementBreakdown.Instance.n_BreakDownLvl == 0)
                    SC_MovementBreakdown.Instance.StartNewBreakdown(1);

                //cequi n'arrivera sans doute jamais kek
                if (SC_BreakdownDisplayManager.Instance.CurNbOfBreakdown == 0)
                    SC_BreakdownDisplayManager.Instance.StartNewBreakdown(1);

                ///////

                b_BreakEngine = true;
                SC_WaveManager.Instance.nextWave = false;

                if (SC_GameStates.Instance.CurState == SC_GameStates.GameState.Game)
                    SC_BreakdownOnBreakdownAlert.Instance.LaunchGlobalAlert();

                //on fout tous les systemes en panne à balle
                sc_screens_controller.PanneAll();

                //descendre le bouton de validation
                SC_main_breakdown_validation.Instance.isValidated = false;
                SC_main_breakdown_validation.Instance.textStopBlink();
                SC_main_breakdown_validation.Instance.bringDown();

            }

        }

        else if (SC_BreakdownDisplayManager.Instance.CurNbOfBreakdown == 0 && SC_WeaponBreakdown.Instance.CurNbOfBreakdown == 0 && SC_MovementBreakdown.Instance.n_InteractibleInBreakDown == 0 && !SC_main_breakdown_validation.Instance.isValidated)
        {
            //Fait clignoter le Text du bouton
            SC_main_breakdown_validation.Instance.textBlink();
            //Désactive le timer
            SC_BreakdownOnBreakdownAlert.Instance.StopAllCoroutines();
        }

        //on additionne tout et on regarde si ya plus de panne et que le bouton de validation a été set par le joueur
        else if (SC_BreakdownDisplayManager.Instance.CurNbOfBreakdown == 0 && SC_WeaponBreakdown.Instance.CurNbOfBreakdown == 0 && SC_MovementBreakdown.Instance.n_InteractibleInBreakDown  == 0 && SC_main_breakdown_validation.Instance.isValidated)
        {

            if (SC_GameStates.Instance.CurState == SC_GameStates.GameState.Game)
            {
                SC_BreakdownOnBreakdownAlert.Instance.StopGlobalAlert();
                //protection pour les kamikazes en fin de wave
                if (SC_WaveManager.Instance.nextWave == false)
                {
                    SC_WaveManager.Instance.nextWave = true;
                    if (SC_WaveManager.Instance.waveEnded == true)
                        SC_PhaseManager.Instance.EndWave();
                }
            }
            //on répare touuuus les systemes
            sc_screens_controller.RepairAll();
            SC_WeaponBreakdown.Instance.EndBreakdown();
            SC_MovementBreakdown.Instance.EndBreakdown();

            //remonter le bouton de validation
            SC_main_breakdown_validation.Instance.bringUp();

            b_BreakEngine = false;

            //changement de state du tuto
            if (SC_GameStates.Instance.CurState == SC_GameStates.GameState.Tutorial)
                SC_GameStates.Instance.ChangeGameState(SC_GameStates.GameState.Tutorial2);

        }


        #region OldVersion

        /*
        Ancien code qui donne envie de se foutre une balle

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
                    SC_GameStates.Instance.RpcSetState(SC_GameStates.GameState.Tutorial2);

            }

            if (MoveSystem != null && RenderSystem != null && WeaponSystem != null)
            {
                    MoveSystem.GetComponent<IF_BreakdownSystem>().SetEngineBreakdownState(b_BreakEngine);
                    RenderSystem.GetComponent<IF_BreakdownSystem>().SetEngineBreakdownState(b_BreakEngine);
                    WeaponSystem.GetComponent<IF_BreakdownSystem>().SetEngineBreakdownState(b_BreakEngine);
            }

        }
        */

        #endregion

    }

    public void CauseDamageOnSystem(FlockSettings.AttackFocus attackFocus, int DmgValue)
    {

        SC_BreakdownDisplayManager.Instance.CheckBreakdown();
        SC_WeaponBreakdown.Instance.CheckBreakdown();
        SC_MovementBreakdown.Instance.CheckBreakdown();

        
        if ((SC_BreakdownDisplayManager.Instance.CurNbOfBreakdown + SC_WeaponBreakdown.Instance.CurNbOfBreakdown + SC_MovementBreakdown.Instance.n_InteractibleInBreakDown) < nbOfBreakDownBeforeTotalBreak && !b_BreakEngine)
        {
            switch (attackFocus)
            {

                ////////////////////////////////////////////////////////////////////////////////////////////DISPLAY
                case FlockSettings.AttackFocus.Display:

                    if (!SC_BreakdownDisplayManager.Instance.b_MaxBreakdown)
                        Displaylife -= DmgValue;
                    else
                    {

                        int rnd;
                        rnd = Random.Range(0, 2);

                        if (rnd == 0)
                            CauseDamageOnSystem(FlockSettings.AttackFocus.Movement, DmgValue);

                        else
                            CauseDamageOnSystem(FlockSettings.AttackFocus.Weapon, DmgValue);

                    }

                    if (Displaylife <= 0)
                    {

                        SC_BreakdownDisplayManager.Instance.StartNewBreakdown(2);

                        Displaylife = 10;

                    }

                    SyncSystemsLifes();

                    break;

                ////////////////////////////////////////////////////////////////////////////////////////////MOVEMENT
                case FlockSettings.AttackFocus.Movement:

                    if (!SC_MovementBreakdown.Instance.b_MaxBreakdown)
                        MovementLife -= DmgValue;
                    else
                        CauseDamageOnSystem(FlockSettings.AttackFocus.Display, DmgValue);

                    if (MovementLife <= 0)
                    {

                        SC_MovementBreakdown.Instance.StartNewBreakdown(1);

                        MovementLife = 10;

                    }

                    SyncSystemsLifes();

                    break;

                ////////////////////////////////////////////////////////////////////////////////////////////WEAPON
                case FlockSettings.AttackFocus.Weapon:

                    if (!SC_WeaponBreakdown.Instance.b_MaxBreakdown)
                        WeaponLife -= DmgValue;
                    else
                        CauseDamageOnSystem(FlockSettings.AttackFocus.Display, DmgValue);


                    if (WeaponLife <= 0)
                    {

                        SC_WeaponBreakdown.Instance.StartNewBreakdown(1);

                        WeaponLife = 10;

                    }

                    SyncSystemsLifes();


                    break;

            }
        }

        CheckBreakdown();

    }

    void SyncSystemsLifes()
    {
        SC_SyncVar_DisplaySystem.Instance.n_Displaylife = Displaylife;
        SC_SyncVar_MovementSystem.Instance.n_MovementLife = MovementLife;
        SC_SyncVar_WeaponSystem.Instance.n_WeaponLife = WeaponLife;
    }

}
