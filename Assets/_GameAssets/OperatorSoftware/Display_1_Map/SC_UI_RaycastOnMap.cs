using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_UI_RaycastOnMap : MonoBehaviour
{
    /// <summary>
    /// Par defaut : Camera Map
    /// Debug :  Exemple d'utilisation : Si vous travaillez sur le canvas d'informations (gauche), il faut mettre la camera correspondante (Camera informations//Camera à gauche)
    /// A Modifier seulement pour tester les réactions du raycast, à remettre par défaut
    /// </summary>
    [Tooltip("Camera à mettre correspondant à l'écran de Test")]
    [SerializeField]
    Camera RaycastCamera;

    //[SerializeField]
    //BoxCollider boxColliderOfTheObject;


    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rayCastOnIt();
        }
    }
    void rayCastOnIt()
    {
        RaycastHit hit;

        //Debug.Log(RaycastCamera);
        Ray ray = RaycastCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            //TEST
            //if (hit.collider == boxColliderOfTheObject) //Possible de capter l'objet par le tag (objet instancié)
            //{
            //    Debug.Log("HIT Object : " + boxColliderOfTheObject)
            //    boxColliderOfTheObject.GetComponent<SC_test>().getHit(); //Prévoir script qui sera le comportement lors du clic
            //}

            //if (hit.collider == ligneFrequence)
            //{
            //    Debug.Log("HIT Line");
            //    hit.collider.GetComponent<frequenceLine>().getHit();
            //}
            //if (hit.collider.gameObject.tag == "bar")
            //{
            //    Debug.Log("HIT Bar");
            //    hit.collider.GetComponent<Bar>().getHit();
            //}

            if (hit.collider.gameObject.tag == "Koa")
            {
                Debug.Log("Koa Touch by your little mouse");
                //hit.collider.GetComponent<Bar>().getHit();
            }
        }
    }

}
