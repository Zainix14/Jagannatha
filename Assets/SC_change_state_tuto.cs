using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_change_state_tuto : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("callRPC_changeState", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void callRPC_changeState()
    {
        SC_GameStates.Instance.RpcSetState(SC_GameStates.GameState.Tutorial);

    }
}
