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
    public SC_BreakdownDisplayManager DisplayBreakdownSC;
    GameObject RenderSystem;
    [SerializeField]
    public SC_WeaponBreakdown WeaponBreakdownSC;
    GameObject WeaponSystem;
    [SerializeField]
    public SC_MovementBreakdown MovementBreakdownSC;
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

        //get du script qui gere l'affichage des ecrans de panne
        if (screenController != null && sc_screens_controller == null)
            sc_screens_controller = screenController.GetComponent<SC_breakdown_displays_screens>();
        
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

        
        if(sc_screens_controller == null)
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
                {
                    SC_BreakdownOnBreakdownAlert.Instance.LaunchGlobalAlert();
                    SC_FogBreakDown.Instance.BreakDownDensity();
                }   

                //on fout tous les systemes en panne à balle
                sc_screens_controller.PanneAll();

                //descendre le bouton de validation
                if (SC_GameStates.Instance.CurState == SC_GameStates.GameState.Game)
                {
                    SC_main_breakdown_validation.Instance.isValidated = false;
                    SC_main_breakdown_validation.Instance.textStopBlink();
                    SC_main_breakdown_validation.Instance.bringDown();
                }


            }

        }

        else if (SC_BreakdownDisplayManager.Instance.CurNbOfBreakdown == 0 && SC_WeaponBreakdown.Instance.CurNbOfBreakdown == 0 && SC_MovementBreakdown.Instance.n_InteractibleInBreakDown == 0 && !SC_main_breakdown_validation.Instance.isValidated)
        {
            //Fait clignoter le Text du bouton
            SC_main_breakdown_validation.Instance.textBlink();
            //Désactive le timer
            SC_BreakdownOnBreakdownAlert.Instance.StopAllCoroutines();
            if(SC_GameStates.Instance.CurTutoState == SC_GameStates.TutorialState.Tutorial1_8)
                SC_GameStates.Instance.ChangeTutoGameState(SC_GameStates.TutorialState.Tutorial1_9);
            SC_FogBreakDown.Instance.ClearDensity();

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
        if(SC_GameStates.Instance.CurTutoState == SC_GameStates.TutorialState.Tutorial1_4 && SC_BreakdownDisplayManager.Instance.CurNbOfBreakdown == 0)
        {
            SC_GameStates.Instance.ChangeTutoGameState(SC_GameStates.TutorialState.Tutorial1_7);
        }
        else if (SC_GameStates.Instance.CurTutoState == SC_GameStates.TutorialState.Tutorial1_5 && SC_WeaponBreakdown.Instance.CurNbOfBreakdown == 0)
        {
            SC_GameStates.Instance.ChangeTutoGameState(SC_GameStates.TutorialState.Tutorial1_7);
        }
        else if (SC_GameStates.Instance.CurTutoState == SC_GameStates.TutorialState.Tutorial1_6 && SC_MovementBreakdown.Instance.n_InteractibleInBreakDown == 0)
        {
            SC_GameStates.Instance.ChangeTutoGameState(SC_GameStates.TutorialState.Tutorial1_7);
        }

        SyncSystemsLifes();

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

                    break;

            }

        }

        CheckBreakdown();

    }

    void SyncSystemsLifes()
    {
        SC_SyncVar_DisplaySystem.Instance.f_Displaylife = Ratio(Displaylife, 10, 1);
        SC_SyncVar_DisplaySystem.Instance.b_BreakEngine = b_BreakEngine;
        SC_SyncVar_MovementSystem.Instance.f_MovementLife = Ratio(MovementLife, 10, 1);
        SC_SyncVar_MovementSystem.Instance.b_BreakEngine = b_BreakEngine;
        SC_SyncVar_WeaponSystem.Instance.f_WeaponLife = Ratio(WeaponLife, 10, 1);
        SC_SyncVar_WeaponSystem.Instance.b_BreakEngine = b_BreakEngine;
    }

    float Ratio(float inputValue, float inputMax, float outputMax, float inputMin = 0.0f, float outputMin = 0.0f)
    {
        float product = (inputValue - inputMin) / (inputMax - inputMin);
        float output = ((outputMax - outputMin) * product) + outputMin;
        return output;
    }

}
