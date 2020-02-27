using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SC_NetPlayerWeaponsMng : NetworkBehaviour
{

    GameObject Mng_CheckList;
    GameObject Mng_Weapons;
    SC_WeaponManager WeaponManager;

    // Start is called before the first frame update
    void Start()
    {
        if(isServer)
            GetReferences();
    }

    // Update is called once per frame
    void Update()
    {
        if (isServer && SceneManager.GetActiveScene().buildIndex != 0 && (Mng_CheckList == null || Mng_Weapons == null || WeaponManager == null))
            GetReferences();
    }

    void GetReferences()
    {

        if (Mng_CheckList == null)
            Mng_CheckList = GameObject.FindGameObjectWithTag("Mng_CheckList");
        else
            Debug.LogWarning("SC_NetPlayerWeaponsMng - Cant Find Mng_CheckList");

        if (Mng_CheckList != null && Mng_Weapons == null)
            Mng_Weapons = Mng_CheckList.GetComponent<SC_CheckList_Weapons>().GetMngWeapons();
        else
            Debug.LogWarning("SC_NetPlayerWeaponsMng - Cant Find Mng_Weapons");

        if (Mng_Weapons != null && WeaponManager == null)
            WeaponManager = Mng_Weapons.GetComponent<SC_WeaponManager>();
        else
            Debug.LogWarning("SC_NetPlayerWeaponsMng - Cant Find SC_WeaponManager");

    }

    [Command]
    public void CmdChangeWeapons(int n_WeapIndex)
    {
        //Debug.Log("SC_NetPlayerWeaponsMng - CmdChangeWeapons");
        WeaponManager.SetWeapon(n_WeapIndex);
    }

    public void SetBtn(Button btn, int n_WeapIndex)
    {      
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => CmdChangeWeapons(n_WeapIndex));
    }

    public GameObject SpawnLaser(GameObject Laser)
    {
        Debug.Log("SpawnLaser");
        GameObject GO_Laser_Temp = (GameObject)Instantiate(Laser, Laser.transform.position, Laser.transform.rotation);
        NetworkServer.Spawn(GO_Laser_Temp);
        return GO_Laser_Temp;
    }

}
