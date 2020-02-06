using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_WeaponManager : MonoBehaviour, IF_BreakdownSystem
{

    bool b_InBreakdown = false;
    bool b_BreakEngine = false;

    bool b_AlreadyCheck = false;

    [Header("Drop the Weapon")]
    [Tooltip("Tableau avec toute les Armes")]
    [SerializeField]
    GameObject[] tab_Weapons; //Tableau des cameras

    public int n_CurWeapon = 0;

    GameObject Mng_CheckList = null;

    // Start is called before the first frame update
    void Start()
    {
        IsCheck();
        CreateWeapon();
        CheckWeapon();
        SetWeapon(n_CurWeapon);
    }

    void IsCheck()
    {
        Mng_CheckList = GameObject.FindGameObjectWithTag("Mng_CheckList");
        if (Mng_CheckList != null)
            Mng_CheckList.GetComponent<SC_CheckList_Weapons>().Mng_Weapons = this.gameObject;
        else if (!b_AlreadyCheck)
            Debug.LogWarning("SC_WeaponManager - Can't Find Mng_CheckList");
        if (!b_AlreadyCheck)
            b_AlreadyCheck = true;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.Alpha0) || Input.GetAxis("Fire1") > 0) && !b_BreakEngine)
            Fire();
    }

    void CreateWeapon()
    {
        for (int i = 0; i < tab_Weapons.Length; i++)
        {
            tab_Weapons[i] = Instantiate(tab_Weapons[i]);
        }
    }

    void CheckWeapon()
    {
        for (int i = 0; i < tab_Weapons.Length; i++)
        {
            tab_Weapons[i].SetActive(true);
        }
    }

    void Fire()
    {
        tab_Weapons[n_CurWeapon].GetComponent<IF_Weapon>().Trigger();
    }

    /// <summary>
    /// Change l'arme equipé selon l'index |
    /// 0 = MiniGun
    /// 1 = Shrapnel
    /// 2 = FlameThrower
    /// </summary>
    /// <param name="n_Index"></param>
    public void SetWeapon(int n_Index)
    {

        if (!b_InBreakdown)
        {

            n_CurWeapon = n_Index;

            for (int i = 0; i < tab_Weapons.Length; i++)
            {
                if (i == n_Index)
                    tab_Weapons[i].GetComponent<SC_FollowHand>().b_OnFollow = true;
                else
                    tab_Weapons[i].GetComponent<SC_FollowHand>().b_OnFollow = false;
            }

        }
       
    }

    public void SetBreakdownState(bool State)
    {
        b_InBreakdown = State;
    }

    public void SetEngineBreakdownState(bool State)
    {
        b_BreakEngine = State;
    }

}
