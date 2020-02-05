using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A Mettre sur ScreenRender | 
/// Met la Render Texture 2D sur le material | 
/// Script by Leni |
/// </summary>
public class SC_ScreenRender : MonoBehaviour
{

    public RenderTexture equirect;
    public Renderer renderer;

    // Use this for initialization
    void Start()
    {
        SetEquidirect();
    }

    void SetEquidirect()
    {
        renderer = this.GetComponent<Renderer>();
        renderer.material.SetTexture("_MainTex", equirect);
    }

}
