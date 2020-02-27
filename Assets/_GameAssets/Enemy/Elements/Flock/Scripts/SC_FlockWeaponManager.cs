using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_FlockWeaponManager : MonoBehaviour
{

    ////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////

    ///////////////////////-- BOTH --//////////////////////
    FlockSettings flockSettings;
    Transform target;

    float timer;
    bool isFiring;

    ///////////////////////-- LASER --//////////////////////
    [SerializeField]                                      
    GameObject laserPrefab;
    [SerializeField]
    GameObject laserFxPrefab;

    GameObject laserFx;
    GameObject laser;
    bool laserFire;
    float laserTimer;
    bool startLaser;

    ///////////////////////-- BULLET --//////////////////////
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    GameObject bulletContainer;
    GameObject[] bulletPool;
    int n_CurBullet;
    int nbBulletFire;

    ////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////

    void Start()
    {
        Reset();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public void Initialize(FlockSettings curFlockSettings)
    {
        flockSettings = curFlockSettings;
        switch ((int)flockSettings.attackType)
        {
            case 0: //Bullet
                InitBulletPool();
                break;

            case 1: //Laser
                InitLaser();
                break;


        }
    }



    public void StartFire()
    {
        isFiring = true;
        startLaser = true;
    }

    // Update is called once per frame
    void Update()
    {
        FireUpdate();
    }

    void FireUpdate()
    {
        if(isFiring)
        {
            timer += Time.deltaTime;
            switch ((int)flockSettings.attackType)
            {
                case 0: //Bullet
                    if(timer >= 1/flockSettings.fireRate )
                    {
                        FireBullet();
                        timer = 0;
                        if(nbBulletFire >= flockSettings.nbBulletToShoot)
                        {
                            EndOfAttack();
                        }
                    }
                    break;

                case 1: //Laser

                    if(!laserFire)
                    {
                        laserFx.transform.position = transform.position;
                        float scale = (Time.deltaTime / flockSettings.chargingAttackTime)*5;
                        laserFx.transform.localScale += new Vector3 (scale,scale,scale);
                    }

                    if(timer >= flockSettings.chargingAttackTime)
                    {
                        FireLaser();
                    }
                    //PlayChargingLaserFX
                    //https://www.youtube.com/watch?v=y1_SCfLxLFA
                    break;


            }
        }
    }

    #region Bullet
    void InitBulletPool()
    {
        GameObject _bulletContainer = Instantiate(bulletContainer);

        bulletPool = new GameObject[20];
        for (int i = 0; i < 20; i++)
        {
            GameObject curBullet = Instantiate(bulletPrefab);
            bulletPool[i] = curBullet;
            curBullet.transform.SetParent(_bulletContainer.transform); 
        }
    }

    void FireBullet()
    {
        Rigidbody rb = bulletPool[n_CurBullet].GetComponent<Rigidbody>();

        bulletPool[n_CurBullet].transform.position = transform.position;
        bulletPool[n_CurBullet].transform.rotation = transform.rotation;

        rb.isKinematic = true;
        rb.isKinematic = false;

        //noise
        Vector3 dir = new Vector3(transform.forward.x , transform.forward.y , transform.forward.z );

        rb.AddForce(dir * 24000);

        n_CurBullet++;

        if (n_CurBullet >= bulletPool.Length)
            n_CurBullet = 0;

        nbBulletFire++;
    }

    #endregion

    #region Laser
    void InitLaser()
    {
        laser = Instantiate(laserPrefab);
        laserFx = Instantiate(laserFxPrefab);
    }


    void FireLaser()
    {
        laserFire = true;
        if(startLaser)
        {
            Sc_ScreenShake.Instance.ShakeIt(0.1f, flockSettings.activeDuration);
            SC_CockpitShake.Instance.ShakeIt(0.1f, flockSettings.activeDuration);
            SC_MainBreakDownManager.Instance.causeDamageOnSystem(20);
            startLaser = false;
        }


        laserTimer += Time.deltaTime;
        float scale = (Time.deltaTime / flockSettings.activeDuration);
        laserFx.transform.localScale -= new Vector3(scale*5, scale*5, scale*5);
        //Positionne le laser a la base de l'arme (GunPos) et l'oriente dans la direction du point visée par le joueur
        laser.transform.position = Vector3.Lerp(transform.position, target.position, .5f);
        laser.transform.LookAt(new Vector3(target.position.x,target.position.y-5,target.position.z));

        //Scale en Z le laser pour l'agrandir jusqu'a ce qu'il touche le point visée par le joueur (C STYLE TAHU)
        laser.transform.localScale = new Vector3(laser.transform.localScale.x +scale,
                                laser.transform.localScale.y + scale,
                                Vector3.Distance(transform.position, target.transform.position));
        if (laserTimer >= flockSettings.activeDuration)
        {
            DestroyFx();
            EndOfAttack();
        }

        //INSERT LASER SHIT

    }
    #endregion

    public void DestroyFx()
    {
        isFiring = false;
        if(laser != null)
        {

            laser.transform.localScale = new Vector3(0, 0, 0);
            laser.transform.position = new Vector3(0, -2000, 0);
        }
        if(laserFx != null)
        {

            laserFx.transform.localScale = new Vector3(0, 0, 0);
            laserFx.transform.position = new Vector3(0, -2000, 0);
        }
    }



    void EndOfAttack()
    {
        this.GetComponent<SC_FlockManager>().EndAttack();
        Reset();
    }

    void Reset()
    {
        laserFire = false;
        n_CurBullet = 0;
        nbBulletFire = 0;
        timer = 0;
        laserTimer = 0;
        isFiring = false;
    }
}
