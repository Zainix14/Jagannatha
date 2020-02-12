using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Animation artisanale de la prise 
/// </summary>
public class electricPlug : MonoBehaviour
{
    Transform plugMale; //partie gauche
    Transform plugFemale; //Partie droite

    Quaternion firstOrientation; //Stockage de l'orientation à l'init
    bool b_allowRotate = true; //Sécurité | Rotation de l'objet

    void Start()
    {
        plugMale = gameObject.transform.GetChild(0); //Stockage enfant 0
        plugFemale = gameObject.transform.GetChild(1); //Stockage enfant 1
        plugMale.GetComponent<MeshRenderer>().material.color = new Color32(255, 0, 0, 255); //Ajout d'une couleur ici rouge
        plugFemale.GetComponent<MeshRenderer>().material.color = new Color32(255, 0, 0, 255); //meme couleur | pas le meme objet
        firstOrientation = transform.rotation; //Stockage de la rotation à l'init 
        
    }

    void Update()
    {
        if (b_allowRotate)
        {
            this.transform.Rotate(Vector3.up); //Tourne sur lui-meme
        }
        else
        {
            Quaternion curOrientation = transform.rotation; //Stockage de la rotation actuelle
            transform.rotation = Quaternion.Lerp(curOrientation, firstOrientation, 0.1f); //smoothy lerp into init rotation
        }

    }

    public void plugConnected()
    {
        b_allowRotate = false; //Sécurité
        gameObject.GetComponent<Animator>().SetBool("b_On", true); //Activation de l'animation (prise branchée)

        plugMale.GetComponent<MeshRenderer>().material.color = new Color32(0, 255, 0, 255); //ajout d'une couleur ici verte
        plugFemale.GetComponent<MeshRenderer>().material.color = new Color32(0, 255, 0, 255); //meme couleur | pas le meme objet
    }
}
