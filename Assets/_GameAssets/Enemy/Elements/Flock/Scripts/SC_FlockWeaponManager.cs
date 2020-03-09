using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_FlockWeaponManager : MonoBehaviour
{

    ////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////
    ///
    /////////////////////-- NETWORK --//////////////////////

    GameObject Mng_CheckList;
    GameObject NetPlayerP;
    SC_NetPlayer_Flock_P NetPFloackSC;

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
    SC_LaserFlock laserSC;
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

    void Awake()
    {
        GetReferences();
    }

    void Start()
    {
        
        Reset();
        
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public void Initialize(FlockSettings curFlockSettings)
    {
        flockSettings = curFlockSettings;
        switch (flockSettings.attackType)
        {
            case FlockSettings.AttackType.Bullet: //Bullet
                InitBulletPool();
                break;

            case FlockSettings.AttackType.Laser: //Laser
                InitLaser();
                break;
        }
    }

    void GetReferences()
    {
        if (Mng_CheckList == null)
            Mng_CheckList = GameObject.FindGameObjectWithTag("Mng_CheckList");
        if (Mng_CheckList != null && NetPlayerP == null)
            NetPlayerP = Mng_CheckList.GetComponent<SC_CheckList>().GetNetworkPlayerPilot();
        if (NetPlayerP != null && NetPFloackSC == null)
            NetPFloackSC = NetPlayerP.GetComponent<SC_NetPlayer_Flock_P>();
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

        if (Mng_CheckList == null || NetPlayerP == null || NetPFloackSC == null)
            GetReferences();
    }

    void FireUpdate()
    {
        if(isFiring)
        {
            timer += Time.deltaTime;
            switch (flockSettings.attackType)
            {
                case FlockSettings.AttackType.Bullet: //Bullet
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

                case FlockSettings.AttackType.Laser: //Laser

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

                case FlockSettings.AttackType.Kamikaze:

                    transform.position = Vector3.Lerp(transform.position, target.position, flockSettings.speedToTarget*Time.deltaTime);
                    if(Vector3.Distance(transform.position,target.position) < 20)
                    {
                        isFiring = false;
                        this.GetComponent<SC_FlockManager>()._SCKoaManager.GetHit(new Vector3(100,100,100));
                        Sc_ScreenShake.Instance.ShakeIt(0.025f, flockSettings.activeDuration);
                        SC_CockpitShake.Instance.ShakeIt(0.025f, flockSettings.activeDuration);
                        SC_MainBreakDownManager.Instance.causeDamageOnSystem(20);
                        //https://www.youtube.com/watch?v=kXYiU_JCYtU

                    }
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
            
            //GameObject curBullet = Instantiate(bulletPrefab);
            GameObject curBullet = NetPFloackSC.SpawnBulletF();
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

        bulletPool[n_CurBullet].GetComponent<SC_BulletFlock>().b_IsFire = true;

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
        //laser = Instantiate(laserPrefab);
        laser = NetPFloackSC.SpawnLaserF();
        laserFx = Instantiate(laserFxPrefab);
        laserSC = laser.GetComponent<SC_LaserFlock>();
    }


    void FireLaser()
    {
        laserFire = true;
        if(startLaser)
        {
            Sc_ScreenShake.Instance.ShakeIt(0.025f, flockSettings.activeDuration);
            SC_CockpitShake.Instance.ShakeIt(0.025f, flockSettings.activeDuration);
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

        laserSC.DisplayFlockLaser();

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
            laserSC.DisplayFlockLaser();

        }
        if (laserFx != null)
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
