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

    [ClientRpc]
    public void RpcSetState(GameState TargetState)
    {

        CurState = TargetState;  
        
        switch (TargetState)
        {
            case GameState.Lobby:

                break;

            case GameState.Tutorial:
                if (!isServer)
                    SC_instruct_op_manager.Instance.Activate(1);
                break;

            case GameState.Tutorial2:
                if (!isServer)
                    SC_instruct_op_manager.Instance.Deactivate(1);
                    SC_instruct_op_manager.Instance.Activate(0);

                break;

            case GameState.Game:
                if (!isServer)
                    SC_instruct_op_manager.Instance.Deactivate(0);
                break;

            case GameState.GameEnd:
                if (!isServer)
                    SC_EndGameOP.Instance.EndGameDisplay();
                break;

        }

    }

}
