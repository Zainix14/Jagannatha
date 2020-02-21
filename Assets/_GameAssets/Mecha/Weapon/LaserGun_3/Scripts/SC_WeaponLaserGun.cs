using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_WeaponLaserGun : MonoBehaviour, IF_Weapon, IF_BreakdownSystem
{

    bool b_InBreakdown = false;

    public GameObject prefab_bullet;
    public GameObject helper_startPos;
    public GameObject Target;
    SC_AimHit AimHit;

    public float frequency;

    [SerializeField]
    GameObject _bulletContainer;

    Vector3Int sensitivity;

    float timer = 0;

    GameObject[] t_Bullet; //Tableau permettant de stocker toutes les balles initialisées (Bullet pool )
    MeshRenderer[] t_MrBullet;
    public int n_BulletMagazine; //Nombre de balles totale dans le bullet pool (a initialisé dans l'éditeur)
    int n_CurBullet; //Permet de stocker la prochaine balle a tirer dans le chargeur

    GameObject Bullet;
    MeshRenderer mrBullet;

    bool laserFire;
    float laserTimer;

    GameObject Mng_CheckList;

    void Awake()
    {
        GetReferences();
        CreateBulletPull();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mng_CheckList == null || Target == null)
            GetReferences();
    }

    void GetReferences()
    {
        if(Mng_CheckList == null)
            Mng_CheckList = GameObject.FindGameObjectWithTag("Mng_CheckList");
        if (Target == null && Mng_CheckList != null)
            Target = Mng_CheckList.GetComponent<SC_CheckList_Weapons>().GetAimIndicator();
        if (Target != null && AimHit == null)
            AimHit = Target.GetComponent<SC_AimHit>();
    }

    void CreateBulletPull()
    {

        GameObject bulletContainer = Instantiate(_bulletContainer);

        Bullet = Instantiate(prefab_bullet, new Vector3(1000, 1000, 1000), Quaternion.identity);
        Bullet.transform.SetParent(bulletContainer.transform);
        mrBullet = Bullet.GetComponentInChildren<MeshRenderer>();

        /*
        //Initialise le tableau de la longueur du chargeur voulu
        t_Bullet = new GameObject[n_BulletMagazine];
        t_MrBullet = new MeshRenderer[n_BulletMagazine];

        for (int i = 0; i < n_BulletMagazine; i++)
        {
            //Initialisation du Prefab BallePilote par le Serveur pour la scene pilote et la scene opérateur
            GameObject curBullet = Instantiate(prefab_bullet, new Vector3(1000, 1000, 1000), Quaternion.identity);
            curBullet.transform.SetParent(bulletContainer.transform);
            t_Bullet[i] = curBullet;
            t_MrBullet[i] = curBullet.GetComponentInChildren<MeshRenderer>();
        }

        //Je sais plus pourquoi mais c'est utile tkt
        n_CurBullet = 0;
        */

    }

    public void Trigger()
    {

        if (timer > (1 / frequency))
        {
            timer = 0;
            if (!b_InBreakdown)
                Fire();
        }

        timer += Time.deltaTime;

    }

    void Fire()
    {

        laserFire = true;
        laserTimer += Time.deltaTime;
        //Positionne le laser a la base de l'arme (GunPos) et l'oriente dans la direction du point visée par le joueur
        Bullet.transform.position = Vector3.Lerp(helper_startPos.transform.position, Target.transform.position, .5f);
        Bullet.transform.LookAt(Target.transform.position);

        if(mrBullet.enabled == false)
            mrBullet.enabled = true;

        //Scale en Z le laser pour l'agrandir jusqu'a ce qu'il touche le point visée par le joueur (C STYLE TAHU)
        Bullet.transform.localScale = new Vector3(Bullet.transform.localScale.x,
                                                Bullet.transform.localScale.y,
                                                Vector3.Distance(helper_startPos.transform.position, Target.transform.position));

        if(AimHit.b_OnFire == false)
            AimHit.b_OnFire = true;

        //INSERT LASER SHIT
        CustomSoundManager.Instance.PlaySound(gameObject, "SFX_p_shoot_gun_1", false, 0.1f);

    }

    public void ReleaseTrigger()
    {
        Bullet.GetComponent<SC_BulletLaserGun>().ResetPos();
        AimHit.b_OnFire = false;
    }

    public void SetBreakdownState(bool State)
    {
        b_InBreakdown = State;
    }

    public void SetEngineBreakdownState(bool State) { }

    public Vector3Int GetWeaponSensitivity() { return sensitivity; }

    public void SetSensitivity(int index, int value)
    {
        switch (index)
        {
            case 0:
                sensitivity.x = value;
                break;
            case 1:
                sensitivity.y = value;
                break;
            case 2:
                sensitivity.z = value;
                break;

            default:

                break;
        }
    }

}
