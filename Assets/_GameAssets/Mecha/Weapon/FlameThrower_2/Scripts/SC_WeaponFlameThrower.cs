using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_WeaponFlameThrower : MonoBehaviour, IF_Weapon, IF_BreakdownSystem
{

    bool b_InBreakdown = false;

    public GameObject prefab_bullet;
    public GameObject helper_startPos;

    [SerializeField]
    GameObject _bulletContainer;

    public float frequency;
    public int n_fireForce;
    public float scattering;

    Vector3Int sensitivity;

    float timer = 0;

    GameObject[] t_Bullet; //Tableau permettant de stocker toutes les balles initialisées (Bullet pool )
    Rigidbody[] t_RbBullet;
    MeshRenderer[] t_MrBullet;
    public int n_BulletMagazine; //Nombre de balles totale dans le bullet pool (a initialisé dans l'éditeur)
    int n_CurBullet; //Permet de stocker la prochaine balle a tirer dans le chargeur

    // Start is called before the first frame update
    void Awake()
    {
        CreateBulletPull();
    }

    void CreateBulletPull()
    {

        GameObject bulletContainer = Instantiate(_bulletContainer);

        //Initialise le tableau de la longueur du chargeur voulu
        t_Bullet = new GameObject[n_BulletMagazine];
        t_RbBullet = new Rigidbody[n_BulletMagazine];
        t_MrBullet = new MeshRenderer[n_BulletMagazine];

        for (int i = 0; i < n_BulletMagazine; i++)
        {

            //Initialisation du Prefab BallePilote par le Serveur pour la scene pilote et la scene opérateur
            GameObject curBullet = Instantiate(prefab_bullet, new Vector3(1000, 1000, 1000), Quaternion.identity);
            curBullet.transform.SetParent(bulletContainer.transform);
            t_Bullet[i] = curBullet;
            t_RbBullet[i] = curBullet.GetComponent<Rigidbody>();
            t_MrBullet[i] = curBullet.GetComponentInChildren<MeshRenderer>();
        }

        //Je sais plus pourquoi mais c'est utile tkt
        n_CurBullet = 0;

    }

    public void Trigger()
    {

        if (timer > (1 / frequency))
        {
            timer = 0;
            if(!b_InBreakdown)
                Fire();
        }

        timer += Time.deltaTime;
    }

    public void ReleaseTrigger() { }
    void Fire()
    {

        t_RbBullet[n_CurBullet].isKinematic = true;
        t_Bullet[n_CurBullet].transform.position = helper_startPos.transform.position;
        t_Bullet[n_CurBullet].transform.rotation = helper_startPos.transform.rotation;
        t_MrBullet[n_CurBullet].enabled = true;
        t_RbBullet[n_CurBullet].isKinematic = false;

        //noise
        Vector3 dir = new Vector3(transform.forward.x + Random.Range(-scattering, +scattering), transform.forward.y + Random.Range(-scattering, +scattering), transform.forward.z + Random.Range(-scattering, +scattering));

        t_RbBullet[n_CurBullet].AddForce(dir * n_fireForce);

        n_CurBullet++;

        if (n_CurBullet >= n_BulletMagazine)
            n_CurBullet = 0;

    }

    public void SetBreakdownState(bool State)
    {
        b_InBreakdown = State;
    }

    public void SetEngineBreakdownState(bool State){}


    public Vector3Int GetWeaponSensitivity(){return sensitivity;}
    public void SetSensitivity(Vector3Int value){sensitivity = value;}
}
