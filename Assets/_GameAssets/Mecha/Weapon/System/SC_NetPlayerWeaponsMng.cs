using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SC_NetPlayerWeaponsMng : NetworkBehaviour
{

    public GameObject SpawnLaser(GameObject Laser)
    {
        GameObject GO_Laser_Temp = (GameObject)Instantiate(Laser, Laser.transform.position, Laser.transform.rotation);
        NetworkServer.Spawn(GO_Laser_Temp);
        return GO_Laser_Temp;
    }

    #region NoMoreUse

    [Command]
    public void CmdChangeWeapons(int n_WeapIndex)
    {
        SC_WeaponManager.Instance.SetWeapon(n_WeapIndex);
    }

    public void SetBtn(Button btn, int n_WeapIndex)
    {      
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => CmdChangeWeapons(n_WeapIndex));
    }

    #endregion NoMoreUse

}
