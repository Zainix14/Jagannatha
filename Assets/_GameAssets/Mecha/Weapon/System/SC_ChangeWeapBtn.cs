using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SC_ChangeWeapBtn : MonoBehaviour
{

    [Header("Get References")]
    [SerializeField]
    SC_NetPlayerWeaponsMng NetPlayerWeaponMng;
    [SerializeField]
    Button LocalBtn;

    [Header("Parameters")]
    [SerializeField]
    int n_WeaponIndex = 0;

    [Header("Infos")]
    [SerializeField]
    bool b_IsSet = false;

    // Start is called before the first frame update
    void Start()
    {
        GetReferences();
        SetBtn();
    }

    // Update is called once per frame
    void Update()
    {

        if (LocalBtn == null || NetPlayerWeaponMng == null)
            GetReferences();

        if (!b_IsSet)
            SetBtn();

    }

    void GetReferences()
    {

        if (LocalBtn == null)
            LocalBtn = this.gameObject.GetComponent<Button>();

        if ( NetPlayerWeaponMng == null)
            NetPlayerWeaponMng = SC_CheckList.Instance.NetworkPlayerOperator.GetComponent<SC_NetPlayerWeaponsMng>();

    }

    void SetBtn()
    {
        if (NetPlayerWeaponMng != null && !b_IsSet)
        {
            b_IsSet = true;
            NetPlayerWeaponMng.SetBtn(LocalBtn, n_WeaponIndex);
        }        
    }

}
