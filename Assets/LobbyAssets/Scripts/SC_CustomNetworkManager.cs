using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;


/// <summary>
/// Scene Lobby sur le Canvas
/// Destiné à remplacer le NetworkManagerHUD
/// Lance les fonctions pour connecter les joueurs entre eux
/// </summary>



public class SC_CustomNetworkManager : MonoBehaviour
{
    //public SC_SceneManager SC_SceneManager;
    public NetworkManager NetworkManager;

    //sécurité pour que l'appel ne soit fait qu'une fois
    bool b_HasClicked = false;

    public void StartHosting()
    {
        if(b_HasClicked == false)
        {
            NetworkManager.StartHost();
            b_HasClicked = true;
        }
    }

    public void StartClienting()
    {
        NetworkManager.StartClient();
    }

    //Lance la version DEBUG FPS du Pilote
    public void StartHostingFPS()
    {

        //SC_SceneManager.b_IsDebugFPS = true;
        NetworkManager.StartHost();

    }

}
