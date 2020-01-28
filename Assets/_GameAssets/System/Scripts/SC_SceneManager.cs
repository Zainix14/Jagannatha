using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SC_SceneManager : NetworkBehaviour
{

    GameObject Mng_CheckList = null;

    [SyncVar]
    public int n_ConnectionsCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        IsCheck();
    }

    void IsCheck()
    {
        Mng_CheckList = GameObject.FindGameObjectWithTag("Mng_CheckList");
        Mng_CheckList.GetComponent<SC_CheckList>().Mng_Scene = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Lobby")
            LobbyUpdate();
    }

    void LobbyUpdate()
    {
        // on lit le nombre de connexions
        if (isServer)
            n_ConnectionsCount = NetworkServer.connections.Count;

        //si les deux joueurs sont connectés
        if (n_ConnectionsCount >= 2)
        {

            //si pas Server on load la scène opérateur
            if (!isServer && SceneManager.GetActiveScene().name == "Lobby")
                LoadTutoOperator();

            //si server on invoke le chargement de la scène pilote (la scène opérateur necessitant de se lancer en première)
            //sécurisé par b_hasInvoked pour n'être appelé qu'une fois
            if (isServer)
            {
                LoadTutoPilot();
            }

        }

    }

    void LoadTutoLobby()
    {
        SceneManager.LoadScene(0);
    }

    void LoadTutoPilot()
    {
        SceneManager.LoadScene(1);
    }

    void LoadTutoOperator()
    {
        SceneManager.LoadScene(2);
    }

    void LoadGamePilot()
    {
        SceneManager.LoadScene(3);
    }

    void LoadGameOperator()
    {
        SceneManager.LoadScene(4);
    }

}
