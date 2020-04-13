using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SC_NetPlayerDummyP_P : NetworkBehaviour
{

    bool b_NotInLobby = false;
    bool b_IsSpawn = false;

    public GameObject MechDummy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (isServer && !b_NotInLobby)
            CheckScene();

        if (isServer && b_NotInLobby && !b_IsSpawn && isLocalPlayer)
            SpawnMechDummy();

    }

    void CheckScene()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
            b_NotInLobby = true;
    }

    void SpawnMechDummy()
    {       
        b_IsSpawn = true;
        GameObject GO_MechDummy_Temp = (GameObject)Instantiate(MechDummy, MechDummy.transform.position, MechDummy.transform.rotation);
        NetworkServer.Spawn(GO_MechDummy_Temp);
    }

}
