using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Place le joueur VR a la position du cockpit.
/// </summary>
public class SC_SetPosition : MonoBehaviour
{

    bool b_PosSet = false;

    public float f_CockpitPos = -1000;
    public Camera cam_VR;
    public Camera cam_FPS;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!b_PosSet)
            SetPos();
    }

    void SetPos()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0 && this.gameObject.transform.position.y != f_CockpitPos)
        {
            b_PosSet = true;
            this.gameObject.transform.position = new Vector3(0, f_CockpitPos, 0);
            cam_VR.farClipPlane = 15;
            cam_FPS.farClipPlane = 15;
        }
    }



}
