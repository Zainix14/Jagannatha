using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_Movement_MechState : MonoBehaviour
{
    #region Singleton

    private static SC_Movement_MechState _instance;
    public static SC_Movement_MechState Instance { get { return _instance; } }

    #endregion

    [SerializeField]
    SC_UI_SystmShield _SystmShield;

    [SerializeField]
    SC_Movement_Direction dirRight;
    [SerializeField]
    SC_Movement_Direction dirLeft;

    [SerializeField]
    Image[] arcR;
    [SerializeField]
    Image[] arcL;
    [SerializeField]
    Color32 validColor;
    [SerializeField]
    Color32 breakdownColor;


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

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateValue();
        updateDirection();
        updateBrokenDirection();
    }

    void UpdateValue()
    {
        _SystmShield.simpleValue = SC_SyncVar_MovementSystem.Instance.f_MovementLife;
        
    }

    void CheckState()
    {

        if(SC_SyncVar_MovementSystem.Instance.CurState == SC_GameStates.GameState.Lobby)
        {
            CurState = SystemState.Disconnected;
        }

        else
        {

            CurState = SystemState.Connected;

            if (SC_SyncVar_MovementSystem.Instance.CurState != SC_GameStates.GameState.Tutorial)
            {
                CurState = SystemState.Initialize;

                if (SC_SyncVar_MovementSystem.Instance.CurState == SC_GameStates.GameState.Game && !SC_SyncVar_MovementSystem.Instance.b_BreakEngine)
                    CurState = SystemState.Launched;

            }   

        }  

    }
    
    void updateDirection()
    {
        if (SC_SyncVar_MovementSystem.Instance.CurDir != SC_JoystickMove.Dir.None)
        {
            if (SC_SyncVar_MovementSystem.Instance.CurDir == SC_JoystickMove.Dir.Right)
            {
                dirRight.speedRotateBase = dirRight.speedRotateUsed;
                dirLeft.speedRotateBase = dirLeft.speedRotateInit;
            }
            else if (SC_SyncVar_MovementSystem.Instance.CurDir == SC_JoystickMove.Dir.Left)
            {
                dirLeft.speedRotateBase = dirLeft.speedRotateUsed;
                dirRight.speedRotateBase = dirRight.speedRotateInit;
            }
        }
        else
        {
            dirRight.speedRotateBase = dirRight.speedRotateInit;
            dirLeft.speedRotateBase = dirLeft.speedRotateInit;
        }
    }

    void updateBrokenDirection()
    {
        if(SC_SyncVar_MovementSystem.Instance.CurBrokenDir == SC_JoystickMove.Dir.Left)
        {
            int nbBreakdown = SC_SyncVar_MovementSystem.Instance.n_BreakDownLvl;
            if(nbBreakdown !=0)
            {
                for (int i = 0; i < nbBreakdown; i++)
                {
                    arcL[i].color = breakdownColor;
                }
            }
            else
            {
                for (int i = 0; i < arcL.Length; i++)
                {
                    arcL[i].color = validColor;
                }
            }
            
        }
        else if (SC_SyncVar_MovementSystem.Instance.CurBrokenDir == SC_JoystickMove.Dir.Right)
        {
            int nbBreakdown = SC_SyncVar_MovementSystem.Instance.n_BreakDownLvl;
            if (nbBreakdown != 0)
            {
                Debug.Log("Panne");
                for (int i = 0; i < nbBreakdown; i++)
                {
                    arcR[i].color = breakdownColor;
                }
            }
            else
            {
                
                for (int i = 0; i < arcL.Length; i++)
                {
                    arcR[i].color = validColor;
                }
            }

        }

    }
}
