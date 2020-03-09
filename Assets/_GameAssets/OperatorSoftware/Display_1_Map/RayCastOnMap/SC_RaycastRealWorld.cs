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
    GameObject OldObjectClic;
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
                if (hit.collider.GetComponent<SC_KoaSettingsOP>() != null)
                {
                    if (objectOnclic != null)
                    {
                        OldObjectClic = objectOnclic;
                        var OldSelect = OldObjectClic.GetComponent<SC_KoaSettingsOP>();
                        OldSelect.bSelected = false;
                        OldSelect.SetColor();
                    }
                        
                    SC_UI_Display_MapInfos_KoaState.Instance.SetNewKoaSettings(hit.collider.GetComponent<SC_KoaSettingsOP>());
                    objectOnclic = hit.collider.gameObject;
                    var script = objectOnclic.GetComponent<SC_KoaSettingsOP>();
                    script.bSelected = true;
                    script.SetColor();
                }
         

                //CHANGEMENT ETAT TUTO
                //Debug.Log(SC_GameStates.Instance.CurState);
                if (SC_GameStates.Instance.CurState == SC_GameStates.GameState.Tutorial2)
                {
                    SC_instruct_op_manager.Instance.Deactivate(0);
                    SC_instruct_op_manager.Instance.Activate(2);
                    SC_instruct_op_manager.Instance.Activate(3);
                    SC_instruct_op_manager.Instance.Deactivate(4);
                    SC_instruct_op_manager.Instance.Deactivate(5);


                    //Debug.Log("babar");
                }
                //Debug.Log("Sensi à " + sensi);
                //debugText.text = sensi.ToString();
            }
            else
            {
                //Debug.Log("Clic on nothing on Map");
                //objectOnclic = null;
                //SC_UI_Display_MapInfos_KoaState.Instance.activated = false;
            }
        }
    }
}
