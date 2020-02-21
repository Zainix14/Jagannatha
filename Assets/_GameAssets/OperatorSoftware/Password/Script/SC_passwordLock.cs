using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;


public class SC_passwordLock : MonoBehaviour
{
    [SerializeField]
    GameObject objectPassword; //Contient le texte du inputField

    string s_password = "LV246"; //Mot de passe à taper

    [SerializeField]
    GameObject canvasMng; //récupération du DisplayManager

    [SerializeField]
    Text textFeedback; //Texte de feedback 


    float countTime = 0; //Compteur 
    bool unlock = false; //Sécurité

    [SerializeField]
    SC_electricPlug plugObject; //récupération de l'objet prise electrique

    [SerializeField]
    bool cheatCode = true;

    void Start()
    {
        if (cheatCode)
        {
            for (int i = 0; i < 4; i++)
            {
                canvasMng.GetComponent<SC_CanvasManager>().activateChild(i);
            }
            canvasMng.GetComponent<SC_CanvasManager>().checkTaskBeforeGo();
            gameObject.SetActive(false);
            unlock = false;
            countTime = 0;
        }
        else
        {
            canvasMng.GetComponent<SC_CanvasManager>().lockScreenDisplay();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(objectPassword.GetComponent<Text>().text == s_password) //Check du mot de passe
        {
            if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) //Validation
            {
                textFeedbackFunction("Valid password", new Color32(0, 255, 0, 255)); //Feedback textuel vert
                unlock = true; //Sécurité
                CustomSoundManager.Instance.PlaySound(gameObject, "SFX_o_opening", false, 0.4f);

                plugObject.GetComponent<SC_electricPlug>().plugConnected(); //Animation PLay
            }
            if (countTime > 4f) //Fin de compteur
            {
                canvasMng.GetComponent<SC_CanvasManager>().checkTaskBeforeGo(); //Activation des écrans (cf distributionDisplay)
                gameObject.SetActive(false); //désactivation du canvas de mot de passe (ContainerPassword)
                
                unlock = false; //Sécurité
                countTime = 0; //RaZ compteur
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Return)) //Mot de passe faux
            {
                textFeedbackFunction("Invalid password", new Color32(255, 0, 0, 255)); //feedback textuel rouge
            }
        }
        if(unlock)
        {
            countTime += Time.deltaTime; //Compteur ++
        }
        else
        {
            countTime = 0; //RaZ compteur
        }
    }
    /// <summary>
    /// Fonction qui permet d'agir sur un texte selon son contenu et sa couleur
    /// </summary>
    /// <param name="texte"></param>
    /// <param name="color"></param>
    void textFeedbackFunction(string texte,Color32 color)
    {
        textFeedback.color = color;
        textFeedback.text = texte;
    }
}
