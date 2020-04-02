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
    GameObject GeneralOffState;
    [SerializeField]
    GameObject ConnectedOffState;
    [SerializeField]
    GameObject InitializeOffState;
    [SerializeField]
    GameObject LaunchedOffState;


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

    public void CheckState()
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

        ApplyState();

    }

    void ApplyState()
    {

        switch (CurState)
        {

            case SystemState.Disconnected:
                GeneralOffState.SetActive(true);
                ConnectedOffState.SetActive(true);
                InitializeOffState.SetActive(true);
                LaunchedOffState.SetActive(true);
                break;

            case SystemState.Connected:
                GeneralOffState.SetActive(true);
                ConnectedOffState.SetActive(false);
                InitializeOffState.SetActive(false);
                LaunchedOffState.SetActive(false);
                break;

            case SystemState.Initialize:
                GeneralOffState.SetActive(true);
                ConnectedOffState.SetActive(true);
                InitializeOffState.SetActive(false);
                LaunchedOffState.SetActive(false);
                break;

            case SystemState.Launched:
                GeneralOffState.SetActive(false);
                ConnectedOffState.SetActive(false);
                InitializeOffState.SetActive(false);
                LaunchedOffState.SetActive(false);
                break;

        }

    }

}
