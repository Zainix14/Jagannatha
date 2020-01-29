using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_WeaponManager : MonoBehaviour
{

    [Header("Drop the Weapon")]
    [Tooltip("Tableau avec toute les Armes")]
    [SerializeField]
    GameObject[] tab_Weapons; //Tableau des cameras

    int n_CurWeapon = 0;

    // Start is called before the first frame update
    void Start()
    {
        CreateWeapon();
        SetWeapon(n_CurWeapon);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            Fire();
    }

    void CreateWeapon()
    {
        for (int i = 0; i < tab_Weapons.Length; i++)
        {
            tab_Weapons[i] = Instantiate(tab_Weapons[i]);
        }
    }

    void Fire()
    {
        tab_Weapons[n_CurWeapon].GetComponent<IF_Weapon>().Trigger();
    }

    public void SetWeapon(int n_Index)
    {

        n_CurWeapon = n_Index;

        for (int i = 0; i < tab_Weapons.Length; i++)
        {
            if (i == n_Index)
                tab_Weapons[i].SetActive(true);
            else
                tab_Weapons[i].SetActive(false);
        }

    }

}
