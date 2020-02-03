using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Mouvement sinosïdale haut-bas du cockpit (effet de marche)
/// GameObject scene : CockpitP
/// </summary>
public class SC_Headbobing : MonoBehaviour
{

    public Transform TRS_Camera;
    public Rigidbody rb_Mecha;

    float f_Height;
    float f_InitialHeight;
    float f_LastHeight;

    float f_MaxAmplitude;
    public float f_MaxAmplitudeFactor = 7;

    float f_HeadBobingTimer;
    public float f_FrequenceFactor;


    // Start is called before the first frame update
    void Start()
    {
        //Taille
        f_InitialHeight = transform.position.y;
        //Hauteur Actuelle du Cockpit
        f_Height = f_InitialHeight;
        //Hauteur Précédente du Cockpit
        f_LastHeight = f_InitialHeight;

        //Amplitude du HeadBobing
        f_MaxAmplitude = f_InitialHeight / 100 * f_MaxAmplitudeFactor;

        //Timer
        f_HeadBobingTimer = 1f;

    }

    private void Update()
    {
        if (rb_Mecha.velocity.magnitude != 0)
            HeadBob();
        else if (rb_Mecha.velocity.magnitude != 0 && TRS_Camera.position.y != f_InitialHeight)
            ResetHeight();
    }

    /// <summary>
    /// Headbob Marley
    /// </summary>
    public void HeadBob()
    {

        //Anciene Hauteur
        f_LastHeight = f_Height;

        float f_CurAmplitude = (Mathf.Cos(f_HeadBobingTimer * f_FrequenceFactor) * f_MaxAmplitude) + f_InitialHeight;

        f_Height = f_Height - (f_InitialHeight - (f_InitialHeight - f_Height)) + f_CurAmplitude;

        TRS_Camera.position = new Vector3(TRS_Camera.position.x, f_Height, TRS_Camera.position.z);

        f_HeadBobingTimer += Time.deltaTime * Mathf.Floor(rb_Mecha.velocity.magnitude / 10);

    }

    public void ResetHeight()
    {

        if (f_HeadBobingTimer != 1f)
            f_HeadBobingTimer = 1f;

        if (TRS_Camera.position.y < f_InitialHeight)
            TRS_Camera.position = new Vector3(TRS_Camera.position.x, f_InitialHeight, TRS_Camera.position.z);

    }

}
