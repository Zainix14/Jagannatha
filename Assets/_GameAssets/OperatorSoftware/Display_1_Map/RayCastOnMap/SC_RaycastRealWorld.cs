using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_RaycastRealWorld : MonoBehaviour
{

    //camera cockpit
    public GameObject Cam_Map;
    private Ray ray;
    public GameObject objectOnclic = null;
    public Vector3 sensi;
    public string type;
    public Text debugText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //en Update dans ce script pour l'instant, mais à appeler par un autre script ultérieurement?
        if(Input.GetMouseButton(0))
        {
            castRayInWorld();
        }
        if(objectOnclic != null)
        {
            //Debug.Log("élément selectionné : " + objectOnclic.name);
        }
        
    }

    /// <summary>
    /// récupère le hit du cockpit et tire un rayon depuis les coordonnées UV de la collision avec l'écran.
    /// </summary>
    void castRayInWorld()
    {
        RaycastHit hit = Cam_Map.GetComponent<SC_RaycastVirtual>().getRay();
        ray = this.GetComponent<Camera>().ViewportPointToRay(hit.textureCoord);

        //Debug.Log(hit.textureCoord);
        if (Physics.Raycast(ray, out hit))
        {
            //Debug.Log("Collider est " + hit.collider.name);
            if(hit.collider.GetComponent<IF_ClicableForOperator>() != null)
            {
                objectOnclic = hit.collider.gameObject;
                //Debug.Log("Clic on " + hit.collider.tag);
                sensi = hit.collider.GetComponent<IF_ClicableForOperator>().GetSensibility();
                type = hit.collider.GetComponent<IF_ClicableForOperator>().GetKoaID();
                
                //Debug.Log("Sensi à " + sensi);
                //debugText.text = sensi.ToString();
            }
            else
            {
                //Debug.Log("Clic on nothing on Map");
                objectOnclic = null;
            }
        }
    }
}
