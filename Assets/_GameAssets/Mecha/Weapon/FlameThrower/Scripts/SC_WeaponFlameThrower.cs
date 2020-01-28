using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_WeaponFlameThrower : MonoBehaviour, IF_Weapon
{
    public GameObject prefab_bullet;
    public GameObject helper_startPos;

    public float frequency;
    public int n_fireForce;
    public float scattering;

    GameObject[] t_Bullet; //Tableau permettant de stocker toutes les balles initialisées (Bullet pool )
    public int n_BulletMagazine; //Nombre de balles totale dans le bullet pool (a initialisé dans l'éditeur)
    int n_CurBullet; //Permet de stocker la prochaine balle a tirer dans le chargeur

    // Start is called before the first frame update
    void Awake()
    {
        CreateBulletPull();
    }

    void CreateBulletPull()
    {

        //Initialise le tableau de la longueur du chargeur voulu
        t_Bullet = new GameObject[n_BulletMagazine];

        for (int i = 0; i < n_BulletMagazine; i++)
        {

            //Initialisation du Prefab BallePilote par le Serveur pour la scene pilote et la scene opérateur
            GameObject curBullet = Instantiate(prefab_bullet, new Vector3(1000, 1000, 1000), Quaternion.identity);
            t_Bullet[i] = curBullet;

        }

        //Je sais plus pourquoi mais c'est utile tkt
        n_CurBullet = 0;

    }

    public void Trigger()
    {
        Debug.LogWarning("SC_WeaponFlameThrower - Not Coded");
    }

    void Fire()
    {

        t_Bullet[n_CurBullet].GetComponent<Rigidbody>().isKinematic = true;
        t_Bullet[n_CurBullet].transform.position = helper_startPos.transform.position;
        t_Bullet[n_CurBullet].transform.rotation = helper_startPos.transform.rotation;
        t_Bullet[n_CurBullet].GetComponent<MeshRenderer>().enabled = true;
        t_Bullet[n_CurBullet].GetComponent<Rigidbody>().isKinematic = false;

        //noise
        Vector3 dir = new Vector3(transform.forward.x + Random.Range(-scattering, +scattering), transform.forward.y + Random.Range(-scattering, +scattering), transform.forward.z + Random.Range(-scattering, +scattering));

        t_Bullet[n_CurBullet].GetComponent<Rigidbody>().AddForce(dir * n_fireForce);

        n_CurBullet++;

        if (n_CurBullet >= n_BulletMagazine)
            n_CurBullet = 0;

    }
}
