using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Place le joueur VR a la position du cockpit.
/// </summary>
public class SC_SetPosition : MonoBehaviour
{

    public float f_CockpitPos = -1000;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.position.y != f_CockpitPos && SceneManager.GetActiveScene().name != "Lobby")
            this.gameObject.transform.position = new Vector3 (0, f_CockpitPos, 0);
    }



}
