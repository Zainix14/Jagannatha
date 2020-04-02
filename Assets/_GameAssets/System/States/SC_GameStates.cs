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

    public enum GameState {Lobby, Tutorial,Tutorial2, Game, GameEnd }
    public GameState CurState;

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
                if(isServer)
                {
                    SC_main_breakdown_validation.Instance.isValidated = false;
                    SC_main_breakdown_validation.Instance.textStopBlink();
                    SC_main_breakdown_validation.Instance.bringDown();
                }
                break;

            case GameState.Tutorial2:

                if (!isServer)
                {

                    SC_instruct_op_manager.Instance.Deactivate(1);
                    SC_instruct_op_manager.Instance.Activate(0);
                    SC_instruct_op_manager.Instance.Deactivate(6);

                }

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

    void SyncSystemState(GameState TargetState)
    {
        SC_SyncVar_DisplaySystem.Instance.CurState = TargetState;
        SC_SyncVar_MovementSystem.Instance.CurState = TargetState;
        SC_SyncVar_WeaponSystem.Instance.CurState = TargetState;
    }

}
