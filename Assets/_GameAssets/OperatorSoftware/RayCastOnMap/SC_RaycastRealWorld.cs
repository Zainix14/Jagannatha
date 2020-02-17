using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_RaycastRealWorld : MonoBehaviour
{

    //camera cockpit
    public GameObject Cam_FullView;
    //objet sur lequel est projetté la rendermap
    public GameObject Screens;
    //indicateur 3D placé au point de contact sur le terrain
    //public GameObject Indicator;

    private Vector3 v3_curDir = new Vector3(0, 0, 0);

    private RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //en Update dans ce script pour l'instant, mais à appeler par un autre script ultérieurement?
        castRayInWorld();
    }

    /// <summary>
    /// récupère le hit du cockpit et tire un rayon depuis les coordonnées UV de la collision avec l'écran.
    /// </summary>
    void castRayInWorld()
    {
        //récupération du ray du cockpit
        RaycastHit hitCockpit = Cam_FullView.GetComponent<SC_RaycastVirtual>().getRay();

        //On prend le rayon du point touché du viewport
        Ray ray = GetComponent<Camera>().ViewportPointToRay(new Vector3(hitCockpit.textureCoord.x, hitCockpit.textureCoord.y, 0));

        //on le lance
        if (Physics.Raycast(ray, out hit))
        {
            //debug
            Debug.DrawRay(transform.position, ray.direction * hit.distance, Color.yellow);

            //placement de l'indicateur
            //Indicator.transform.position = hit.point;

        }
    }
}
