using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SC_ChangeWeapBtn : MonoBehaviour
{

    GameObject Mng_CheckList;
    GameObject NetPlayerOP;
    SC_NetPlayerWeaponsMng NetPlayerWeaponMng;
    Button LocalBtn;

    public int n_WeaponIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        GetReferences();
    }

    // Update is called once per frame
    void Update()
    {
        if (LocalBtn == null  || Mng_CheckList == null || NetPlayerWeaponMng == null)
            GetReferences();
    }

    void GetReferences()
    {

        if (LocalBtn == null)
            LocalBtn = this.gameObject.GetComponent<Button>();

        if (LocalBtn != null && Mng_CheckList == null)
            Mng_CheckList = GameObject.FindGameObjectWithTag("Mng_CheckList");
        else
            Debug.LogWarning("SC_ChangeWeapBtn - Cant Find Mng_CheckList");

        if (Mng_CheckList != null && NetPlayerOP == null)
            NetPlayerOP = Mng_CheckList.GetComponent<SC_CheckList>().GetNetworkPlayerOperator();
        else
            Debug.LogWarning("SC_ChangeWeapBtn - Cant Find SC_NetPlayerWeaponsMng");

        if (NetPlayerOP != null && NetPlayerWeaponMng == null)
            NetPlayerWeaponMng = NetPlayerOP.GetComponent<SC_NetPlayerWeaponsMng>();
        else
            Debug.LogWarning("SC_ChangeWeapBtn - Cant Find SC_NetPlayerWeaponsMng");

        if (NetPlayerWeaponMng != null)
            SetBtn();
        else
            Debug.LogWarning("SC_ChangeWeapBtn - Cant Set Button");

    }

    void SetBtn()
    {
        NetPlayerWeaponMng.SetBtn(LocalBtn, n_WeaponIndex);
    }

}
