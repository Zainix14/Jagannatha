using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SC_Net_Player_TutoState : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    [Command]
    public void CmdChangeTutoState(SC_GameStates.TutorialState TargetState)
    {
        SC_GameStates.Instance.ChangeTutoGameState(TargetState);
    }

    public IEnumerator Delay (float time, SC_GameStates.TutorialState Target)
    {
        yield return new WaitForSeconds(time);
        CmdChangeTutoState(Target);
        StopAllCoroutines();
    }
}
