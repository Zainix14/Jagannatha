using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SC_NetPlayer_Flock_P : MonoBehaviour
{

    public GameObject FBullet;
    public GameObject FLaser;

    public GameObject SpawnBulletF()
    {
        GameObject GO_BulletF_Temp = (GameObject)Instantiate(FBullet, FBullet.transform.position, FBullet.transform.rotation);
        NetworkServer.Spawn(GO_BulletF_Temp);
        return GO_BulletF_Temp;
    }

    public GameObject SpawnLaserF()
    {
        GameObject GO_LaserF_Temp = (GameObject)Instantiate(FLaser, FLaser.transform.position, FLaser.transform.rotation);
        NetworkServer.Spawn(GO_LaserF_Temp);
        return GO_LaserF_Temp;
    }

}
