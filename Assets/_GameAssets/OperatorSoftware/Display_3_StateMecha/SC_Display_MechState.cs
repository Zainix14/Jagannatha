using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Display_MechState : MonoBehaviour
{
    #region Singleton

    private static SC_Display_MechState _instance;
    public static SC_Display_MechState Instance { get { return _instance; } }

    #endregion

    [SerializeField]
    SC_UI_SystmShield _SystmShield;
    [SerializeField]
    GameObject OffState;


    public enum SystemState { Disconnected, Connected, Initialize, Launched }
    public SystemState CurState;

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

    }

    // Update is called once per frame
    void Update()
    {
        updateValue();
    }

    void updateValue()
    {
        _SystmShield.simpleValue = SC_SyncVar_DisplaySystem.Instance.f_Displaylife;
    }

    void CheckState()
    {

        if (SC_SyncVar_DisplaySystem.Instance.CurState == SC_GameStates.GameState.Lobby)
        {
            CurState = SystemState.Disconnected;
        }

        else
        {

            CurState = SystemState.Connected;

            if (SC_SyncVar_DisplaySystem.Instance.CurState != SC_GameStates.GameState.Tutorial)
            {
                CurState = SystemState.Initialize;

                if (SC_SyncVar_DisplaySystem.Instance.CurState == SC_GameStates.GameState.Game && !SC_SyncVar_DisplaySystem.Instance.b_BreakEngine)
                    CurState = SystemState.Launched;

            }

        }

    }

    void ApplyState()
    {

    }

}
