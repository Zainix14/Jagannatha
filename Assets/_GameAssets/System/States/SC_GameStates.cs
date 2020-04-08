using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SC_GameStates : NetworkBehaviour
{

    #region Singleton

    private static SC_GameStates _instance;
    public static SC_GameStates Instance { get { return _instance; } }

    #endregion

    public enum GameState {Lobby, Tutorial, Tutorial2, Game, GameEnd }
    public enum TutorialState { Tutorial1_1, Tutorial1_2, Tutorial1_3, Tutorial1_4, Tutorial1_5, Tutorial1_6, Tutorial1_7, Tutorial1_8, Tutorial1_9, Tutorial1_10, Tutorial1_11, Tutorial2_1, Tutorial2_2, Tutorial2_3, Tutorial2_4, TutorialEnd}
    public GameState CurState;
    public TutorialState CurTutoState;

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

    // Start is called before the first frame update
    void Start()
    {
        if(isServer)
            RpcSetState(GameState.Lobby);
    }

    public void ChangeGameState (GameState TargetState)
    {
        if (isServer)
        {
            RpcSetState(TargetState);
            SyncSystemState(TargetState);
        }

    }
    public void ChangeTutoGameState (TutorialState TargetTutoState)
    {
        if (isServer)
        {
            RpcSetTutoState(TargetTutoState);
            SyncSystemTutoState(TargetTutoState);
        }

    }

    [ClientRpc]
    public void RpcSetState(GameState TargetState)
    {

        CurState = TargetState;  
        
        switch (TargetState)
        {
            case GameState.Lobby:

                break;

            case GameState.Tutorial:
                //Descendre le Bouton Reboot au tuto
                ChangeTutoGameState(TutorialState.Tutorial1_1);

                break;

            case GameState.Tutorial2:

                ChangeTutoGameState(TutorialState.Tutorial2_1);

                break;     

            case GameState.Game:

                if (!isServer)
                {
                    SC_instruct_op_manager.Instance.Deactivate(2);
                    SC_instruct_op_manager.Instance.Deactivate(3);
                }
                    
                break;

            case GameState.GameEnd:

                if (!isServer)
                    SC_EndGameOP.Instance.EndGameDisplay();

                if(isServer)
                    SC_breakdown_displays_screens.Instance.EndScreenDisplay();

                break;

        }

    }

    [ClientRpc]
    public void RpcSetTutoState(TutorialState TargetTutoState)
    {

        CurTutoState = TargetTutoState;  
        
        switch (TargetTutoState)
        {
            case TutorialState.Tutorial1_1:
                if(!isServer)
                {
                    SC_instruct_op_manager.Instance.Activate(6);
                    SC_instruct_op_manager.Instance.Deactivate(0);
                    SC_instruct_op_manager.Instance.Deactivate(2);
                }
                 

                break;

            case TutorialState.Tutorial1_2:
                if (isServer)
                {
                    SC_main_breakdown_validation.Instance.isValidated = false;
                    SC_main_breakdown_validation.Instance.textStopBlink();
                    SC_main_breakdown_validation.Instance.bringDown();
                }
                
                StartCoroutine(Swichtuto(0.4f, TutorialState.Tutorial1_3));

                break;

            case TutorialState.Tutorial1_3:
                if(!isServer)
                {
                    SC_instruct_op_manager.Instance.Activate(7);
                    SC_instruct_op_manager.Instance.Activate(8);
                }


                break;     

            case TutorialState.Tutorial1_4:
                if (!isServer)
                {
                    SC_instruct_op_manager.Instance.Deactivate(7);
                    SC_instruct_op_manager.Instance.Deactivate(8);
                    SC_instruct_op_manager.Instance.Activate(9);
                }
                break;

            case TutorialState.Tutorial1_5:

                if (!isServer)
                {
                    SC_instruct_op_manager.Instance.Deactivate(7);
                    SC_instruct_op_manager.Instance.Deactivate(8);
                    SC_instruct_op_manager.Instance.Activate(10);
                }
                break;

            case TutorialState.Tutorial1_6:
                if (!isServer)
                {
                    SC_instruct_op_manager.Instance.Deactivate(7);
                    SC_instruct_op_manager.Instance.Deactivate(8);
                    SC_instruct_op_manager.Instance.Activate(11);
                }

                break;

            case TutorialState.Tutorial1_7:
                if (!isServer)
                {
                    SC_instruct_op_manager.Instance.Deactivate(9);
                    SC_instruct_op_manager.Instance.Deactivate(10);
                    SC_instruct_op_manager.Instance.Deactivate(11);
                    SC_instruct_op_manager.Instance.Activate(12);
                }

                    break;

            case TutorialState.Tutorial1_8:

                if(!isServer)
                {
                    SC_instruct_op_manager.Instance.Deactivate(12);  
                }

                break;

            case TutorialState.Tutorial1_9:

                if (!isServer)
                {
                    SC_instruct_op_manager.Instance.Activate(13);
                    
                }
                StartCoroutine(Swichtuto(0.8f, TutorialState.Tutorial1_10));


                break;

                 case TutorialState.Tutorial1_10:

                if (!isServer)
                {
                    SC_instruct_op_manager.Instance.Activate(14);
                }

                break;


            case TutorialState.Tutorial2_1:

                if (!isServer)
                {

                    SC_instruct_op_manager.Instance.Deactivate(1);
                    SC_instruct_op_manager.Instance.Deactivate(13);
                    SC_instruct_op_manager.Instance.Deactivate(14);
                    SC_instruct_op_manager.Instance.Activate(0);
                    SC_instruct_op_manager.Instance.Deactivate(6);

                }

                break;

            case TutorialState.Tutorial2_2:


                break;

            case TutorialState.Tutorial2_3:


                break;

            case TutorialState.Tutorial2_4:
                if (!isServer)
                {
                    SC_instruct_op_manager.Instance.Deactivate(0);
                    SC_instruct_op_manager.Instance.Deactivate(1);
                    SC_instruct_op_manager.Instance.Deactivate(2);
                    SC_instruct_op_manager.Instance.Deactivate(3);
                    SC_instruct_op_manager.Instance.Deactivate(4);
                    SC_instruct_op_manager.Instance.Deactivate(5);
                    SC_instruct_op_manager.Instance.Deactivate(6);
                    SC_instruct_op_manager.Instance.Deactivate(7);
                    SC_instruct_op_manager.Instance.Deactivate(8);
                    SC_instruct_op_manager.Instance.Deactivate(9);
                    SC_instruct_op_manager.Instance.Deactivate(10);
                    SC_instruct_op_manager.Instance.Deactivate(11);
                    SC_instruct_op_manager.Instance.Deactivate(12);
                    SC_instruct_op_manager.Instance.Deactivate(13);
                    SC_instruct_op_manager.Instance.Deactivate(14);
                }
                if (isServer)
                {
                    SC_main_breakdown_validation.Instance.isValidated = false;
                    SC_main_breakdown_validation.Instance.textStopBlink();
                    SC_main_breakdown_validation.Instance.bringDown();
                }
                StartCoroutine(Swichtuto(1f, TutorialState.TutorialEnd));
                break;

            case TutorialState.TutorialEnd:

                ChangeGameState(GameState.Game);

                break;

        }

    }
   

    void SyncSystemState(GameState TargetState)
    {
        SC_SyncVar_DisplaySystem.Instance.CurState = TargetState;
        SC_SyncVar_MovementSystem.Instance.CurState = TargetState;
        SC_SyncVar_WeaponSystem.Instance.CurState = TargetState;
    }
    void SyncSystemTutoState(TutorialState TargetTutoState)
    {

    }
    IEnumerator Swichtuto(float duration, TutorialState TargetState)
    {
        yield return new WaitForSeconds(duration);
        ChangeTutoGameState(TargetState);
        StopAllCoroutines();
    }
}
