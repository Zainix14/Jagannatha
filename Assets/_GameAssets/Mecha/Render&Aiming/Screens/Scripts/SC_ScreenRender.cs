using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A Mettre sur ScreenRender | 
/// Met la Render Texture 2D sur le material | 
/// Script by Leni |
/// </summary>
public class SC_ScreenRender : MonoBehaviour, IF_BreakdownSystem
{

    bool b_InBreakdown = false;
    bool b_BreakEngine = false;

    bool b_IsBreak = false;
    bool b_IsActive = false;

    public Material BreakDownMat;
    public Material RenderMat;
    public RenderTexture equirect;
    public Renderer renderer;

    // Use this for initialization
    void Start()
    {
        SetEquidirect();
    }

    void Update()
    {
        if(!b_InBreakdown && !b_BreakEngine && !b_IsActive)
            SetEquidirect();
        if ((b_InBreakdown || b_BreakEngine) && !b_IsBreak)
            SetBreakdownState();
    }

    void SetEquidirect()
    {

        b_IsBreak = false;
        b_IsActive = true;
        renderer = this.GetComponent<Renderer>();
        renderer.material = RenderMat;
        renderer.material.SetTexture("_MainTex", equirect);     
    }

    void SetBreakdownState()
    {

        b_IsBreak = true;
        b_IsActive = false;
        renderer = this.GetComponent<Renderer>();
        renderer.material = BreakDownMat;
    }

    public void SetBreakdownState(bool State)
    {
        b_InBreakdown = State;
    }

    public void SetEngineBreakdownState(bool State)
    {
        b_BreakEngine = State;
    }

}
