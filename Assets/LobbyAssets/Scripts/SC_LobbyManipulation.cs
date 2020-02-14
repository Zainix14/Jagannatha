using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

/// <summary>
/// Sur les controlleurs
/// Script permettant la visée et la selection des options dans le menu avec la manette VR
/// </summary>


public class SC_LobbyManipulation : MonoBehaviour
{
    /*
    //Récupération des inputs VR
    public SteamVR_Input_Sources curHandInput;
    public SteamVR_Action_Boolean InputAction;
    */
    //Tableau contenant tous les objets sujettes à la manipulation en VR
    public Collider[] tab_colliders;

    public SC_CustomNetworkManager SC_CustomNetworkManager;
    public GameObject Laser;
    Transform T_Laser;
    // Start is called before the first frame update
    void Start()
    {
        //Instancie le laser de visée
        GameObject curLaser =  Instantiate(Laser, transform.position,transform.rotation);
        T_Laser = curLaser.transform;
    }

    // Update is called once per frame
    void Update()
    {

        //Layer 5 : UI
        int layerMask = 1 << 5;

        RaycastHit hit_Raycast;
        if(Physics.Raycast(transform.position,transform.forward,out hit_Raycast,Mathf.Infinity, layerMask))
        {
            //Affichage du Raycast
            if (T_Laser != null)
                ShowLaser(hit_Raycast);
            //Debug.DrawRay(transform.position, transform.forward * hit_Raycast.distance, Color.yellow);

            if (SteamVR_Controller.Input(3).GetPressDown(SteamVR_Controller.ButtonMask.Trigger) || SteamVR_Controller.Input(4).GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                //On parcourt le tableau
                for(int i =0; i <tab_colliders.Length;i++)
                {
                    //On compare si l'objet collidé se trouve dedans
                    if (hit_Raycast.collider== tab_colliders[i])
                    {
                        //Au final on HOST QUELQUE SOIT L'OBJET COLLIDé   <================================= A modifier avec un tag et un appel de méthode de l'objet collidé pour != effets.
                        GameObject curCollider = hit_Raycast.collider.gameObject;
                        SC_CustomNetworkManager.StartHosting();
                    }

                }

            }
            
        }
     
    }

    //Scale et oriente le rayon d'après la hit distance et le point d'impact
    private void ShowLaser(RaycastHit hit)
    {
        // 2
        T_Laser.position = Vector3.Lerp(transform.position, hit.point, .5f);
        // 3
        T_Laser.LookAt(hit.point);
        // 4
        T_Laser.localScale = new Vector3(T_Laser.localScale.x,
                                                T_Laser.localScale.y,
                                                hit.distance);
    }

}
