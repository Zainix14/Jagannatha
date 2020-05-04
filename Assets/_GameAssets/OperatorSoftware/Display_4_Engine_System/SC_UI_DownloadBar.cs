using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_UI_DownloadBar : MonoBehaviour
{
    Slider progress;
    void Start()
    {
        progress = this.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SC_GameStates.Instance.CurState == SC_GameStates.GameState.Game)
            progress.value = SC_SyncVar_DisplaySystem.Instance.progress;
    }
}
